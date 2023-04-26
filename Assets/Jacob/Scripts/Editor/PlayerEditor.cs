using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jacob.Scripts.Controllers;
using Jacob.Scripts.Data;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(Player))]
	public class PlayerEditor : UnityEditor.Editor
	{
		private static AnimatorController _animatorController;
		private Animator _animator;
		private PlayerOnFire _onFire;
		private bool _monitoringAnimatorController;
		private AudioSource _audioSource;

		private void OnEnable()
		{
			var script = target as Player;

			script.TryGetComponent(out _animator);

			_monitoringAnimatorController = true;
			EditorCoroutineUtility.StartCoroutineOwnerless(MonitorAnimatorController());

			script.TryGetComponent(out _onFire);
			script.TryGetComponent(out _audioSource);
		}

		private void OnDisable()
		{
			_monitoringAnimatorController = false;
			_animatorController = null;
		}

		public override void OnInspectorGUI()
		{
			// Get the current script's serialized data (this contains the data you see in the Player Inspector)
			var script = target as Player;

			MovementProperties(script);
			SprintProperties(script);
			JumpingProperties(script);
			AnimationProperties(script);
			OnFireAbilityProperties(script);
			SoundEffectProperties(script);

			serializedObject.ApplyModifiedProperties();
			Utilities.CheckIfGUIChanged(script);
		}

		private static void MovementProperties(Player script)
		{
			EditorGUILayout.LabelField("Movement Properties", EditorStyles.boldLabel);
			script.jumpForce = EditorGUILayout.FloatField("Jump Force", script.jumpForce);
			script.moveSpeed = EditorGUILayout.FloatField("Move Speed", script.moveSpeed);
			script.topDown = EditorGUILayout.Toggle("Top Down View", script.topDown);
			script.flipWhenTurningDirection =
				EditorGUILayout.Toggle("Flip When Turning Direction", script.flipWhenTurningDirection);
		}

		private static void SprintProperties(Player script)
		{
			EditorGUILayout.LabelField("Sprint Properties", EditorStyles.boldLabel);
			script.enableSprinting = EditorGUILayout.Toggle("Enable Sprinting", script.enableSprinting);
			using (new EditorGUI.DisabledScope(!script.enableSprinting))
			{
				script.maxMoveSpeed = EditorGUILayout.FloatField("Max Move Speed", script.maxMoveSpeed);
				script.secondsUntilFullSprint =
					EditorGUILayout.FloatField("Seconds until Full Sprint", script.secondsUntilFullSprint);
			}
		}

		private static void JumpingProperties(Player script)
		{
			EditorGUILayout.LabelField("Jumping Properties", EditorStyles.boldLabel);
			script.checkForGround = EditorGUILayout.Toggle("Check for Ground", script.checkForGround);

			using (new EditorGUI.DisabledScope(!script.checkForGround))
			{
				script.groundTag = EditorGUILayout.TextField("Ground Tag", script.groundTag);
			}
		}

		private void AnimationProperties(Player script)
		{
			EditorGUILayout.LabelField("Animation Properties", EditorStyles.boldLabel);

			if (!_animator)
			{
				EditorGUILayout.HelpBox(
					"You don't have the Animator Component attached. " +
					"Please attach the component for this functionality to work.",
					MessageType.Warning
				);
			}

			using (new EditorGUI.DisabledScope(!_animator))
			{
				EditorGUILayout.LabelField("Walking", EditorStyles.boldLabel);
				script.animationParameter ??= "";
				script.jumpingAnimationParameter ??= "";
				script.animationParameter =
					StringPopup(script.animationParameter, "Animation Parameter", script.animationType);
				script.animationType =
					(AnimationType)EditorGUILayout.EnumPopup("Animation Type", script.animationType);
				script.inputToTrack = (TrackInput)EditorGUILayout.EnumPopup("Input to Track", script.inputToTrack);

				EditorGUILayout.LabelField("Jumping", EditorStyles.boldLabel);
				script.jumpingAnimationParameter = StringPopup(script.jumpingAnimationParameter, "Animation Parameter",
					AnimationType.Boolean);
			}
		}

		private void OnFireAbilityProperties(Player script)
		{
			EditorGUILayout.LabelField("On Fire Ability Properties", EditorStyles.boldLabel);

			if (!_onFire)
			{
				EditorGUILayout.HelpBox(
					"You don't have the PlayerOnFire Component attached. " +
					"Please attach the component for this functionality to work.",
					MessageType.Warning
				);
			}

			using (new EditorGUI.DisabledScope(true))
			{
				EditorGUILayout.ObjectField("Player On Fire", _onFire, typeof(PlayerOnFire), false);
			}

			using (new EditorGUI.DisabledScope(!_onFire))
			{
				script.onFireAbilityKey =
					(KeyCode)EditorGUILayout.EnumPopup("On Fire Ability Key", script.onFireAbilityKey);
				script.onFireAbilityUnlocked =
					EditorGUILayout.Toggle("On Fire Ability Unlocked", script.onFireAbilityUnlocked);
				script.onFireTimer = EditorGUILayout.FloatField("On Fire Timer", script.onFireTimer);
			}
		}

		private void SoundEffectProperties(Player script)
		{
			EditorGUILayout.LabelField("Sound Effect Properties");

			if (!_onFire)
			{
				EditorGUILayout.HelpBox(
					"You don't have the AudioSource Component attached. " +
					"Please attach the component for this functionality to work.",
					MessageType.Warning
				);
			}

			using (new EditorGUI.DisabledScope(true))
			{
				EditorGUILayout.ObjectField("Audio Source", _audioSource, typeof(AudioSource), false);
			}

			using (new EditorGUI.DisabledScope(!_audioSource))
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty("walkingSoundEffect"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("jumpingSoundEffect"));
			}
		}

		private static AnimationParameters GetAnimationParameters(AnimatorControllerParameterType type,
			IEnumerable<AnimatorControllerParameter> parameterList)
		{
			var array = (from parameter in parameterList where parameter.type == type select parameter.name).ToArray();
			var dict = new Dictionary<string, int>();
			for (var i = 0; i < array.Length; i++)
			{
				dict.Add(array[i], i);
			}

			return new AnimationParameters
			{
				ParameterArray = array,
				StringToIndexDictionary = dict
			};
		}

		private string StringPopup(string animationParameterString, string label, AnimationType type)
		{
			var convertedType = type switch
			{
				AnimationType.Float => AnimatorControllerParameterType.Float,
				AnimationType.Boolean => AnimatorControllerParameterType.Bool,
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
			if (!_animator || !_animatorController)
			{
				EditorGUILayout.HelpBox(
					"Unable to load an Animation Controller. " +
					"Please attach an Animation Controller to your Animator.",
					MessageType.Error
				);
				using (new EditorGUI.DisabledScope(true))
				{
					EditorGUILayout.Popup(selectedIndex: 0, displayedOptions: new[] { animationParameterString },
						label: label);
				}

				return animationParameterString;
			}

			if (_animatorController.parameters.Length == 0)
			{
				EditorGUILayout.HelpBox(
					"Your Animation Controller has no Parameters. " +
					"Please add some Parameters to your Animation Controller.",
					MessageType.Error
				);

				using (new EditorGUI.DisabledScope(true))
				{
					EditorGUILayout.Popup(selectedIndex: 0, displayedOptions: new[] { animationParameterString },
						label: label);
				}

				return animationParameterString;
			}

			var parameters = GetAnimationParameters(convertedType, _animatorController.parameters);
			var popup = EditorGUILayout.Popup(
				selectedIndex: parameters.StringToIndexDictionary.TryGetValue(animationParameterString, out var value)
					? value
					: 0,
				displayedOptions: parameters.ParameterArray,
				label: label
			);
			return _animatorController.parameters[popup].name;
		}

		private void SetAnimatorController()
		{
			_animatorController =
				AssetDatabase.LoadAssetAtPath<AnimatorController>(
					AssetDatabase.GetAssetPath(_animator.runtimeAnimatorController));
		}

		private IEnumerator MonitorAnimatorController()
		{
			while (_monitoringAnimatorController)
			{
				if (_animator)
				{
					if (!_animator.runtimeAnimatorController && _animatorController)
					{
						_animatorController = null;
					}

					if (_animator.runtimeAnimatorController && !_animatorController)
					{
						SetAnimatorController();
					}

					if (_animatorController && _animator.runtimeAnimatorController &&
					    _animatorController.name != _animator.runtimeAnimatorController.name)
					{
						SetAnimatorController();
					}
				}

				yield return new WaitForEndOfFrame();
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using Jacob.Scripts.Controllers;
using Jacob.Scripts.Data;
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

		private void OnEnable()
		{
			var script = target as Player;

			script.TryGetComponent(out _animator);

			if (!_animatorController && _animator)
			{
				_animatorController =
					AssetDatabase.LoadAssetAtPath<AnimatorController>(
						AssetDatabase.GetAssetPath(_animator.runtimeAnimatorController));
			}

			script.TryGetComponent(out _onFire);
		}

		private void OnDisable()
		{
			_animatorController = null;
		}

		public override void OnInspectorGUI()
		{
			var script = target as Player;

			script.jumpForce = EditorGUILayout.FloatField("Jump Force", script.jumpForce);
			script.moveSpeed = EditorGUILayout.FloatField("Move Speed", script.moveSpeed);
			script.topDown = EditorGUILayout.Toggle("Top Down View", script.topDown);

			EditorGUILayout.LabelField("Sprint Properties", EditorStyles.boldLabel);
			script.checkForGround = EditorGUILayout.Toggle("Check for Ground", script.checkForGround);

			using (new EditorGUI.DisabledScope(!script.checkForGround))
			{
				script.groundTag = EditorGUILayout.TextField("Ground Tag", script.groundTag);
			}

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
				script.animationParameter =
					StringPopup(script.animationParameter, "Animation Parameter", script.animationType);
				script.animationType =
					(AnimationType)EditorGUILayout.EnumPopup("Animation Type", script.animationType);
				script.inputToTrack = (TrackInput)EditorGUILayout.EnumPopup("Input to Track", script.inputToTrack);

				EditorGUILayout.LabelField("Jumping", EditorStyles.boldLabel);
				script.jumpingAnimationParameter = StringPopup(script.jumpingAnimationParameter, "Animation Parameter",
					AnimationType.Boolean);
			}

			EditorGUILayout.LabelField("On Fire Ability Properties", EditorStyles.boldLabel);

			if (!_onFire)
			{
				EditorGUILayout.HelpBox(
					"You don't have the PlayerOnFire Component attached. " +
					"Please attach the component for this functionality to work.",
					MessageType.Warning
				);
			}

			using (new EditorGUI.DisabledScope(!_onFire))
			{
				script.onFireAbilityKey =
					(KeyCode)EditorGUILayout.EnumPopup("On Fire Ability Key", script.onFireAbilityKey);
				script.onFireAbilityUnlocked =
					EditorGUILayout.Toggle("On Fire Ability Unlocked", script.onFireAbilityUnlocked);
				script.onFireTimer = EditorGUILayout.FloatField("On Fire Timer", script.onFireTimer);
			}

			Utilities.CheckIfGUIChanged(script);
		}

		private static (string[], Dictionary<string, int>) GetAnimationParameters(AnimatorControllerParameterType type,
			IEnumerable<AnimatorControllerParameter> parameterList)
		{
			var array = (from parameter in parameterList where parameter.type == type select parameter.name).ToArray();
			var dict = new Dictionary<string, int>();
			for (var i = 0; i < array.Length; i++)
			{
				dict.Add(array[i], i);
			}

			return (array, dict);
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

			var parameters = GetAnimationParameters(convertedType, _animatorController.parameters);
			var popup = EditorGUILayout.Popup(
				selectedIndex: parameters.Item2.ContainsKey(animationParameterString)
					? parameters.Item2[animationParameterString]
					: 0,
				displayedOptions: parameters.Item1,
				label: label
			);
			return _animatorController.parameters[popup].name;
		}
	}
}
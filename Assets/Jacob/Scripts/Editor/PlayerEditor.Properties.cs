using Jacob.Scripts.Controllers;
using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	public partial class PlayerEditor
	{
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
	}
}
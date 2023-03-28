using Jacob.Controllers;
using Jacob.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Editor
{
	[CustomEditor(typeof(Player), true)]
	public class PlayerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var script = target as Player;

			var animator = script.GetComponent<Animator>();
			var onFire = script.GetComponent<PlayerOnFire>();

			using (var group = new EditorGUI.DisabledScope(!script.checkForGround))
			{
				script.groundTag = EditorGUILayout.TextField("Ground Tag", script.groundTag);
			}

			EditorGUILayout.LabelField("Animation Properties", EditorStyles.boldLabel);
			using (var group = new EditorGUI.DisabledScope(!animator))
			{
				if (!animator)
				{
					EditorGUILayout.LabelField("You don't have the Animator Component attached.");
					EditorGUILayout.LabelField("Please attach the component for this to work.");
				}

				script.animationParameter =
					EditorGUILayout.TextField("Animation Parameter", script.animationParameter);
				script.animationType =
					(AnimationType)EditorGUILayout.EnumPopup("Animation Type", script.animationType);
				script.inputToTrack = (TrackInput)EditorGUILayout.EnumPopup("Input to Track", script.inputToTrack);
			}

			EditorGUILayout.LabelField("On Fire Ability Properties", EditorStyles.boldLabel);
			using (var group = new EditorGUI.DisabledScope(!onFire))
			{
				if (!onFire)
				{
					EditorGUILayout.LabelField("You don't have the PlayerOnFire Component attached.");
					EditorGUILayout.LabelField("Please attach the component for this to work.");
				}

				script.onFireAbilityKey =
					(KeyCode)EditorGUILayout.EnumPopup("On Fire Ability Key", script.onFireAbilityKey);
				script.onFireAbilityUnlocked =
					EditorGUILayout.Toggle("On Fire Ability Unlocked", script.onFireAbilityUnlocked);
				script.onFireTimer = EditorGUILayout.FloatField("On Fire Timer", script.onFireTimer);
			}
			
			Utilities.CheckIfGUIChanged(script);
		}
	}
}
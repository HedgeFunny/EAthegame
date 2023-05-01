using Jacob.Scripts.Controllers;
using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(HealthBar))]
	public class HealthBarEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var script = target as HealthBar;

			if (script.TryGetComponent<Animator>(out var animator))
			{
				script.animator = animator;
			}
			else
			{
				script.animator = null;
				EditorGUILayout.HelpBox("This Script requires an Animator attached to this GameObject.",
					MessageType.Error);
			}

			using (new EditorGUI.DisabledScope(true))
			{
				EditorGUILayout.ObjectField("Animator", script.animator, typeof(Animator), false);
			}

			script.useGameManagerHealthSystem =
				EditorGUILayout.Toggle("Use GameManager Health", script.useGameManagerHealthSystem);

			if (!script.healthSystem && !script.useGameManagerHealthSystem)
			{
				EditorGUILayout.HelpBox("The HealthBar script requires a Health component to function.",
					MessageType.Warning);
			}

			using (new EditorGUI.DisabledScope(script.useGameManagerHealthSystem))
			{
				script.healthSystem =
					(Health)EditorGUILayout.ObjectField("Health System", script.healthSystem, typeof(Health), true);
			}

			script.animationType =
				(HealthBarAnimationType)EditorGUILayout.EnumPopup("Animation Type", script.animationType);

			serializedObject.ApplyModifiedProperties();
			Utilities.CheckIfGUIChanged(script);
		}
	}
}
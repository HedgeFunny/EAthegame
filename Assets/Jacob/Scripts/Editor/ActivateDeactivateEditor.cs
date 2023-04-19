using Jacob.Scripts.Controllers;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(ActivateDeactivate))]
	public class ActivateDeactivateEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var script = target as ActivateDeactivate;
			var hasCollider = script.TryGetComponent<Collider2D>(out _);

			if (!hasCollider)
			{
				EditorGUILayout.HelpBox(
					"This Script requires a Collider2D to have the Click and Collide functions to work. " +
					"Without a Collider2D, " +
					"you can only call the Activate and Deactivate methods from Scripts and Events.",
					MessageType.Warning
				);
			}

			using (new EditorGUI.DisabledScope(!hasCollider))
			{
				script.clickToTrigger = EditorGUILayout.Toggle("Click to Trigger", script.clickToTrigger);
				script.collideToTrigger = EditorGUILayout.Toggle("Collide to Trigger", script.collideToTrigger);
			}

			EditorGUILayout.PropertyField(serializedObject.FindProperty("activate"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("deactivate"));

			serializedObject.ApplyModifiedProperties();
			
			Utilities.CheckIfGUIChanged(script);
		}
	}
}
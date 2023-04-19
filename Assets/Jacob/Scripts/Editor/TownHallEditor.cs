using Jacob.Scripts.Controllers;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(TownHall))]
	public class TownHallEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var script = target as TownHall;
			var collider = script.TryGetComponent<Collider2D>(out _);

			EditorGUILayout.LabelField("Health Properties", EditorStyles.boldLabel);
			script.maxHealth = EditorGUILayout.DoubleField("Max Health", script.maxHealth);

			EditorGUILayout.LabelField("Collider Checking Properties", EditorStyles.boldLabel);

			if (!collider)
			{
				if (script.TryGetComponent<Collider>(out _))
				{
					EditorGUILayout.HelpBox("Using a 3D Collider is not compatible with this script. Use a Collider2D.",
						MessageType.Error);
				}
				else
				{
					EditorGUILayout.HelpBox("You need to have a Collider2D on your Object to use these properties.",
						MessageType.Warning);
				}
			}

			using (var _ = new EditorGUI.DisabledScope(!collider))
			{
				script.tagToCheck = EditorGUILayout.TextField("Tag to Check", script.tagToCheck);
				script.healthToSubtract = EditorGUILayout.DoubleField("Health to Subtract", script.healthToSubtract);
			}

			Utilities.CheckIfGUIChanged(script);
		}
	}
}
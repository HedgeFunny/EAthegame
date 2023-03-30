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
			var collider = script.GetComponent<Collider2D>();

			EditorGUILayout.LabelField("Health Properties", EditorStyles.boldLabel);
			script.maxHealth = EditorGUILayout.DoubleField("Max Health", script.maxHealth);

			EditorGUILayout.LabelField("Collider Checking Properties", EditorStyles.boldLabel);

			if (!collider)
			{
				EditorGUILayout.LabelField("You need to have a Collider on your Object to use these properties.");
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
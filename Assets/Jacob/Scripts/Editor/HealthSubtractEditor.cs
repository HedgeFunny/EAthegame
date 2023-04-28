using Jacob.Scripts.Controllers;
using UnityEditor;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(HealthSubtract))]
	public class HealthSubtractEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var script = target as HealthSubtract;

			script.useGameManagerHealth = EditorGUILayout.Toggle("Use GameManager Health", script.useGameManagerHealth);
			
			using (new EditorGUI.DisabledScope(script.useGameManagerHealth))
			{
				script.healthSystem =
					(Health)EditorGUILayout.ObjectField("Health System", script.healthSystem, typeof(Health), true);
			}

			script.healthToSubtract = EditorGUILayout.DoubleField("Health to Subtract", script.healthToSubtract);


			Utilities.CheckIfGUIChanged(script);
		}
	}
}
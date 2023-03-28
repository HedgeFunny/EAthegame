using Jacob.Scripts.Controllers;
using Jacob.Scripts.Data;
using UnityEditor;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(GameManager))]
	public class GameManagerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var script = target as GameManager;

			EditorGUILayout.LabelField("Currency Properties", EditorStyles.boldLabel);
			script.returnType = (GameManagerReturnType)EditorGUILayout.EnumPopup("Return Type", script.returnType);
			switch (script.returnType)
			{
				case GameManagerReturnType.Float:
					EditorGUILayout.PropertyField(serializedObject.FindProperty("whenMoneyChangesFloat"));
					break;
				case GameManagerReturnType.String:
					EditorGUILayout.PropertyField(serializedObject.FindProperty("whenMoneyChangesString"));
					break;
			}

			serializedObject.ApplyModifiedProperties();
			Utilities.CheckIfGUIChanged(script);
		}
	}
}
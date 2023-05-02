using System.Collections.Generic;
using Jacob.Scripts.Controllers;
using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(GameManager))]
	public class GameManagerEditor : UnityEditor.Editor
	{
		private const string HealthSystemWarning =
			"You need a Health object attached for GameManager Health functionality to work. " +
			"Not having one defined will have calling Health methods throw an exception.";

		public override void OnInspectorGUI()
		{
			var script = target as GameManager;

			EditorGUILayout.LabelField("Health Properties", EditorStyles.boldLabel);

			if (!script.healthComponent)
			{
				EditorGUILayout.HelpBox(HealthSystemWarning, MessageType.Warning);
			}

			script.healthComponent =
				(Health)EditorGUILayout.ObjectField("Health System", script.healthComponent, typeof(Health), true);

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
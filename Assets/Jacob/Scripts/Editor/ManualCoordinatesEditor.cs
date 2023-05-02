using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(ManualCoordinatesData))]
	public class ManualCoordinatesEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var script = target as ManualCoordinatesData;

			EditorGUILayout.LabelField("X Axis", EditorStyles.boldLabel);
			script.minX = EditorGUILayout.FloatField("Minimum", script.minX);
			script.maxX = EditorGUILayout.FloatField("Maximum", script.maxX);

			EditorGUILayout.LabelField("Y Axis", EditorStyles.boldLabel);
			script.minY = EditorGUILayout.FloatField("Minimum", script.minY);
			script.maxY = EditorGUILayout.FloatField("Maximum", script.maxY);

			Utilities.CheckIfGUIChangedObject(script);
			
			using (new EditorGUI.DisabledScope(!EditorUtility.IsDirty(script)))
			{
				if (GUILayout.Button("Save"))
				{
					AssetDatabase.SaveAssetIfDirty(script);
				}
			}
		}
	}
}
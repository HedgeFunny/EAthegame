using Jacob.Scripts.Controllers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(Cam))]
	public class CamEditor: UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var script = target as Cam;

			script.followedObject = (Transform)EditorGUILayout.ObjectField("Followed Object", script.followedObject, typeof(Transform));
			EditorGUILayout.LabelField("Tilemap Settings", EditorStyles.boldLabel);
			script.clampToTilemap = EditorGUILayout.Toggle("Clamp to Tilemap", script.clampToTilemap);
			using var group = new EditorGUI.DisabledScope(!script.clampToTilemap);
			script.tilemap =
				(TilemapCollider2D)EditorGUILayout.ObjectField("Tilemap", script.tilemap,
					typeof(TilemapCollider2D));
			script.clampVerticalPosition =
				EditorGUILayout.Toggle("Clamp Vertical Position", script.clampVerticalPosition);
		}
	}
}
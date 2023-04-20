using Jacob.Scripts.Controllers;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(DebugSupport))]
	public class DebugSupportEditor : UnityEditor.Editor
	{
		private const string MainPlayerTooltip =
			"This will load the Main Player into the Ad you load. " +
			"If this is disabled, the Main Player will not be in the Ad you load.";

		public override void OnInspectorGUI()
		{
			var script = target as DebugSupport;

			script.enableMainPlayer = EditorGUILayout.Toggle(new GUIContent("Enable Main Player", MainPlayerTooltip),
				script.enableMainPlayer);
			using (new EditorGUI.DisabledScope(!script.enableMainPlayer))
			{
				script.locationToTeleportTo =
					EditorGUILayout.Vector2Field("Location to Teleport To", script.locationToTeleportTo);
			}
		}
	}
}
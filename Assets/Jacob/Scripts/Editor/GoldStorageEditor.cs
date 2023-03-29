using Jacob.Scripts.Controllers;
using UnityEditor;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(GoldStorage))]
	public class GoldStorageEditor: UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var gameManager = FindObjectOfType<GameManager>();
			if (!gameManager)
			{
				EditorGUILayout.LabelField("You need a GameManager for this script to work.");
			}
			using var group = new EditorGUI.DisabledScope(!gameManager);
			base.OnInspectorGUI();
		}
	}
}
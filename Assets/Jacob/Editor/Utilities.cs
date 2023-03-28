using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Jacob.Editor
{
	public abstract class Utilities
	{
		public static void CheckIfGUIChanged(Component script)
		{
			if (!GUI.changed) return;
			EditorUtility.SetDirty(script);
			EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
		}
	}
}
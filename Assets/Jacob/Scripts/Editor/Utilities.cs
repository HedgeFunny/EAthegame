using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	public abstract class Utilities
	{
		public static void CheckIfGUIChangedObject(Object script)
		{
			if (Application.isPlaying) return;
			if (!GUI.changed) return;
			EditorUtility.SetDirty(script);
		}
		
		public static void CheckIfGUIChanged(Component script)
		{
			if (Application.isPlaying) return;
			if (!GUI.changed) return;
			CheckIfGUIChangedObject(script);
			EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
		}
	}
}
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	public abstract class Utilities
	{
		internal static void CheckIfGUIChangedObject(Object script)
		{
			if (Application.isPlaying) return;
			if (!GUI.changed) return;
			EditorUtility.SetDirty(script);
		}

		internal static void CheckIfGUIChanged(Component script)
		{
			if (Application.isPlaying) return;
			if (!GUI.changed) return;
			CheckIfGUIChangedObject(script);
			EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
		}

		internal static string ComponentWarning(string componentName) =>
			$"You don't have the {componentName} Component attached. " +
			"Please attach the component for this functionality to work.";
	}
}
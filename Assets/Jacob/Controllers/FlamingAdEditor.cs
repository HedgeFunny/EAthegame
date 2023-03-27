using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Jacob.Controllers
{
	[CustomEditor(typeof(FlamingAd))]
	public class FlamingAdEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var script = target as FlamingAd;

			script.runObsoleteCode = EditorGUILayout.Toggle("Run Obsolete Code", script.runObsoleteCode);

			using var group = new EditorGUI.DisabledScope(!script.runObsoleteCode);
			script.mainSceneActive = EditorGUILayout.Toggle("Main Scene Active", script.mainSceneActive);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickedEnough"));
			
			CheckIfGUIChanged(script);
		}

		private static void CheckIfGUIChanged(Component script)
		{
			if (!GUI.changed) return;
			EditorUtility.SetDirty(script);
			EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
		}
	}
}
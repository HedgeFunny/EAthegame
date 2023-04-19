using Jacob.Scripts.Controllers;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(HealthBar))]
	public class HealthBarEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var script = target as HealthBar;
			var gameManager = FindObjectOfType<GameManager>();

			if (script.TryGetComponent<Animator>(out var animator))
			{
				script.animator = animator;
			}
			else
			{
				script.animator = null;
				EditorGUILayout.HelpBox("This Script requires an Animator attached to this GameObject.",
					MessageType.Error);
			}

			if (gameManager)
			{
				script.GameManager = gameManager;
			}
			else
			{
				script.GameManager = null;
				EditorGUILayout.HelpBox("This Script requires a GameManager to be in your Scene.", MessageType.Error);
			}

			using var group = new EditorGUI.DisabledScope(true);
			EditorGUILayout.ObjectField("Animator", script.animator, typeof(Animator));
			EditorGUILayout.ObjectField("Game Manager", script.GameManager, typeof(GameManager));

			Utilities.CheckIfGUIChanged(script);
		}
	}
}
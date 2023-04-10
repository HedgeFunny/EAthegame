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
			var animator = script.GetComponent<Animator>();
			var gameManager = FindObjectOfType<GameManager>();

			if (animator)
			{
				script.animator = animator;
			}
			else
			{
				script.animator = null;
				EditorGUILayout.LabelField("This Script requires an Animator attached to this GameObject.");
			}

			if (gameManager)
			{
				script.gameManager = gameManager;
			}
			else
			{
				script.gameManager = null;
				EditorGUILayout.LabelField("This Script requires a GameManager to be in your Scene.");
			}

			using var group = new EditorGUI.DisabledScope(true);
			EditorGUILayout.ObjectField("Animator", script.animator, typeof(Animator));
			EditorGUILayout.ObjectField("Game Manager", script.gameManager, typeof(GameManager));

			Utilities.CheckIfGUIChanged(script);
		}
	}
}
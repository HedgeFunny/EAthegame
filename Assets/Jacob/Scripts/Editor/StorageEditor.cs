using Jacob.Scripts.Controllers;
using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	[CustomEditor(typeof(Storage))]
	public class StorageEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var script = target as Storage;
			var gameManager = FindObjectOfType<GameManager>();
			var storage = Resources.Load("StoragePresetSettings") as StoragePreset;
			if (!storage)
			{
				storage = StoragePreset.CreateStoragePreset();
			}

			if (GUILayout.Button("Preset Settings"))
			{
				StorageSettingsWindow.ShowWindow();
				return;
			}

			if (!gameManager)
			{
				EditorGUILayout.LabelField("You need a GameManager for this script to work.");
			}

			using var group = new EditorGUI.DisabledScope(!gameManager);

			EditorGUILayout.BeginHorizontal();
			using (var _ = new EditorGUI.DisabledScope(IfPreset(script, storage.goldStorageAmount,
				       storage.goldStorageTime)))
			{
				if (GUILayout.Button("Set Gold Storage Preset"))
				{
					if (IfPreset(script, 100, 1)) return;
					script.amountYouWantToGenerate = storage.goldStorageAmount;
					script.timeBetweenGeneration = storage.goldStorageTime;
					Utilities.CheckIfGUIChanged(script);
				}
			}

			using (var _ = new EditorGUI.DisabledScope(IfPreset(script, storage.elixirAmount, storage.elixirTime)))
			{
				if (GUILayout.Button("Set Elixir Storage Preset"))
				{
					if (IfPreset(script, 1000, 10)) return;
					script.amountYouWantToGenerate = storage.elixirAmount;
					script.timeBetweenGeneration = storage.elixirTime;
					Utilities.CheckIfGUIChanged(script);
				}
			}

			EditorGUILayout.EndHorizontal();

			script.amountYouWantToGenerate =
				EditorGUILayout.FloatField("Money to Generate", script.amountYouWantToGenerate);
			script.timeBetweenGeneration =
				EditorGUILayout.FloatField("Time between Generation", script.timeBetweenGeneration);
			script.generatingMoney = EditorGUILayout.Toggle("Generate Money", script.generatingMoney);
			Utilities.CheckIfGUIChanged(script);
		}

		private bool IfPreset(Storage script, float amount, float time)
		{
			return script.amountYouWantToGenerate == amount && script.timeBetweenGeneration == time;
		}
	}
}
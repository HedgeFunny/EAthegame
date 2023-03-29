using Jacob.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Editor
{
	public class StorageSettingsWindow : EditorWindow
	{
		public static void ShowWindow()
		{
			GetWindow<StorageSettingsWindow>("Storage Preset Settings");
		}

		private void OnGUI()
		{
			var storage = Resources.Load("StoragePresetSettings") as StoragePreset;
			if (!storage)
			{
				storage = StoragePreset.CreateStoragePreset();
			}

			EditorGUILayout.LabelField("Gold Storage", EditorStyles.boldLabel);
			storage.goldStorageAmount = EditorGUILayout.FloatField("Money to Generate",
				storage.goldStorageAmount);
			storage.goldStorageTime = EditorGUILayout.FloatField("Time between Generation",
				storage.goldStorageTime);
			EditorGUILayout.LabelField("Elixir Storage", EditorStyles.boldLabel);
			storage.elixirAmount = EditorGUILayout.FloatField("Money to Generate",
				storage.elixirAmount);
			storage.elixirTime = EditorGUILayout.FloatField("Time between Generation",
				storage.elixirTime);

			if (GUI.changed)
			{
				EditorUtility.SetDirty(storage);
			}

			using (var _ = new EditorGUI.DisabledScope(!EditorUtility.IsDirty(storage)))
			{
				if (GUILayout.Button("Save"))
				{
					AssetDatabase.SaveAssetIfDirty(storage);
					EditorUtility.ClearDirty(storage);
				}
			}

			using (var _ = new EditorGUI.DisabledScope(StoragePreset.IsDefault(storage)))
			{
				if (!GUILayout.Button("Reset to Default")) return;
				EditorUtility.SetDirty(storage);
				StoragePreset.ResetToDefault(storage);
			}
		}
	}
}
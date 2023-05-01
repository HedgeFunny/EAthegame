using UnityEditor;
using UnityEngine;

namespace Jacob.Scripts.Data
{
	[CreateAssetMenu(fileName = "StoragePresetSettings", menuName = "TotallyAverageMobileGame/StoragePreset")]
	public class StoragePreset : ScriptableObject
	{
		public const float GoldAmount = 100;
		public const float GoldTime = 1;
		public const float ElixirAmount = 1000;
		public const float ElixirTime = 5;
		
		public float goldStorageAmount = GoldAmount;
		public float goldStorageTime = GoldTime;
		public float elixirAmount = ElixirAmount;
		public float elixirTime = ElixirTime;

		public static void ResetToDefault(StoragePreset storage)
		{
			storage.goldStorageAmount = GoldAmount;
			storage.goldStorageTime = GoldTime;
			storage.elixirAmount = ElixirAmount;
			storage.elixirTime = ElixirTime;
		}

		public static bool IsDefault(StoragePreset storage)
		{
			return storage.goldStorageAmount == GoldAmount && storage.goldStorageTime == GoldTime &&
			       storage.elixirAmount == ElixirAmount && storage.elixirTime == ElixirTime;
		}

		#if UNITY_EDITOR
		public static StoragePreset CreateStoragePreset()
		{
			var storage = CreateInstance<StoragePreset>();
			AssetDatabase.CreateAsset(storage, "Assets/Jacob/Resources/StoragePresetSettings.asset");
			return storage;
		}
		#endif
	}
}
using System.IO;
using UnityEngine;

namespace Jacob.Scripts.Data
{
	public class SettingsManager
	{
		public static Settings Settings
		{
			get => ReadSettings();
			set => SetSettings(value);
		}
		
		private static readonly string Path = $"{Application.persistentDataPath}/Settings.json";

		private static Settings ReadSettings()
		{
			string file;
			
			if (File.Exists(Path))
			{
				file = File.ReadAllText(Path);
			}
			else
			{
				return new Settings();
			}

			return DeserializeSettings(file);
		}

		private static void SetSettings(Settings settings)
		{
			File.WriteAllText(Path, SerializeSettings(settings));
		}

		private static Settings DeserializeSettings(string settingsJson)
		{
			return JsonUtility.FromJson<Settings>(settingsJson);
		}

		private static string SerializeSettings(Settings settings)
		{
			return JsonUtility.ToJson(settings);
		}
	}
}
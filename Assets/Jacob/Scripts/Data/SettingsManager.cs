using System;
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
			try
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
			catch (Exception e)
			{
				Debug.LogError($"An error occured while trying to read the Settings File. " +
				               "Default values will now apply.\n" +
				               $"This is the Exception that was thrown: {e}");

				return new Settings();
			}
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
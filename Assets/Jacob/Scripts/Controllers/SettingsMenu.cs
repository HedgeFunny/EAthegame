using System;
using Jacob.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Jacob.Scripts.Controllers
{
	public class SettingsMenu : MonoBehaviour
	{
		public Action OnBack;
		public Slider volume;
		public Toggle smoothOutCamera;

		private void Awake()
		{
			InitializeVolume.SetVolume();
			volume.value = SettingsManager.Settings.Volume;
			smoothOutCamera.isOn = SettingsManager.Settings.SmoothCamera;
		}

		public void VolumeChanged(float value)
		{
			var newSettings = SettingsManager.Settings;
			newSettings.Volume = value;
			SettingsManager.Settings = newSettings;
			InitializeVolume.SetVolume();
		}

		public void SmoothOutCameraChanged(bool value)
		{
			var newSettings = SettingsManager.Settings;
			newSettings.SmoothCamera = value;
			SettingsManager.Settings = newSettings;
			Cam.Instance.SmoothCamera = value;
		}

		public void Back()
		{
			OnBack?.Invoke();
			Destroy(gameObject);
		}
	}
}
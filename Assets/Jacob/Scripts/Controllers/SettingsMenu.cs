using System;
using Jacob.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Jacob.Scripts.Controllers
{
	public class SettingsMenu : MonoBehaviour
	{
		public Action OnBack;
		public Slider slider;

		private void Awake()
		{
			InitializeVolume.SetVolume();
			slider.value = SettingsManager.Settings.Volume;
		}

		public void VolumeChanged(float volume)
		{
			SettingsManager.Settings = new Settings
			{
				Volume = volume
			};
			InitializeVolume.SetVolume();
		}
		
		public void Back()
		{
			OnBack?.Invoke();
			Destroy(gameObject);
		}
	}
}
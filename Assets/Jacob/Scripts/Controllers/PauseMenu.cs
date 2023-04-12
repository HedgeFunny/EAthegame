using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jacob.Scripts.Controllers
{
	public class PauseMenu : MonoBehaviour
	{
		public static bool Initialized;
		private GameObject _menu;
		
		private void Awake()
		{
			Time.timeScale = 0;
			_menu = transform.Find("Menu").gameObject;
			Initialized = true;
		}

		public void Resume()
		{
			Time.timeScale = 1;
			Initialized = false;
			Destroy(gameObject);
		}

		public void Settings()
		{
			var settingsPrefab = Resources.Load<GameObject>("Settings Menu");
			DisablePauseMenu();
			var settingsObject = Instantiate(settingsPrefab, settingsPrefab.transform.position,
				settingsPrefab.transform.rotation);
			settingsObject.transform.SetParent(transform);
			settingsObject.GetComponent<SettingsMenu>().OnBack += OnSettingsBack;
		}

		private void DisablePauseMenu()
		{
			_menu.SetActive(false);
		}

		private void EnablePauseMenu()
		{
			_menu.SetActive(true);
		}
		
		private void OnSettingsBack()
		{
			EnablePauseMenu();
		}

		public void Quit()
		{
			Time.timeScale = 1;
			SceneManager.LoadScene("TitleScreen");
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using Jacob.Scripts.Data;
using TMPro;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class DebugMenu : MonoBehaviour
	{
		[NonSerialized] public List<DebugAd> Ads;
		public static bool Initialized;

		private TMP_Dropdown _dropdown;
		private Dictionary<int, GameObject> _indexToGameObject;
		private int _selectedOption;
		private GameManager _gameManager;

		public void InitializeMenu()
		{
			Time.timeScale = 0;
			Initialized = true;
			_dropdown = transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
			_indexToGameObject = new Dictionary<int, GameObject>();

			for (var i = 0; i < Ads.Count; i++)
			{
				_indexToGameObject.Add(i, Ads[i].adObject);
			}

			var optionData = Ads.Select(ad => new TMP_Dropdown.OptionData { text = ad.adName }).ToList();
			_dropdown.options = optionData;
			_gameManager = GameManager.Get();
		}

		private void Update()
		{
			DebugKeyCheck();
		}

		private void DebugKeyCheck()
		{
			if (!Input.GetKeyDown(KeyCode.O)) return;
			Exit();
		}

		public void Exit()
		{
			Time.timeScale = 1;
			Initialized = false;
			Destroy(gameObject);
		}

		public void SetDropdownOption(int option)
		{
			if (_indexToGameObject.ContainsKey(option))
			{
				_selectedOption = option;
			}
		}

		public void LoadLevel()
		{
			if (Ads.Count == 0) return;
			_gameManager.CurrentlyActiveScene.SetActive(false);
			_indexToGameObject[_selectedOption].gameObject.SetActive(true);
			Exit();
		}
	}
}
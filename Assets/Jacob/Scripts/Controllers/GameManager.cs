using System;
using System.Collections.Generic;
using Jacob.Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
	public class GameManager : MonoBehaviour
	{
		private const string HealthComponentError =
			"You don't have a Health Component defined in the HealthSystem property on the GameManager.";

		private const string HealthComponentWarning =
			"You don't have a Health Component defined in the HealthSystem property on the GameManager. " +
			"GameManager Health functionality will not work.";

		public Health HealthSystem
		{
			get
			{
				if (!healthComponent)
				{
					throw new NullReferenceException(HealthComponentError);
				}

				return healthComponent;
			}
		}

		public GameManagerReturnType returnType;
		public UnityEvent<float> whenMoneyChangesFloat;
		public UnityEvent<string> whenMoneyChangesString;

		[NonSerialized] public GameObject CurrentlyActiveScene;
		[NonSerialized] public GameObject MainPlayer;

		public HealthSystem Health
		{
			get
			{
				if (_healthSystem == null)
				{
					throw new NullReferenceException(HealthComponentError);
				}

				return _healthSystem;
			}
			private set => _healthSystem = value;
		}

		public CashSystem Cash;

		private GameObject _pauseMenu;
		private GameObject _debugMenu;
		private List<DebugAd> _ads;
		[SerializeField] public Health healthComponent;
		private HealthSystem _healthSystem;

		private void Awake()
		{
			InitializeStats();
			_pauseMenu = Resources.Load<GameObject>("Pause Menu");
			_debugMenu = Resources.Load<GameObject>("Debug Menu");
			InitializeVolume.SetVolume();
			_ads = FindObjectOfType<AdList>()?.ads;
		}

		private void Update()
		{
			PauseMenuCheck();
			DebugMenuCheck();
		}

		private void InitializeStats()
		{
			// Re-export the Health components HealthSystem.
			try
			{
				Health = HealthSystem.HealthSystem;
			}
			catch (NullReferenceException)
			{
				Debug.LogWarning(HealthComponentWarning);
			}

			Cash = new CashSystem
			{
				SetAction = SetAction
			};
		}

		private void SetAction()
		{
			switch (returnType)
			{
				case GameManagerReturnType.Float:
					whenMoneyChangesFloat.Invoke(Cash.Money);
					break;
				case GameManagerReturnType.String:
					whenMoneyChangesString.Invoke($"Money: ${Cash.Money.ToString()}");
					break;
			}
		}

		private void PauseMenuCheck()
		{
			if (!Input.GetKeyDown(KeyCode.P)) return;
			if (PauseMenu.Initialized) return;
			Instantiate(_pauseMenu, _pauseMenu.transform.position, _pauseMenu.transform.rotation);
		}

		private void DebugMenuCheck()
		{
			if (!Input.GetKeyDown(KeyCode.O)) return;
			if (DebugMenu.Initialized) return;
			if (!CurrentlyActiveScene) return;
			if (_ads == null) return;
			var debugMenu = Instantiate(_debugMenu, _debugMenu.transform.position, _debugMenu.transform.rotation);
			if (!debugMenu.TryGetComponent<DebugMenu>(out var debug)) return;
			debug.Ads = _ads;
			debug.InitializeMenu();
		}

		/// <summary>
		/// Get the active GameManager. Adapts to the name of the GameObject your GameManager is on.
		/// </summary>
		/// <returns>The currently active GameManager object.</returns>
		public static GameManager Get()
		{
			return FindObjectOfType<GameManager>();
		}

		public void SetActiveScene(GameObject activeScene)
		{
			CurrentlyActiveScene = activeScene;
		}

		public void ClearActiveScene()
		{
			CurrentlyActiveScene = null;
		}
	}
}
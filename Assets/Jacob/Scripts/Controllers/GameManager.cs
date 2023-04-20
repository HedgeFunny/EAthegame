using System;
using System.Collections.Generic;
using Jacob.Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
	public class GameManager : MonoBehaviour
	{
		/// <summary>
		/// The Maximum amount of Health your player should have. When you start the game, your Health will bind to
		/// this.
		/// </summary>
		[Header("Health Properties")] public double maxHealth;

		public UnityEvent whenYouDie;

		[HideInInspector] public GameManagerReturnType returnType;
		[SerializeField] [HideInInspector] public UnityEvent<float> whenMoneyChangesFloat;
		[SerializeField] [HideInInspector] public UnityEvent<string> whenMoneyChangesString;
		[NonSerialized] public GameObject CurrentlyActiveScene;
		public List<DebugAd> ads;

		internal HealthSystem Health;
		internal CashSystem Cash;

		/// <summary>
		/// The name of the GameObject the GameManager is on.
		/// </summary>
		private static string _gameManagerName;

		private GameObject _pauseMenu;
		private GameObject _debugMenu;
		

		private void Awake()
		{
			_gameManagerName = gameObject.name;
			InitializeStats();
			_pauseMenu = Resources.Load<GameObject>("Pause Menu");
			_debugMenu = Resources.Load<GameObject>("Debug Menu");
			InitializeVolume.SetVolume();
		}

		private void Update()
		{
			PauseMenuCheck();
			DebugMenuCheck();
		}

		private void InitializeStats()
		{
			Health = new HealthSystem(maxHealth)
			{
				WhenYouDie = () => whenYouDie.Invoke()
			};
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
			var debugMenu = Instantiate(_debugMenu, _debugMenu.transform.position, _debugMenu.transform.rotation);
			if (!debugMenu.TryGetComponent<DebugMenu>(out var debug)) return;
			debug.Ads = ads;
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
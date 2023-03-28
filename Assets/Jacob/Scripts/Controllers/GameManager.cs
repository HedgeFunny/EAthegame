using System;
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

		/// <summary>
		/// A Health stat.
		/// </summary>
		internal double Health { get; private set; }

		internal float Money
		{
			get => _money;
			private set
			{
				_money = value;
				switch (returnType)
				{
					case GameManagerReturnType.Float:
						whenMoneyChangesFloat.Invoke(_money);
						break;
					case GameManagerReturnType.String:
						whenMoneyChangesString.Invoke($"Money: {_money.ToString()}");
						break;
				}
			}
		}


		/// <summary>
		/// The name of the GameObject the GameManager is on.
		/// </summary>
		private static string _gameManagerName;

		private float _money;

		private void Awake()
		{
			_gameManagerName = gameObject.name;
			InitializeStats();
		}

		private void InitializeStats()
		{
			Health = maxHealth;
		}

		/// <summary>
		/// Add some Health to the Health stat. Will clamp to the maxHealth value as you can't go over your maxHealth.
		/// </summary>
		/// <param name="amountOfHealth">The amount of Health you want to add.</param>
		public void AddHealth(double amountOfHealth)
		{
			Health += Math.Clamp(amountOfHealth, 0, maxHealth);
			if (Health <= 0) Kill();
		}

		/// <summary>
		/// Subtract some Health to the Health stat. Will clamp to the maxHealth value as you can't go under your maxHealth.
		/// </summary>
		/// <param name="amountOfHealth"></param>
		public void SubtractHealth(double amountOfHealth)
		{
			Health -= Math.Clamp(amountOfHealth, 0, maxHealth);
			if (Health <= 0) Kill();
		}

		/// <summary>
		/// When you completely run out of Health.
		/// Runs a UnityEvent so you can run whatever you want when you run out of Health.
		/// </summary>
		public void Kill()
		{
			if (Health != 0) Health = 0;
			whenYouDie.Invoke();
		}

		/// <summary>
		/// Adds money based on the number you give to the method.
		/// </summary>
		/// <param name="money">The money you want to add.</param>
		public void AddMoney(float money) => Money += money;

		/// <summary>
		/// Subtracts money based on the number you give to the Method. Returns 0 if your money is 0 and it doesn't
		/// subtract more.
		/// </summary>
		/// <param name="money">The money you want to subtract.</param>
		public void SubtractMoney(float money)
		{
			Money = Money < 0 ? Money -= money : 0;
		}

		/// <summary>
		/// Sets money to the number specified in the method.
		/// </summary>
		/// <param name="money">The number you want to set the money to.</param>
		public void SetMoney(float money) => Money = money;

		/// <summary>
		/// Bankrupt your money.
		/// </summary>
		public void Bankrupt() => SetMoney(0);

		/// <summary>
		/// Get the active GameManager. Adapts to the name of the GameObject your GameManager is on.
		/// </summary>
		/// <returns>The currently active GameManager object.</returns>
		public static GameManager Get()
		{
			return GameObject.Find(_gameManagerName).GetComponent<GameManager>();
		}
	}
}
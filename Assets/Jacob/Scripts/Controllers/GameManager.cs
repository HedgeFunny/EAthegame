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

		internal HealthSystem Health;
		internal CashSystem Cash;

		/// <summary>
		/// The name of the GameObject the GameManager is on.
		/// </summary>
		private static string _gameManagerName;

		private void Awake()
		{
			_gameManagerName = gameObject.name;
			InitializeStats();
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

		

		/// <summary>
		/// Get the active GameManager. Adapts to the name of the GameObject your GameManager is on.
		/// </summary>
		/// <returns>The currently active GameManager object.</returns>
		public static GameManager Get()
		{
			return FindObjectOfType<GameManager>();
		}
	}
}
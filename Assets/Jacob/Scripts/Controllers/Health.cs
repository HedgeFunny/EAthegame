using Jacob.Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
	public class Health: MonoBehaviour, IHealth
	{
		public double maxHealth;
		public UnityEvent whenYouDie;
		public HealthSystem HealthSystem;

		public double HealthPoints
		{
			get => HealthSystem.Health;
			set => HealthSystem.SetHealth(value);
		}

		private void Awake()
		{
			HealthSystem = new HealthSystem(maxHealth)
			{
				WhenYouDie = () => whenYouDie?.Invoke()
			};
		}

		public void AddHealth(double amountOfHealth)
		{
			HealthSystem.AddHealth(amountOfHealth);
		}

		public void SubtractHealth(double amountOfHealth)
		{
			HealthSystem.SubtractHealth(amountOfHealth);
		}

		public void SetHealth(double amountOfHealth)
		{
			HealthSystem.SetHealth(amountOfHealth);
		}

		public void Kill()
		{
			HealthSystem.Kill();
		}
	}
}
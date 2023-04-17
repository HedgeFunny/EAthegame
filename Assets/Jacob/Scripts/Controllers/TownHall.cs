using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class TownHall: MonoBehaviour
	{
		public double maxHealth;
		public HealthSystem Health;
		public string tagToCheck;
		public double healthToSubtract;

		private void Awake()
		{
			Health = new HealthSystem(maxHealth)
			{
				WhenYouDie = WhenYouDie
			};
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.CompareTag(tagToCheck))
			{
				Health.SubtractHealth(healthToSubtract);
				print(Health.Health);
			}
		}

		private void WhenYouDie()
		{
			gameObject.SetActive(false);
		}
	}
}
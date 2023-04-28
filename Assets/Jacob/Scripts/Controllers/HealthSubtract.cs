using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class HealthSubtract : MonoBehaviour
	{
		public bool useGameManagerHealth;
		public Health healthSystem;
		public double healthToSubtract;

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (!col.collider.CompareTag("Player")) return;
			
			if (useGameManagerHealth)
			{
				GameManager.Get().Health.SubtractHealth(healthToSubtract);
			}
			else
			{
				healthSystem.SubtractHealth(healthToSubtract);
			}
		}
	}
}
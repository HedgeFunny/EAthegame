using System;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
    public class HealthSubtract : MonoBehaviour
    {
        public double healthToSubtract;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Cannon"))
            {
                GameManager.Get().Health.SubtractHealth(healthToSubtract);
            }

            if (col.collider.CompareTag("GoldStorage"))
            {
                GameManager.Get().Health.SubtractHealth(healthToSubtract);
            }

            if (col.collider.CompareTag("ElixirStorage"))
            {
                GameManager.Get().Health.SubtractHealth(healthToSubtract);
            }

            if (col.collider.CompareTag("TownHall"))
            {
                GameManager.Get().Health.SubtractHealth(healthToSubtract);
            }
        }
    }
}

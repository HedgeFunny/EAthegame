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
            if (col.collider.CompareTag("Player"))
            {
                GameManager.Get().Health.SubtractHealth(healthToSubtract);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
    [RequireComponent(typeof(Collider2D))]
    public class OnCollisionEnter : MonoBehaviour
    {
        public UnityEvent onCollisionEnter;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                onCollisionEnter?.Invoke();
            }
        }
    }
}

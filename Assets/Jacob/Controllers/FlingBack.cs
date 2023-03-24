using UnityEngine;

namespace Jacob.Controllers
{
    [RequireComponent(typeof(Collider2D))]
    public class FlingBack : MonoBehaviour
    {
        public float metersToFling;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            var player = col.GetComponent<Player>();
            player.FlingPlayer(Vector2.left, metersToFling);
        }
    }
}

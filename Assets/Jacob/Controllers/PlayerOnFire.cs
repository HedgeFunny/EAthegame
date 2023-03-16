using UnityEngine;

namespace Jacob.Controllers
{
    public class PlayerOnFire : MonoBehaviour
    {
        public string playerName;
        private Player _player;
        private Collider2D _collider;
        private bool _onFire;

        private void Awake()
        {
            _player = Player.Get(playerName);
            _collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (!_onFire) return;
            var hit = Physics2D.Raycast(_collider.bounds.max, Vector2.right, 0.2f);
            Debug.DrawRay(_collider.bounds.max, Vector2.right);
            if (!hit.collider) return;
            _player.FlipPlayer();
            _player.FlipHorizontalInput();
        }

        public void OnFire()
        {
            _player.moveSpeed = 12;
            _player.DisableControllingMovement();
            _player.SetHorizontalInput(1);
            _onFire = true;
        }
    }
}
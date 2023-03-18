using UnityEngine;

namespace Jacob.Controllers
{
    public class PlayerOnFire : MonoBehaviour
    {
        public string tagName;

        private Player _player;
        private Collider2D _collider;
        private bool _onFire;
        private float _originalPlayerMoveSpeed;

        private void Awake()
        {
            // Assumes that this script is on the same Object that the Player script is on.
            _player = GetComponent<Player>();

            _collider = GetComponent<Collider2D>();
            _originalPlayerMoveSpeed = _player.moveSpeed;
        }

        private void Update()
        {
            FireRaycast();
        }

        /// <summary>
        /// A raycast that fires 20cm in front of the player to check if the player hits a collider. Activates the
        /// onFire ability if the player bumps into the tag that's in the tagName string, else, flip the player in the
        /// other direction
        /// </summary>
        private void FireRaycast()
        {
            if (!_onFire) return;
            var bounds = _collider.bounds;

            var raycastOrigin = RaycastOrigin(bounds);
            var hit = Physics2D.Raycast(raycastOrigin, _player.Direction, 0.2f);
            Debug.DrawRay(raycastOrigin, _player.Direction);

            ColliderCheck(hit);
        }

        /// <summary>
        /// Draw the Raycast at the max Y position of the colliders bounding box (so the top of the collider), but
        /// draw the Raycast at either the right position of the collider (so the max x part of the collider), or the
        /// left position of the collider (so the min x part of the collider).
        /// </summary>
        /// <param name="bounds">The Bounds you want to get the origin from.</param>
        /// <returns>A Raycast origin adjusted to the Direction the player is facing.</returns>
        private Vector3 RaycastOrigin(Bounds bounds)
        {
            return _player.Direction == Vector2.right
                ? new Vector3(bounds.max.x, bounds.max.y, 0)
                : new Vector3(bounds.min.x, bounds.max.y, 0);
        }

        /// <summary>
        /// Checks if you hit the tag that's described in tagName, if not, the method flips the Player. If it finds
        /// that collider with a tag that's described in tagName, it Extinguishes the Fire and it turns on the onFire
        /// ability for the player.
        /// </summary>
        /// <param name="hit">The RaycastHit2D Object you want to check for colliders.</param>
        private void ColliderCheck(RaycastHit2D hit)
        {
            if (!hit.collider) return;

            if (hit.collider.CompareTag(tagName) && !_player.onFireAbilityUnlocked)
            {
                ExtinguishFire();
                _player.onFireAbilityUnlocked = true;
                return;
            }

            _player.FlipPlayer();
            _player.FlipHorizontalInput();
            FlipDirection();
        }

        /// <summary>
        /// Set the Player on Fire. Activates the Fire effect.
        /// </summary>
        public void SetOnFire()
        {
            _player.moveSpeed = 12;
            _player.DisableControllingMovement();
            _player.SetHorizontalInput(_player.Direction == Vector2.right ? 1 : -1);
            _onFire = true;
        }

        /// <summary>
        /// Extinguish the Fire that's on the Player. Deactivates the Fire effect.
        /// </summary>
        public void ExtinguishFire()
        {
            _onFire = false;
            _player.SetHorizontalInput(0);
            _player.moveSpeed = _originalPlayerMoveSpeed;
            _player.EnableControllingMovement();
        }

        /// <summary>
        /// Flip the Players Direction. This lets the Raycast flip.
        /// </summary>
        private void FlipDirection()
        {
            if (_player.Direction == Vector2.right)
            {
                _player.Direction = Vector2.left;
            }
            else if (_player.Direction == Vector2.left)
            {
                _player.Direction = Vector2.right;
            }
        }
    }
}
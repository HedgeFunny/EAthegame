using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Player))]
	public class PlayerWallClimbing : MonoBehaviour
	{
		private Player _player;
		internal bool TouchingWall;
		private readonly Distance _rayDistance = Distance.Centimetre(1);

		private void Awake()
		{
			_player = GetComponent<Player>();
		}

		private void ShootRaycast()
		{
			var playerBounds = _player.Collider2D.bounds;
			var extentsX = playerBounds.extents.x;
			var extentsY = playerBounds.extents.y;
			var extentsOffset = YOnly(extentsY) - YOnly(Distance.Centimetre(15));

			var directionOffset = XOnly(extentsX + Distance.Centimetre(1));
			var direction = _player.Direction == Vector2.right
				? playerBounds.center + directionOffset - extentsOffset
				: playerBounds.center - directionOffset - extentsOffset;

			var hit = Physics2D.Raycast(direction, _player.Direction, _rayDistance);
			Debug.DrawRay(direction, _player.Direction * _rayDistance, Color.red);

			if (!hit)
			{
				TouchingWall = false;
				return;
			}
			
			print(hit.collider.name);
			TouchingWall = hit.collider.CompareTag("Ground");
		}

		internal void WallClimbingCheck()
		{
			if (!_player.enableWallClimbing) return;
			ShootRaycast();
			if (!TouchingWall) return;
			_player.Rigidbody.gravityScale = 0;
			_player.Rigidbody.velocity = YOnly(Mathf.Abs(_player.HorizontalInput) * _player.wallClimbingSpeed);
		}

		private static Vector3 XOnly(float xAxis)
		{
			return new Vector3(xAxis, 0, 0);
		}

		private static Vector3 YOnly(float yAxis)
		{
			return new Vector3(0, yAxis, 0);
		}
	}
}
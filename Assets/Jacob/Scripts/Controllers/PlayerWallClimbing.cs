using System;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	[RequireComponent(typeof(Player))]
	public class PlayerWallClimbing : MonoBehaviour
	{
		private Player _player;
		private bool _touchingWall;

		private void Awake()
		{
			_player = GetComponent<Player>();
		}

		private void ShootRaycast()
		{
			var directionOffset = new Vector3(_player.Collider2D.bounds.extents.x + 0.01f, 0, 0);
			var direction = _player.Direction == Vector2.right
				? _player.Collider2D.bounds.center + directionOffset
				: _player.Collider2D.bounds.center - directionOffset;
			var hit = Physics2D.Raycast(direction, _player.Direction, 1);
			Debug.DrawRay(direction, _player.Direction * 1, Color.red);
			
			if (!hit) return;
			print(hit.collider.name);
			if (hit.collider.CompareTag("Ground"))
			{
				print("You have ran into a wall.");
				print(_player.Collider2D.bounds.extents.x);
			}
		}

		internal void WallClimbingCheck()
		{
			if (!_player.enableWallClimbing) return;
			ShootRaycast();
		}
	}
}
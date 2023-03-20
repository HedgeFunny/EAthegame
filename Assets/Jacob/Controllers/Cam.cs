using Jacob.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Jacob.Controllers
{
	public class Cam : MonoBehaviour
	{
		public Transform followedObject;
		public TilemapCollider2D tilemap;

		private Camera _camera;
		private float _beginningOfLevel;
		private float _endingOfLevel;
		private float _horizontalSize;
		private TilemapBounds _tilemapBounds;

		private void Awake()
		{
			_camera = GetComponent<Camera>()!;
			CalculateHorizontalSize();
		}

		private void Update()
		{
			transform.position = new Vector3(GetClampedHorizontalPosition(), 0, -10);
		}

		private float GetClampedHorizontalPosition()
		{
			return Mathf.Clamp(followedObject.position.x, _tilemapBounds.Left,
				_tilemapBounds.Right < _horizontalSize ? _tilemapBounds.Left : _tilemapBounds.Right);
		}

		private static float GetHorizontalSize(Camera camera)
		{
			return camera.orthographicSize * camera.aspect;
		}

		private void CalculateHorizontalSize()
		{
			_horizontalSize = GetHorizontalSize(_camera);
			_tilemapBounds = new TilemapBounds(tilemap.bounds, _horizontalSize);
		}
	}
}
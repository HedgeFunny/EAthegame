using Jacob.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Jacob.Controllers
{
	public class Cam : MonoBehaviour
	{
		public Transform followedObject;
		public TilemapCollider2D tilemap;
		public bool clampVerticalPosition;

		private Camera _camera;
		private float _beginningOfLevel;
		private float _endingOfLevel;
		private float _horizontalSize;
		private TilemapBounds _tilemapBounds;

		private void Awake()
		{
			_camera = GetComponent<Camera>()!;
			CalculateSize();
		}

		private void Update()
		{
			transform.position = new Vector3(GetClampedHorizontalPosition(),
				clampVerticalPosition ? GetClampedVerticalPosition() : 0, -10);
		}

		private float GetClampedHorizontalPosition()
		{
			return Mathf.Clamp(followedObject.position.x, _tilemapBounds.Left,
				_tilemapBounds.Right < _horizontalSize ? _tilemapBounds.Left : _tilemapBounds.Right);
		}

		private float GetClampedVerticalPosition()
		{
			return Mathf.Clamp(followedObject.position.y,
				_tilemapBounds.Down > _camera.orthographicSize ? _tilemapBounds.Up : _tilemapBounds.Down,
				_tilemapBounds.Up);
		}

		private static float GetHorizontalSize(Camera camera)
		{
			return camera.orthographicSize * camera.aspect;
		}

		private void CalculateSize()
		{
			_horizontalSize = GetHorizontalSize(_camera);
			_tilemapBounds = new TilemapBounds(tilemap.bounds, _horizontalSize, _camera.orthographicSize);
		}

		/// <summary>
		/// Extends the Camera's view with another Collider bounding box.
		/// </summary>
		/// <param name="tileMapCollider">The Collider you want to extend the view to.</param>
		public void ExtendCameraView(TilemapCollider2D tileMapCollider)
		{
			var size = tileMapCollider.bounds.size + tilemap.bounds.size;
			var center = size / 2;
			print(size);
			var bound = new Bounds(center, size);
			_tilemapBounds = new TilemapBounds(bound, _horizontalSize, _camera.orthographicSize);
		}
	}
}
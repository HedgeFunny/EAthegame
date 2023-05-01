using UnityEngine;

namespace Jacob.Scripts.Data.Clamp
{
	public class TilemapClamping : Clamping, ICameraClamping
	{
		private TilemapBounds _tilemapBounds;
		private readonly Bounds _bounds;
		private readonly Transform _followedObject;

		public TilemapClamping(Camera camera, Bounds bounds, Transform followedObject)
		{
			Camera = camera;
			_bounds = bounds;
			_followedObject = followedObject;
		}

		public void Awake()
		{
			CalculateSize(_bounds);
		}

		public float GetClampedHorizontalPosition()
		{
			return Mathf.Clamp(_followedObject.position.x, _tilemapBounds.Left,
				_tilemapBounds.Right < horizontalSize ? _tilemapBounds.Left : _tilemapBounds.Right);
		}

		public float GetClampedVerticalPosition()
		{
			return Mathf.Clamp(_followedObject.position.y,
				_tilemapBounds.Down > Camera.orthographicSize ? _tilemapBounds.Up : _tilemapBounds.Down,
				_tilemapBounds.Up);
		}

		private void CalculateSize(Bounds bounds)
		{
			_tilemapBounds = new TilemapBounds(bounds, horizontalSize, Camera.orthographicSize);
		}
	}
}
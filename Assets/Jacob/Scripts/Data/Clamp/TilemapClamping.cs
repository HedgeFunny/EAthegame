using System;
using UnityEngine;

namespace Jacob.Scripts.Data.Clamp
{
	public class TilemapClamping : Clamping, ICameraClamping
	{
		private TilemapBounds _tilemapBounds;
		private readonly Bounds _bounds;
		private readonly Transform _followedObject;
		private Action _resolutionChanged;
		private (int width, int height) _currentResolution;

		public TilemapClamping(Camera camera, Bounds bounds, Transform followedObject)
		{
			Camera = camera;
			_bounds = bounds;
			_followedObject = followedObject;
		}

		public void Awake()
		{
			_resolutionChanged += CalculateSize;
			_currentResolution = (Screen.width, Screen.height);
			CalculateSize();
		}

		public void Update()
		{
			if (!(Screen.width, Screen.height).Equals(_currentResolution)) _resolutionChanged.Invoke();
		}

		public void OnDestroy()
		{
			_resolutionChanged -= CalculateSize;
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

		private void CalculateSize()
		{
			_tilemapBounds = new TilemapBounds(_bounds, horizontalSize, Camera.orthographicSize);
			_currentResolution = (Screen.width, Screen.height);
		}
	}
}
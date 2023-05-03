using System;
using Jacob.Scripts.Data;
using Jacob.Scripts.Data.Clamp;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Jacob.Scripts.Controllers
{
	public class Cam : ClampingProperties
	{
		public static Cam Instance;
		public Transform followedObject;

		public bool Clamp
		{
			get => clampProperty;
			set
			{
				clampProperty = value;
				ClampCheck();
			}
		}

		// Manual Coordinates
		[NonSerialized] public ManualCoordinatesData ManualCoordinatesData;

		[NonSerialized] public Camera Camera;
		[NonSerialized] public float SmoothTime = 0.35f;
		private float _horizontalSize;
		private TilemapBounds _tilemapBounds;
		private ICameraClamping _clampingInterface;
		private Vector3 _velocity = Vector3.zero;
		private Transform _currentFollowedObject;
		private Action _onFollowedObjectChange;
		internal bool SmoothCamera;

		private void Awake()
		{
			if (Instance)
				throw new InvalidOperationException("You can't have more then 1 Cam script active at once.");
			
			Instance = this;
			SmoothCamera = SettingsManager.Settings.SmoothCamera;
			_onFollowedObjectChange += FollowedObjectChange;
			Camera = GetComponent<Camera>();
			ClampCheck();
		}

		private void OnDestroy()
		{
			_clampingInterface.OnDestroy();
			Instance = null;
			_onFollowedObjectChange -= FollowedObjectChange;
		}

		private void FollowedObjectChange()
		{
			print("Followed Object has been changed.");
			_velocity = Vector3.zero;
			var targetPosition = new Vector3(followedObject.transform.position.x,
				followedObject.transform.position.y, -10);
			transform.position = targetPosition;
			_currentFollowedObject = followedObject;
		}

		private void Update()
		{
			if (!followedObject)
			{
				if (_currentFollowedObject) _currentFollowedObject = null;
				return;
			}

			if (!SmoothCamera) _velocity = Vector3.zero;
			
			if (followedObject && _currentFollowedObject && !followedObject.Equals(_currentFollowedObject))
			{
				_onFollowedObjectChange.Invoke();
			}

			if (followedObject && !_currentFollowedObject) _currentFollowedObject = followedObject;

			if (Clamp)
			{
				var targetPosition = new Vector3(_clampingInterface.GetClampedHorizontalPosition(),
					clampVerticalPosition ? _clampingInterface.GetClampedVerticalPosition() : 0, -10);

				transform.position = SmoothCamera
					? Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, SmoothTime)
					: targetPosition;
				
				_clampingInterface.Update();
			}
			else
			{
				var targetPosition = new Vector3(followedObject.transform.position.x,
					followedObject.transform.position.y, -10);

				transform.position = SmoothCamera
					? Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, SmoothTime)
					: targetPosition;
			}
		}

		public void ClampCheck()
		{
			if (!Clamp) return;

			_clampingInterface = clampingTypes switch
			{
				ClampingTypes.Tilemap => new TilemapClamping(Camera, tilemap.bounds, followedObject),
				ClampingTypes.ManualCoordinates => new ManualCoordinatesClamping(ManualCoordinatesData,
					followedObject, Camera),
				_ => throw new ArgumentOutOfRangeException()
			};

			_clampingInterface.Awake();
		}

		/// <summary>
		/// Extends the Camera's view with another Collider bounding box.
		/// </summary>
		/// <param name="tileMapCollider">The Collider you want to extend the view to.</param>
		public void ExtendCameraView(TilemapCollider2D tileMapCollider)
		{
			var bound = tilemap.bounds;
			bound.Encapsulate(tileMapCollider.bounds);
			// _CalculateSize(bound);
		}

		/// <summary>
		/// Re-calculates the Camera's view using the original tilemap bounds.
		/// </summary>
		public void RestoreCameraView()
		{
			// CalculateSize(tilemap.bounds);
		}
	}
}
using System;
using Jacob.Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Jacob.Scripts.Controllers
{
	public class Cam : MonoBehaviour
	{
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

		public bool clampVerticalPosition;
		public ClampingTypes clampingTypes;

		// Tilemap
		public TilemapCollider2D tilemap;

		// Manual Coordinates
		[NonSerialized] public ManualCoordinatesData ManualCoordinatesData;


		[NonSerialized] public Camera Camera;
		private float _horizontalSize;
		private TilemapBounds _tilemapBounds;
		private ICameraClamping _clampingInterface;
		public bool clampProperty;


		private void Awake()
		{
			Camera = GetComponent<Camera>()!;
			ClampCheck();
		}

		private void Update()
		{
			if (!followedObject) return;
			
			if (Clamp)
			{
				transform.position = new Vector3(_clampingInterface.GetClampedHorizontalPosition(),
					clampVerticalPosition ? _clampingInterface.GetClampedVerticalPosition() : 0, -10);
			}
			else
			{
				transform.position = new Vector3(followedObject.transform.position.x,
					followedObject.transform.position.y, -10);
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
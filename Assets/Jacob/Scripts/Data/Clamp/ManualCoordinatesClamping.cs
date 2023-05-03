using System;
using UnityEngine;

namespace Jacob.Scripts.Data.Clamp
{
	public class ManualCoordinatesClamping : Clamping, ICameraClamping
	{
		private const string CoordsNullError = "Unable to find a ManualCoordinates data asset. Please use the " +
		                                       "ManualCoordinatesSupport script and assign a ManualCoordinates " +
		                                       "data asset to that script.";

		private readonly ManualCoordinatesData _coords;
		private readonly Transform _followedObject;

		public ManualCoordinatesClamping(ManualCoordinatesData coords, Transform followedObject, Camera camera)
		{
			if (coords == null)
			{
				throw new NullReferenceException(CoordsNullError);
			}

			_coords = coords;
			_followedObject = followedObject;
			Camera = camera;
		}

		public void Awake()
		{
		}

		public void Update()
		{
		}

		public void OnDestroy()
		{
		}

		public float GetClampedHorizontalPosition()
		{
			return Mathf.Clamp(_followedObject.position.x, GetCameraMinX(_coords.minX), GetCameraMaxX(_coords.maxX));
		}

		public float GetClampedVerticalPosition()
		{
			return Mathf.Clamp(_followedObject.position.y, GetCameraMinY(_coords.minY), GetCameraMaxY(_coords.maxY));
		}
	}
}
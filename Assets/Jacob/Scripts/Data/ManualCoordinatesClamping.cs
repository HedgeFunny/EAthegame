using UnityEngine;

namespace Jacob.Scripts.Data
{
	public class ManualCoordinatesClamping : Clamping, ICameraClamping
	{
		private readonly ManualCoordinatesData _coords;
		private readonly Transform _followedObject;

		public ManualCoordinatesClamping(ManualCoordinatesData coords, Transform followedObject, Camera camera)
		{
			_coords = coords;
			_followedObject = followedObject;
			Camera = camera;
		}

		public void Awake()
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
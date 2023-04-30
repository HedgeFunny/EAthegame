using UnityEngine;

namespace Jacob.Scripts.Data
{
	public class Clamping
	{
		protected Camera Camera;

		protected float horizontalSize => Camera.orthographicSize * Camera.aspect;
		protected float verticalSize => Camera.orthographicSize;


		protected float GetCameraMinX(float minX)
		{
			return minX + horizontalSize;
		}

		protected float GetCameraMaxX(float maxX)
		{
			return maxX - horizontalSize;
		}

		protected float GetCameraMinY(float minY)
		{
			return minY + verticalSize;
		}

		protected float GetCameraMaxY(float maxY)
		{
			return maxY - verticalSize;
		}
	}
}
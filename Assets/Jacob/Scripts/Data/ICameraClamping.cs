using UnityEngine;

namespace Jacob.Scripts.Data
{
	public interface ICameraClamping
	{
		public void Awake();
		public float GetClampedHorizontalPosition();
		public float GetClampedVerticalPosition();
	}
}
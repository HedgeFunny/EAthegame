namespace Jacob.Scripts.Data.Clamp
{
	public interface ICameraClamping
	{
		public void Awake();
		public float GetClampedHorizontalPosition();
		public float GetClampedVerticalPosition();
	}
}
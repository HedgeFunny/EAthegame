namespace Jacob.Scripts.Data.Clamp
{
	public interface ICameraClamping
	{
		public void Awake();
		public void Update();
		public void OnDestroy();
		public float GetClampedHorizontalPosition();
		public float GetClampedVerticalPosition();
	}
}
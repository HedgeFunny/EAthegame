namespace Jacob.Scripts.Data
{
	public interface IHealth
	{
		/// <summary>
		/// Add some Health to the Health stat. Will clamp to the maxHealth value as you can't go over your maxHealth.
		/// </summary>
		/// <param name="amountOfHealth">The amount of Health you want to add.</param>
		public void AddHealth(double amountOfHealth);

		/// <summary>
		/// Subtract some Health to the Health stat. Will clamp to the maxHealth value as you can't go under your maxHealth.
		/// </summary>
		/// <param name="amountOfHealth"></param>
		public void SubtractHealth(double amountOfHealth);

		/// <summary>
		/// Sets the Health to the amount provided.
		/// </summary>
		/// <param name="amountOfHealth">A number to set the Health to.</param>
		public void SetHealth(double amountOfHealth);

		/// <summary>
		/// When you completely run out of Health.
		/// Runs a UnityEvent so you can run whatever you want when you run out of Health.
		/// </summary>
		public void Kill();
	}
}
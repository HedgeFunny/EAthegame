using System;

namespace Jacob.Scripts.Data
{
	public class HealthSystem
	{
		public HealthSystem(double maxHealth)
		{
			_maxHealth = maxHealth;
		}

		private double _maxHealth;

		public Action whenYouDie;
		
		/// <summary>
		/// A Health stat.
		/// </summary>
		internal double Health { get; private set; }

		/// <summary>
		/// Add some Health to the Health stat. Will clamp to the maxHealth value as you can't go over your maxHealth.
		/// </summary>
		/// <param name="amountOfHealth">The amount of Health you want to add.</param>
		public void AddHealth(double amountOfHealth)
		{
			Health += Math.Clamp(amountOfHealth, 0, _maxHealth);
			if (Health <= 0) Kill();
		}

		/// <summary>
		/// Subtract some Health to the Health stat. Will clamp to the maxHealth value as you can't go under your maxHealth.
		/// </summary>
		/// <param name="amountOfHealth"></param>
		public void SubtractHealth(double amountOfHealth)
		{
			Health -= Math.Clamp(amountOfHealth, 0, _maxHealth);
			if (Health <= 0) Kill();
		}

		/// <summary>
		/// When you completely run out of Health.
		/// Runs a UnityEvent so you can run whatever you want when you run out of Health.
		/// </summary>
		public void Kill()
		{
			if (Health != 0) Health = 0;
			whenYouDie.Invoke();
		}
	}
}
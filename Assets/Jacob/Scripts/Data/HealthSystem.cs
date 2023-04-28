using System;
using JetBrains.Annotations;

namespace Jacob.Scripts.Data
{
	public class HealthSystem
	{
		/// <summary>
		/// A constructor that will return a HealthSystem.
		/// </summary>
		/// <param name="maxHealth">The maximum amount of health your Object has.</param>
		public HealthSystem(double maxHealth)
		{
			_maxHealth = maxHealth;
			Health = maxHealth;
		}

		private readonly double _maxHealth;

		/// <summary>
		/// An Action that will run when you Die. Needs to be assigned to a method or nothing will happen.
		/// </summary>
		[CanBeNull] public Action WhenYouDie;
		
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
			WhenYouDie?.Invoke();
		}
	}
}
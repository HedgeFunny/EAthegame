using System;
using JetBrains.Annotations;

namespace Jacob.Scripts.Data
{
	public class HealthSystem: IHealth
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

		public void AddHealth(double amountOfHealth)
		{
			Health += Math.Clamp(amountOfHealth, 0, _maxHealth);
			if (Health <= 0) Kill();
		}

		public void SubtractHealth(double amountOfHealth)
		{
			Health -= Math.Clamp(amountOfHealth, 0, _maxHealth);
			if (Health <= 0) Kill();
		}

		public void SetHealth(double amountOfHealth) => Health = amountOfHealth;

		public void Kill()
		{
			if (Health != 0) Health = 0;
			WhenYouDie?.Invoke();
		}
	}
}
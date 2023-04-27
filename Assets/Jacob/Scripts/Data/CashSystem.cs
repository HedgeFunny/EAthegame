using System;
using JetBrains.Annotations;

namespace Jacob.Scripts.Data
{
	public class CashSystem
	{
		private float _money;
		/// <summary>
		/// Method to run when you set Money. You can set this to any method in your class by assigning that methods
		/// name to this Action.
		/// </summary>
		[CanBeNull] public Action SetAction;

		public float Money
		{
			get => _money;
			private set
			{
				_money = value;
				SetAction?.Invoke();
			}
		}

		/// <summary>
		/// Adds money based on the number you give to the method.
		/// </summary>
		/// <param name="money">The money you want to add.</param>
		public void AddMoney(float money) => Money += money;

		/// <summary>
		/// Subtracts money based on the number you give to the Method. Returns 0 if your money is 0 and it doesn't
		/// subtract more.
		/// </summary>
		/// <param name="money">The money you want to subtract.</param>
		public void SubtractMoney(float money)
		{
			Money = Money > 0 ? Money -= money : 0;
		}

		/// <summary>
		/// Sets money to the number specified in the method.
		/// </summary>
		/// <param name="money">The number you want to set the money to.</param>
		public void SetMoney(float money) => Money = money;

		/// <summary>
		/// Bankrupt your money.
		/// </summary>
		public void Bankrupt() => SetMoney(0);
	}
}
﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Controllers
{
	public class GameManager : MonoBehaviour
	{
		/// <summary>
		/// The Maximum amount of Health your player should have. When you start the game, your Health will bind to
		/// this.
		/// </summary>
		[Header("Health Properties")] public double maxHealth;

		public UnityEvent whenYouDie;

		/// <summary>
		/// A Health stat.
		/// </summary>
		internal double Health { get; private set; }

		/// <summary>
		/// The name of the GameObject the GameManager is on.
		/// </summary>
		private static string _gameManagerName;

		private void Awake()
		{
			_gameManagerName = gameObject.name;
			InitializeStats();
		}

		private void InitializeStats()
		{
			Health = maxHealth;
		}

		/// <summary>
		/// Add some Health to the Health stat. Will clamp to the maxHealth value as you can't go over your maxHealth.
		/// </summary>
		/// <param name="amountOfHealth">The amount of Health you want to add.</param>
		public void AddHealth(double amountOfHealth)
		{
			Health += Math.Clamp(amountOfHealth, 0, maxHealth);
			if (Health <= 0) Kill();
		}

		/// <summary>
		/// Subtract some Health to the Health stat. Will clamp to the maxHealth value as you can't go under your maxHealth.
		/// </summary>
		/// <param name="amountOfHealth"></param>
		public void SubtractHealth(double amountOfHealth)
		{
			Health -= Math.Clamp(amountOfHealth, 0, maxHealth);
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

		/// <summary>
		/// Get the active GameManager. Adapts to the name of the GameObject your GameManager is on.
		/// </summary>
		/// <returns>The currently active GameManager object.</returns>
		public static GameManager Get()
		{
			return GameObject.Find(_gameManagerName).GetComponent<GameManager>();
		}
	}
}
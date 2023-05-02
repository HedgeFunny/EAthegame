using System;
using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class HealthBar : MonoBehaviour
	{
		public Animator animator;
		public bool useGameManagerHealthSystem;
		/// <summary>
		/// This is an internal property, for assigning a HealthSystem to your HealthBar.
		/// Use <code>CurrentHealthSystem</code> instead if you want to get the HealthSystem of the HealthBar
		/// programatically.
		/// </summary>
		public Health healthSystem;
		public HealthBarAnimationType animationType;
		
		private float _frames;
		private GameManager _gameManager;

		[NonSerialized] public Health CurrentHealthSystem;

		private void Awake()
		{
			animator.speed = 0;
			if (!healthSystem && !useGameManagerHealthSystem)
				throw new NullReferenceException("The HealthBar script requires a Health component to function.");
			var clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
			_frames = clip.frameRate * clip.length - 1;
		}

		private void Start()
		{
			_gameManager = GameManager.Get();
			
			if (useGameManagerHealthSystem && !_gameManager)
			{
				throw new NullReferenceException("A GameManager is required to use the GameManager Health System");
			}

			CurrentHealthSystem = useGameManagerHealthSystem ? _gameManager.HealthSystem : healthSystem;
		}

		private void Update()
		{
			WatchHealth();
		}

		private void WatchHealth()
		{
			if (!CurrentHealthSystem) return;
			var percentage = Math.Clamp(CurrentHealthSystem.HealthPoints, 0, CurrentHealthSystem.HealthPoints) /
			                 CurrentHealthSystem.maxHealth;
			var frame = Math.Round(_frames * percentage);
			PlayAtFrame((int)frame);
		}

		private void PlayAtFrame(int frame)
		{
			var normalizedTime = frame / _frames - 0.01f;
			var stateName = animationType switch
			{
				HealthBarAnimationType.WorldSpace => "HealthBarWorldSpace",
				HealthBarAnimationType.UISpace => "HealthBar",
				_ => throw new ArgumentOutOfRangeException()
			};
			animator.Play(stateName, 0, Math.Clamp(normalizedTime, 0, normalizedTime < 0 ? 0 : normalizedTime));
		}
	}
}
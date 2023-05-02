using System;
using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class HealthBar : MonoBehaviour
	{
		public Animator animator;
		public bool useGameManagerHealthSystem;
		public Health healthSystem;
		public HealthBarAnimationType animationType;
		
		private float _frames;
		private GameManager _gameManager;

		private Health _currentHealthSystem;

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

			_currentHealthSystem = useGameManagerHealthSystem ? _gameManager.HealthSystem : healthSystem;
		}

		private void Update()
		{
			WatchHealth();
		}

		private void WatchHealth()
		{
			if (!healthSystem) return;
			var percentage = Math.Clamp(healthSystem.HealthPoints, 0, healthSystem.HealthPoints) /
			                 healthSystem.maxHealth;
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
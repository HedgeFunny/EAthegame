using System;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class HealthBar : MonoBehaviour
	{
		public Animator animator;
		public GameManager gameManager;
		private float _frames;

		private void Awake()
		{
			animator.speed = 0;
			var clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
			_frames = clip.frameRate * clip.length - 1;
		}

		private void Update()
		{
			WatchHealth();
		}

		private void WatchHealth()
		{
			var percentage = Math.Clamp(gameManager.Health.Health, 0, gameManager.Health.Health) / gameManager.maxHealth;
			var frame = Math.Round(_frames * percentage);
			PlayAtFrame((int)frame);
		}

		private void PlayAtFrame(int frame)
		{
			var normalizedTime = frame / _frames - 0.01f;
			animator.Play("HealthBar", 0, Math.Clamp(normalizedTime, 0, normalizedTime));
		}
	}
}
using System;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class HealthBar : MonoBehaviour
	{
		public Animator animator;
		[NonSerialized] public GameManager GameManager;
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
			if (!GameManager) return;
			var percentage = Math.Clamp(GameManager.Health.Health, 0, GameManager.Health.Health) / GameManager.maxHealth;
			var frame = Math.Round(_frames * percentage);
			PlayAtFrame((int)frame);
		}

		private void PlayAtFrame(int frame)
		{
			var normalizedTime = frame / _frames - 0.01f;
			animator.Play("HealthBar", 0, Math.Clamp(normalizedTime, 0, normalizedTime < 0 ? 0 : normalizedTime));
		}
	}
}
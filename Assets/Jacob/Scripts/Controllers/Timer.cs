using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Jacob.Scripts.Controllers
{
	public class Timer : MonoBehaviour
	{
		public int seconds;
		public UnityEvent onOutOfTime;

		private Animator _animator;
		private TextMeshProUGUI _timeText;
		private int _secondsLeft;
		private float _frames;
		private float _secondsPerFrame;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_animator.speed = 0;
			_timeText = transform.Find("Time").GetComponent<TextMeshProUGUI>();
			GetFrames();
			StartCoroutine(Animation());
		}

		private void GetFrames()
		{
			var clip = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;
			_frames = clip.frameRate * clip.length;
			_secondsPerFrame = 1 / _frames;
		}

		private void SetTimeText()
		{
			_timeText.text = _secondsLeft.ToString();
			if (!_timeText.gameObject.activeSelf)
			{
				_timeText.gameObject.SetActive(true);
			}
		}

		private void PlayAtFrame(int frame)
		{
			var normalizedTime = frame / _frames;
			_animator.Play("Timer", 0, normalizedTime);
		}

		private IEnumerator Animation()
		{
			_secondsLeft = seconds;
			SetTimeText();
			while (_secondsLeft > 0)
			{
				for (var i = 0; i < _frames; i++)
				{
					PlayAtFrame(i);
					yield return new WaitForSeconds(_secondsPerFrame);
				}
				_secondsLeft--;
				SetTimeText();
			}
			onOutOfTime?.Invoke();
		}
	}
}
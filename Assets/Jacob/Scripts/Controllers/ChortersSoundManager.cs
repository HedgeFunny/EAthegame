using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class ChortersSoundManager: SoundManager
	{
		// Audio Clips are defined here
		public AudioClip crunchNoise;

		public void PlayCrunchNoise()
		{
			if (!crunchNoise) throw AudioNullException("crunchNoise");
			AudioSource.PlayOneShot(crunchNoise);
		}
	}
}
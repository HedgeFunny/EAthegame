using Jacob.Scripts.Data;
using UnityEngine;

namespace Jacob.Scripts.Controllers
{
	public class TwoTongueSoundManager : SoundManager
	{
		// Audio Clips are defined here
		public AudioClip doorSlam;
		public AudioClip owlNoise;
		public AudioClip shotgunNoise;

		public void PlayDoorSlam()
		{
			// If doorSlam is not defined, throw an Exception.
			if (!doorSlam) throw AudioNullException("doorSlam");
			AudioSource.PlayOneShot(doorSlam);
		}

		public void PlayOwlNoise()
		{
			// If owlNoise is not defined, throw an Exception.
			if (!owlNoise) throw AudioNullException("doorSlam");
			AudioSource.PlayOneShot(owlNoise);
		}

		public void PlayShotgunNoise()
		{
			// If shotgunNoise is not defined, throw an Exception.
			if (!shotgunNoise) throw AudioNullException("shotgunNoise");
			AudioSource.PlayOneShot(shotgunNoise);
		}
	}
}
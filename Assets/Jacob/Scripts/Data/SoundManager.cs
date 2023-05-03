using System;
using UnityEngine;

namespace Jacob.Scripts.Data
{
	/// <summary>
	/// A class with some nice to use methods and boilerplate already done for managing Sound Effects.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public abstract class SoundManager: MonoBehaviour
	{
		protected AudioSource AudioSource;
		public AudioClip celebrationNoise;
		
		private void Awake()
		{
			// Get the AudioSource. This should always succeed as this class requires the AudioSource component.
			AudioSource = GetComponent<AudioSource>();
		}

		/// <summary>
		/// Method that returns an exception that explains that you don't have an AudioClip defined.
		/// </summary>
		/// <param name="audioClipName">The name of the AudioClip.</param>
		/// <returns>An exception that you can throw.</returns>
		protected static NullReferenceException AudioNullException(string audioClipName)
		{
			return new NullReferenceException(GenerateErrorMessage(audioClipName));
		}

		/// <summary>
		/// A method that generates the Error Message, customized by the audioClipName parameter.
		/// </summary>
		/// <param name="audioClipName">The name of the AudioClip.</param>
		/// <returns>A string that contains an Error Message.</returns>
		private static string GenerateErrorMessage(string audioClipName) =>
			$"The AudioClip {audioClipName} has not been defined. " +
			"Please define it in the inspector so this sound effect can play.";
		
		/// <summary>
		/// A method that plays a Celebration Noise.
		/// </summary>
		/// <exception cref="NullReferenceException">Throws when you don't have the Celebration Noise defined</exception>
		public void PlayCelebrationNoise()
		{
			// If celebrationNoise is not defined, throw an Exception.
			if (!celebrationNoise) throw AudioNullException("celebrationNoise");
			AudioSource.PlayOneShot(celebrationNoise);
		}
	}
}
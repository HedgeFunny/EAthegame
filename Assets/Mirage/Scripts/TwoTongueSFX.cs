using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoTongueSFX : MonoBehaviour
{
    public AudioClip owlSoundEffect;
    public AudioClip doorSoundEffect;
    public AudioClip shotgunSoundEffect;
    public AudioClip popSoundEffect;
    public AudioClip CelebrateSoundEffect;

	public bool isHooting;
	public bool isDooring;
	public bool isShotgun;
	public bool isCorrect;
	public bool isWinning;

	private AudioSource _audioSource;
	private bool _hasAudioSource;

	private void WalkingAudioCheck()
	{
		if (!_hasAudioSource) return;
		switch (_audioSource.isPlaying)
		{
			case false when isHooting:
				_audioSource.clip = owlSoundEffect;
				_audioSource.loop = true;
				_audioSource.Play();
				break;
			case true when !isHooting:
				_audioSource.loop = false;
				break;
		}
	}
}

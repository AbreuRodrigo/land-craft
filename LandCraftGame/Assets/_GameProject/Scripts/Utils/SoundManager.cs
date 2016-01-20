using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public static SoundManager Instance;

	public AudioSource audioSource;
	public AudioClip magicFX;
	public AudioClip earthShake;
	public AudioClip attraction;
	public AudioClip falling;
	public AudioClip selector;
	public AudioClip denied;

	private bool sfx = true;
	private const float FADE_SOUND_SPEED = 0.2f;

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}

		DoFadeSoundIn();
	}

	public void PlayMagicFX() {
		PlayOneShotSound(magicFX);
	}

	public void PlayEarthShake() {
		PlayOneShotSound(earthShake);
	}

	public void PlayAttraction() {
		PlayOneShotSound(attraction);
	}

	public void PlayFalling() {
		PlayOneShotSound(falling);
	}

	public void PlaySelector() {
		PlayOneShotSound(selector);
	}

	public void PlayDenied() {
		PlayOneShotSound(denied);
	}

	private void PlayOneShotSound(AudioClip clip) {
		if(sfx) {
			audioSource.PlayOneShot(clip);
		}
	}

	public void TurnOnSFX() {
		sfx = true;
	}

	public void TurnOffSFX() {
		sfx = false;
	}

	public void DoFadeSoundIn() {
		StartCoroutine("FadeSoundIn");
	}

	public void DoFadeSoundOut() {
		StartCoroutine("FadeSoundOut");
	}

	IEnumerator FadeSoundIn() {
		float volume = 0;
		audioSource.volume = volume;

		while(volume < 1) {
			volume += 0.1f;

			yield return new WaitForSeconds(FADE_SOUND_SPEED);

			audioSource.volume = volume;
		}

		audioSource.volume = 1;
	}

	IEnumerator FadeSoundOut() {
		float volume = 1;
		audioSource.volume = volume;
		
		while(volume > 0) {
			volume -= 0.1f;
			
			yield return new WaitForSeconds(FADE_SOUND_SPEED);
			
			audioSource.volume = volume;
		}
		
		audioSource.volume = 0;
	}
}
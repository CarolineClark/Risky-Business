using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

	private AudioSource audioSource;
	private AudioClip meowSound1;
	private AudioClip meowSound2;
	private AudioClip meowSound3;
	private AudioClip meowSound4;
	private AudioClip[] clips;
	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;

	void Start () {
		// meowSound = Resources.Load<AudioClip>("SFX/angrycat");
		meowSound1 = Resources.Load<AudioClip>("FinalSounds/cat");
		meowSound2 = Resources.Load<AudioClip>("FinalSounds/cat 2");
		meowSound3 = Resources.Load<AudioClip>("FinalSounds/cat 3");
		meowSound4 = Resources.Load<AudioClip>("FinalSounds/cat 4");
		clips = new AudioClip[] {meowSound1, meowSound2, meowSound3, meowSound4};
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			// SoundManager.instance.PlaySingle(meowSound);
			RandomizeSfx(clips);
			EventManager.TriggerEvent(Constants.CAT_EVENT);
		}
	}

	public void RandomizeSfx (params AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);
		audioSource.pitch = randomPitch;
		audioSource.clip = clips[randomIndex];
		audioSource.Play();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour {

	private AudioClip vaseSound;
	private AudioSource audioSource;

	void Start () {
		vaseSound = Resources.Load<AudioClip>("FinalSounds/Vase Break");
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("ontriggerenter hit");
		if (other.tag == Constants.PLAYER_TAG) {
			audioSource.Play();
			EventManager.TriggerEvent(Constants.BREAK_POT_EVENT);
		}
	}
}

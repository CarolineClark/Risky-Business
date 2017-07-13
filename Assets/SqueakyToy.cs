using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueakyToy : MonoBehaviour {

	private AudioClip sound;
	private AudioSource source;

	void Start () {
		sound = Resources.Load<AudioClip>("FinalSounds/Bear");
		source = GetComponent<AudioSource>();
		source.clip = sound;
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("entered trigger");
		if (other.tag == Constants.PLAYER_TAG) {
			source.Play();
			EventManager.TriggerEvent(Constants.SQUEAKY_TOY_EVENT);
		}
	}
}

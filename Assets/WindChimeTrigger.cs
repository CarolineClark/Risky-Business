using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindChimeTrigger : MonoBehaviour {

	private AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			audioSource.Play();
			EventManager.TriggerEvent(Constants.WIND_CHIME_TRIGGER_EVENT);
		}
	}
}

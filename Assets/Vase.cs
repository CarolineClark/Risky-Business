using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour {

	private AudioClip vaseSound;

	void Start () {
		vaseSound = Resources.Load<AudioClip>("SFX/breakpot");
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			SoundManager.instance.PlaySingle(vaseSound);
			EventManager.TriggerEvent(Constants.BREAK_POT_EVENT);
		}
	}
}

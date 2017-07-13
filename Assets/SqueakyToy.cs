using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueakyToy : MonoBehaviour {

	private AudioClip sound;

	void Start () {
		sound = Resources.Load<AudioClip>("SFX/squeakytoy");
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			SoundManager.instance.PlaySingle(sound);
			EventManager.TriggerEvent(Constants.SQUEAKY_TOY_EVENT);
		}
	}
}

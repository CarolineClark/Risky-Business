using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

	private AudioClip yowlSound;

	void Start () {
		yowlSound = Resources.Load<AudioClip>("SFX/angrycat");
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			SoundManager.instance.PlaySingle(yowlSound);
			EventManager.TriggerEvent(Constants.CAT_EVENT);
		}
	}
}

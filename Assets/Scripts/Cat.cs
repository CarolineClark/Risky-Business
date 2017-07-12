using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

	public AudioClip yowlSound;

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			SoundManager.instance.PlaySingle(yowlSound);
			EventManager.TriggerEvent(Constants.CAT_EVENT);
		}
	}
}

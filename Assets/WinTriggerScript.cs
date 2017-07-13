using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriggerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			EventManager.TriggerEvent(Constants.WIN_GAME_EVENT);
		}
	}
}

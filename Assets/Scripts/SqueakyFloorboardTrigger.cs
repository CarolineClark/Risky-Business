using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueakyFloorboardTrigger : MonoBehaviour {

	void Start () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			other.GetComponent<PlayerController>().SetIsOnSqueakyFloorboard(true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			other.GetComponent<PlayerController>().SetIsOnSqueakyFloorboard(false);
		}
	}
}

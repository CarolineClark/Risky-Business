using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueakyFloorboardTrigger : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			
		}
	}
}

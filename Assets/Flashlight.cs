using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

	GameObject flashlightGameObject;
	Light flashlight;
	bool lightIsActive = false;

	void Start () {
		flashlightGameObject = transform.Find("Flashlight").gameObject;
		flashlight = flashlightGameObject.GetComponent<Light>();
		flashlightGameObject.SetActive(false);
	}

	void Update() {
		if (lightIsActive) {
			float phi = Time.time / 4 * 2 * Mathf.PI;
			float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;
			flashlight.intensity = amplitude;
		}
	}
	
	public void TurnOnFlashlight() {
		flashlightGameObject.SetActive(true);
		lightIsActive = true;
	}

	public void TurnOffFlashlight() {
		flashlightGameObject.SetActive(false);
		lightIsActive = false;
	}
}

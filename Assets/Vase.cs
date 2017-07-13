using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour {

	private AudioClip vaseSound;
	private AudioSource audioSource;
	private bool smashed = false;
	private GameObject vaseUnbroken;
	private GameObject vaseBroken;

	void Start () {
		vaseSound = Resources.Load<AudioClip>("FinalSounds/Vase Break");
		audioSource = GetComponent<AudioSource>();
		vaseUnbroken = transform.Find("VaseUnbroken").gameObject;
		vaseUnbroken.SetActive(true);
		vaseBroken = transform.Find("VaseBroken").gameObject;
		vaseBroken.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG && !smashed) {
			smashed = true;
			audioSource.Play();
			EventManager.TriggerEvent(Constants.BREAK_POT_EVENT);
			vaseBroken.SetActive(true);
			vaseUnbroken.SetActive(false);
		}
	}
}

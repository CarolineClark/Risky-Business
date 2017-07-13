using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	private GameObject startScreen;
	private GameObject loseScreen;
	private GameObject winScreen;
	private PlayerController playerController;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);  

		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		startScreen = GameObject.FindGameObjectWithTag(Constants.START_SCREEN_TAG);
		winScreen = GameObject.FindGameObjectWithTag(Constants.WIN_SCREEN_TAG);
		loseScreen = GameObject.FindGameObjectWithTag(Constants.LOSE_SCREEN_TAG);
		playerController = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).GetComponent<PlayerController>();
		EventManager.StartListening(Constants.WIN_GAME_EVENT, ShowWinScreen);
		EventManager.StartListening(Constants.LOSE_GAME_EVENT, ShowLoseScreen);
		ShowStartScreen();
	}

	void ShowStartScreen() {
		startScreen.SetActive(true);
		loseScreen.SetActive(false);
		winScreen.SetActive(false);
	}

	public void StartGame() {
		startScreen.SetActive(false);
		loseScreen.SetActive(false);
		winScreen.SetActive(false);
		Camera.main.GetComponent<Flashlight>().TurnOffFlashlight();
		playerController.SetPlayerFrozen(false);
		AudioClip audioClip = Resources.Load<AudioClip>("Sounds/Dragon feeder edit_1");
		SoundManager.instance.PlayMusic(audioClip);
	}

	void ShowLoseScreen(Hashtable h) {
		playerController.SetPlayerFrozen(true);
		Camera.main.GetComponent<Flashlight>().TurnOnFlashlight();
		SoundManager.instance.StopPlayingMusic();
		StartCoroutine(Wait());
	}

	void ShowWinScreen(Hashtable h) {
		playerController.SetPlayerFrozen(true);
		SoundManager.instance.StopPlayingMusic();
		AudioClip audioClip = Resources.Load<AudioClip>("Sounds/Win sound");
		SoundManager.instance.PlaySingle(audioClip, true);
		winScreen.SetActive(true);
	}

	IEnumerator Wait() {
		yield return new WaitForSeconds (1.5f);
		AudioClip audioClip = Resources.Load<AudioClip>("Sounds/Lose sound");
		SoundManager.instance.PlaySingle(audioClip, true);
		loseScreen.SetActive(true);
		Debug.Log("Losescreen button = " + loseScreen.GetComponentInChildren<Button>().gameObject);
		EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(loseScreen.GetComponentInChildren<Button>().gameObject);
	}
}

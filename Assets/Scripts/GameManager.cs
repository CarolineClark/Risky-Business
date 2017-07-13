using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	private GameObject startScreen;
	private GameObject loseScreen;
	private GameObject winScreen;

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
		AudioClip audioClip = Resources.Load<AudioClip>("Sounds/Dragon feeder edit_1");
		SoundManager.instance.PlayMusic(audioClip);
	}

	void ShowLoseScreen(Hashtable h) {
		SoundManager.instance.StopPlayingMusic();
		AudioClip audioClip = Resources.Load<AudioClip>("Sounds/Lose sound");
		SoundManager.instance.PlaySingle(audioClip);
		loseScreen.SetActive(true);
	}

	void ShowWinScreen(Hashtable h) {
		SoundManager.instance.StopPlayingMusic();
		AudioClip audioClip = Resources.Load<AudioClip>("Sounds/Win sound");
		SoundManager.instance.PlaySingle(audioClip);
		winScreen.SetActive(true);
	}
}

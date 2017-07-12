using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private GameObject startScreen;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);  

		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		startScreen = GameObject.FindGameObjectWithTag(Constants.START_SCREEN_TAG);
		ShowStartScreen();
	}

	void ShowStartScreen() {
		startScreen.SetActive(true);
	}

	public void StartGame() {
		startScreen.SetActive(false);
	}
}

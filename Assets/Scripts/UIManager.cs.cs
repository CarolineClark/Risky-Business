using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}

	//Reloads the Level
	public void Reload()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	// Update is called once per frame
	void Update () {
		
	}
}

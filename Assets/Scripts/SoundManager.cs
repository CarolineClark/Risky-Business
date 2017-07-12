using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    void Start() {
        EventManager.StartListening(Constants.CAT_EVENT, PlayCatSound);
    }

    void PlayCatSound(Hashtable h) {
        Debug.Log("Playing cat sound");
    }
}
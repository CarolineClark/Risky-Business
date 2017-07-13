using UnityEngine;
using System.Collections;

public class EventTriggerTest : MonoBehaviour {


    void Update () {
        if (Input.GetKeyDown ("w"))
        {
            EventManager.TriggerEvent(Constants.WIN_GAME_EVENT);
        }

        if (Input.GetKeyDown ("l"))
        {
            EventManager.TriggerEvent(Constants.LOSE_GAME_EVENT);
        }
    }
}
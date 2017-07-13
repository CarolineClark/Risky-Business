using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseBar : MonoBehaviour {
	private float catSoundIncrease = 0.2f;
	private float quietSqueakyFloorboardSoundIncrease = 0.2f;
	private float loudSqueakyFloorboardSoundIncrease = 0.3f;
	private float squeakyToyIncrease = 0.4f;
	private float windChimeIncrease = 0.3f;
	private float breakPotIncrease = 0.5f;
	private float descreaseSpeed = 0.1f;
	Image image;
	private Image containerImage;

	void Start () {
		EventManager.StartListening(Constants.CAT_EVENT, CatIncreaseSoundLevel);
		EventManager.StartListening(Constants.SQUEAKY_FLOORBOARD_LOUD_EVENT, LoudSqueakyFloorboardIncreaseSoundLevel);
		EventManager.StartListening(Constants.SQUEAKY_FLOORBOARD_QUIET_EVENT, QuietSqueakyFloorboardIncreaseSoundLevel);
		EventManager.StartListening(Constants.BREAK_POT_EVENT, BreakPotIncreaseSoundLevel);
		EventManager.StartListening(Constants.SQUEAKY_TOY_EVENT, SqueakyToyIncreaseSoundLevel);
		EventManager.StartListening(Constants.WIND_CHIME_TRIGGER_EVENT, WindChimeIncreaseSoundLevel);
		EventManager.StartListening(Constants.LOSE_GAME_EVENT, ResetBar);
		image = GetComponent<Image>();
		image.type = Image.Type.Filled;
		image.fillMethod = Image.FillMethod.Vertical;
		image.fillOrigin = (int)Image.OriginVertical.Bottom;
		image.fillAmount = 0;
		image.canvasRenderer.SetAlpha(0.5f);
		containerImage = GameObject.FindGameObjectWithTag(Constants.EMPTY_NOISE_CONTAINER_TAG).GetComponent<Image>();
		containerImage.canvasRenderer.SetAlpha(0.3f);
	}
	
	void Update () {
		image.fillAmount -= Time.deltaTime * descreaseSpeed;
	}
	
	void CatIncreaseSoundLevel(Hashtable h) {
		image.fillAmount += catSoundIncrease;
		CheckIfLost();
	}

	void QuietSqueakyFloorboardIncreaseSoundLevel(Hashtable h) {
		image.fillAmount += quietSqueakyFloorboardSoundIncrease;
		CheckIfLost();
	}

	void LoudSqueakyFloorboardIncreaseSoundLevel(Hashtable h) {
		image.fillAmount += loudSqueakyFloorboardSoundIncrease;
		CheckIfLost();
	}

	void BreakPotIncreaseSoundLevel(Hashtable h) {
		image.fillAmount += breakPotIncrease;
		CheckIfLost();
	}

	void WindChimeIncreaseSoundLevel(Hashtable h) {
		image.fillAmount += windChimeIncrease;
		CheckIfLost();
	}

	void SqueakyToyIncreaseSoundLevel(Hashtable h) {
		image.fillAmount += squeakyToyIncrease;
		CheckIfLost();
	}

	void CheckIfLost() {
		if (image.fillAmount == 1) {
			EventManager.TriggerEvent(Constants.LOSE_GAME_EVENT);
		}
	}

	void ResetBar(Hashtable h) {
		image.fillAmount = 0;
	}
}

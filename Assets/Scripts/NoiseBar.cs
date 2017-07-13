using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseBar : MonoBehaviour {
	private float catSoundIncrease = 0.3f;
	private float quietSqueakyFloorboardSoundIncrease = 0.3f;
	private float loudSqueakyFloorboardSoundIncrease = 0.6f;
	private float descreaseSpeed = 0.2f;
	Image image;
	private Image containerImage;

	void Start () {
		EventManager.StartListening(Constants.CAT_EVENT, CatIncreaseSoundLevel);
		EventManager.StartListening(Constants.SQUEAKY_FLOORBOARD_LOUD_EVENT, LoudSqueakyFloorboardIncreaseSoundLevel);
		EventManager.StartListening(Constants.SQUEAKY_FLOORBOARD_QUIET_EVENT, QuietSqueakyFloorboardIncreaseSoundLevel);
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

	void CheckIfLost() {
		if (image.fillAmount == 1) {
			EventManager.TriggerEvent(Constants.LOSE_GAME_EVENT);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseBar : MonoBehaviour {
	private float catSoundIncrease = 0.3f;
	private float descreaseSpeed = 0.2f;
	Image image;
	private Image containerImage;

	void Start () {
		EventManager.StartListening(Constants.CAT_EVENT, IncreaseSoundLevel);
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
	
	void IncreaseSoundLevel(Hashtable h) {
		image.fillAmount += catSoundIncrease;
		if (image.fillAmount == 1) {
			EventManager.TriggerEvent(Constants.LOSE_GAME_EVENT);
		}
	}
}

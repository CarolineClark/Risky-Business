using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseBar : MonoBehaviour {
	private float catSoundIncrease = 0.3f;
	private float descreaseSpeed = 0.2f;
	Image image;

	void Start () {
		EventManager.StartListening(Constants.CAT_EVENT, IncreaseSoundLevel);
		image = GetComponent<Image>();
		image.type = Image.Type.Filled;
		image.fillMethod = Image.FillMethod.Vertical;
		image.fillOrigin = (int)Image.OriginVertical.Bottom;
		image.fillAmount = 0;
	}
	
	void Update () {
		image.fillAmount -= Time.deltaTime * descreaseSpeed;
	}
	
	void IncreaseSoundLevel(Hashtable h) {
		image.fillAmount += catSoundIncrease;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentButton : MonoBehaviour {

	void Start () {
		GetComponent<Image>().color = Color.clear;
	}
}

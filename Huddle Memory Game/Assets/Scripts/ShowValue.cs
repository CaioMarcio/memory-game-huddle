using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValue : MonoBehaviour {

    private Text pairNumberText;

	void Start () {
        pairNumberText = GetComponent<Text>();
	}
	
	public void textUpdate (float value) {
        pairNumberText.text = Mathf.RoundToInt(value) + "";	
	}
}

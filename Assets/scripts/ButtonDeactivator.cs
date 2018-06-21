using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ButtonDeactivator : MonoBehaviour {
    public bool activeAtStart;
	// Use this for initialization
	void Start () {
        SetActive(activeAtStart);
	}

    public void SetActive(bool value) {
        Button b = GetComponent<Button>();
        if (b != null) { b.interactable = value; }
        ButtonHandler bh = GetComponent<ButtonHandler>();
        if (bh != null) {
            bh.enabled = value;
        }
        Image img = GetComponent<Image>();
        if (img != null) {
            img.enabled = value;
        }
        Image[] imgs = GetComponentsInChildren<Image>();
        foreach (Image i in imgs) {
            i.enabled = value;
        }

    }
}

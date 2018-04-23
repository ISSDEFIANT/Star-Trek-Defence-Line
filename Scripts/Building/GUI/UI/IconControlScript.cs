using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IconControlScript : MonoBehaviour {
	public Sprite NullObject;
	private Image Target;
	private BackgroudUI _BUI;
	// Use this for initialization
	void Start () {
		Target = gameObject.GetComponent<Image> ();
		_BUI = GameObject.FindGameObjectWithTag("MainUI").GetComponent<BackgroudUI>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_BUI.pictureSelectObject != null) {
			Target.sprite = _BUI.pictureSelectObject;
		} else {
			Target.sprite = NullObject;
		}
	}
}

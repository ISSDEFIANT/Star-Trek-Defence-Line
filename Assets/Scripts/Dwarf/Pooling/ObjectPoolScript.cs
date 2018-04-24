using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour {
	public GameObject[] objects;
	public GameObject Torpido;

	public int poolSize = 0;

	// Use this for initialization
	void Start () {
		objects = new GameObject[poolSize];
		for (int i = 0; i < poolSize; i++) {
			objects [i] = Instantiate (Torpido) as GameObject;
			//objects [i].transform.parent = gameObject.transform;
			objects [i].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ActivateObject(){
		for (int i = 0; i < poolSize; i++) {
			if (objects [i].activeInHierarchy == false) {
				objects [i].SetActive (true);
				return;
			}
		}
	}
}

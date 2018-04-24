using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomObjectActive : MonoBehaviour {
	public List<GameObject> ObjectList;
	public List<GameObject> CamerasList;
	private int RandomNumber;
	// Use this for initialization
	void Start () {
		RandomNumber = Random.Range (0, ObjectList.Count);
		ObjectList [RandomNumber].SetActive (true);
		CamerasList [RandomNumber].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

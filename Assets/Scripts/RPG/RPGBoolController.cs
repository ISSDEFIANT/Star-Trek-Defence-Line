using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGBoolController : MonoBehaviour {
	public GameObject Ship;
	private GlobalDB _GDB;
	// Use this for initialization
	void Start () {
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!FindInList (Ship)) {
			_GDB.selectList.Clear ();
			_GDB.selectList.Add (Ship);
			_GDB.selectList.Clear ();
			_GDB.gameObject.GetComponent<Select> ().RPGTrue = true;
		}
	}
	bool FindInList (GameObject obj)
	{
		foreach(GameObject selObj in _GDB.selectList)
		{
			if(selObj == obj)
				return true;
		}
		return false;
	}
}

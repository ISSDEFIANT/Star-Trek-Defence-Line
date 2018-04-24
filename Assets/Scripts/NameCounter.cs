using UnityEngine;
using System.Collections;

public class NameCounter : MonoBehaviour {
	public int MaxShips;
	public int CurShips;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake(){
		CurShips = MaxShips;
	}
}

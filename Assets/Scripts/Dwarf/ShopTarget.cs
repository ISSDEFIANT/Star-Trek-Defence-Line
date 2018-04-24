using UnityEngine;
using System.Collections;

public class ShopTarget : MonoBehaviour {
	public int Titenium;
	public int Dilitium;
	public int Humans;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject.FindGameObjectWithTag ("MainUI").GetComponent<GlobalDB> ().Titanium += Titenium;
		GameObject.FindGameObjectWithTag ("MainUI").GetComponent<GlobalDB> ().Dilithium += Dilitium;
		GameObject.FindGameObjectWithTag ("MainUI").GetComponent<GlobalDB> ().Humans += Humans;
	}
	void LateUpdate(){
		if (Titenium != 0) {
			Titenium = 0;
		}
		if (Dilitium != 0) {
			Dilitium = 0;
		}
		if (Humans != 0) {
			Humans = 0;
		}
	}
}

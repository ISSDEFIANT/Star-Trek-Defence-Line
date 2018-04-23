using UnityEngine;
using System.Collections;

public class Research : MonoBehaviour {
	public float ScientTime;

	public Sprite Texture;
	public float Cost;

	public bool T1;
	public bool T2;
	public bool T3;

	public bool Scient;

	private Magic Owner;
	private GlobalDB _GDB;

	public GUISkin mainSkin;
	public int numDepth = 1;

	public float XPosition = 220;
	public float YPosition = 70;
	public float XScale = 100;
	public float YScale = 100;

	private float LockTimer;
	[HideInInspector]
	public float StartTime;
	[HideInInspector]
	public bool Locking;
	// Use this for initialization
	void Start () {
		StartTime = ScientTime;

		Owner = gameObject.GetComponent<Magic> ();
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		LockTimer = Random.Range (0, 15);
	}
	
	// Update is called once per frame
	void Update () {
		if (Scient) {
			Owner.TimeScient = ScientTime;
			Owner.Scient = true;
			Owner.CurRes = this;
			if (ScientTime > 0) {
				ScientTime -= Time.deltaTime;
			}
			if(!Owner.AI && !Owner.FreandAI){
				if (ScientTime <= 0) {
					
					Scient = false;
				}
			}
		}
	}
	public void SciActive(){
		if (!Owner.Scient) {
			if (_GDB.Humans >= Cost) {
				_GDB.Humans -= Cost;
				Scient = true;
			}
		}
	}
	public void SciCansled(){
		if (Owner.Scient) {
			_GDB.Humans += Cost;
			Scient = false;

			Owner.TimeScient = 0;
			Owner.Scient = false;
			Owner.CurRes = null;

			ScientTime = StartTime;
		}
	}
}
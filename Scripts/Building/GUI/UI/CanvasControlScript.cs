using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControlScript : MonoBehaviour {
	private GlobalDB _GDB;
	private GameMenu _GM;
	private BackgroudUI _BUI;
	// Use this for initialization
	void Start () {
		_GDB = gameObject.GetComponent<GlobalDB>();
		_GM = gameObject.GetComponent<GameMenu>();
		_BUI = gameObject.GetComponent<BackgroudUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PauseEvent(){
		_GM.pause = true;
	}
	public void GlobalMinimapTypeEvent(){
		GameObject.FindGameObjectWithTag ("CAMERAMOVE").GetComponent<CameraMove> ().LocalMinimap.SetActive (false);
		GameObject.FindGameObjectWithTag ("CAMERAMOVE").GetComponent<CameraMove> ().GlobalMinimap.SetActive (true);
		gameObject.GetComponent<MiniMap>().itsMinimapCamera = GameObject.FindGameObjectWithTag ("CAMERAMOVE").GetComponent<CameraMove> ().GlobalMinimap.GetComponent<Camera>();
	}
	public void LocalMinimapTypeEvent(){
		GameObject.FindGameObjectWithTag ("CAMERAMOVE").GetComponent<CameraMove> ().LocalMinimap.SetActive (true);
		GameObject.FindGameObjectWithTag ("CAMERAMOVE").GetComponent<CameraMove> ().GlobalMinimap.SetActive (false);
		gameObject.GetComponent<MiniMap>().itsMinimapCamera = GameObject.FindGameObjectWithTag ("CAMERAMOVE").GetComponent<CameraMove> ().LocalMinimap.GetComponent<Camera>();
	}
}
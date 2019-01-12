using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GlobalDB : MonoBehaviour
{
    public VoiceIndex.enRace PlayerRace;

	public float Humans;
	public float Dilithium;
	public float Titanium;

	public List<GameObject> obj = new List<GameObject>();
	public int numIntersection;
	public GameObject activeObjectInterface;
	public List<GameObject> dwarfList;
	public List<GameObject> selectList;

	public string PlayerName;

	public int СилаИгрока;
	public int СилаВрага;
	public int СилаСоюзника;

	public bool Win;
	public bool Fale;
	// Use this for initialization
	void Start () {
        
    }
	void OnGUI ()
	{
		if (Win) {
			if (!gameObject.GetComponent<GameMenu> ().pause) {
				Time.timeScale = 0;
			}
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "Mission Complete");
		}
		if (Fale) {
			if (!gameObject.GetComponent<GameMenu> ().pause) {
				Time.timeScale = 0;
			}
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "Mission failed");
		}
	}

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.H))
	    {
            ErrorWindow.ShowError("TestError");
        }
	}
	
	public void activationTrigger ()
	{
		for(int i = 0; i < obj.Count; i++)
		{
			obj[i].GetComponent<BoxCollider>().isTrigger = true;
		}
	}
	
	public void deactivationTrigger ()
	{
		for(int i = 0; i < obj.Count; i++)
		{
			obj[i].GetComponent<BoxCollider>().isTrigger = false;
		}
	}
	
	public void deactivationInterface ()
	{
		activeObjectInterface.GetComponent<Station>().visible = false;
	}
}

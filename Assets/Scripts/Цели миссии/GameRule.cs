using UnityEngine;
using System.Collections;

public class GameRule : MonoBehaviour {
	public bool FindObjectInZone;
	public bool FindObjectInZoneFreand;
	public bool ProtectObject;
	public bool DistroyObject;
	public bool DistroyAllEnemyBase;
	public bool CollectMoney;
	public bool TimeMissing;

	public bool Complite;
	public bool Fale;

	public GameObject FogOfWar;
	public GameObject protectObject;
	public GameObject distroyObject;
	public GameObject findObject;
	public float MissingTimer;
	public GameObject Zone;

	public GameObject GreenTargetGOV;
	public GameObject BlueTargetGOV;
	public GameObject RedTargetGOV;

	public bool Ok;
	public bool Next;
	public bool TotalObjective;

	public GameObject NextObjective;

	GameObject[] gos;
	GameObject closest;

	public bool GoToTarget;
	public GameObject Target;
	// Use this for initialization
	void Start () {

	}
	public	GameObject FindPlayerShips()
	{            
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
	// Update is called once per frame
	void Update () {
		gos = GameObject.FindGameObjectsWithTag("Dwarf");
		if (ProtectObject) {
			if (!Ok) {
				//Instantiate (GreenTargetGOV, protectObject.transform.position, GreenTargetGOV.transform.rotation);
				Ok = true;
			}
			if (protectObject == null) {
				Fale = true;
			}
			if (protectObject != null) {
				if (TimeMissing) {
					MissingTimer -= Time.deltaTime;
					if (MissingTimer <= 0) {
						Complite = true;
					}
				}
			}
		}

		if (FindObjectInZone) {
			if(Vector3.Distance(findObject.transform.position, GameObject.FindGameObjectWithTag("Dwarf").transform.position) < 10){
				Complite = true;
			}
		}
		if (FindObjectInZoneFreand) {
			if(Vector3.Distance(findObject.transform.position, GameObject.FindGameObjectWithTag("Freand").transform.position) < 10){
				Complite = true;
			}
		}

		if (DistroyObject) {
			//if (!Ok) {
			//	Instantiate (RedTargetGOV, distroyObject.transform.position, RedTargetGOV.transform.rotation);
			//	Ok = true;
			//}
			if (distroyObject == null) {
				Complite = true;
			}
		}
		if (Fale) {
			if (TotalObjective) {
				GameObject.FindGameObjectWithTag ("MainUI").GetComponent<GlobalDB> ().Fale = true;
			} else {
				if (NextObjective != null) {
					NextObjective.SetActive (true);
				}
				gameObject.SetActive(false);
			}
		}
		if (Complite) {
			//Destroy(GameObject.FindGameObjectWithTag("GOV"));
			if (NextObjective != null) {
				NextObjective.SetActive (true);
			}
			if (!Next) {
				GameObject.FindGameObjectWithTag ("MainUI").GetComponent<GlobalDB> ().Win = true;
			}
			gameObject.SetActive(false);
		}
		if (GoToTarget) {
			gameObject.transform.position = Target.transform.position;
		}
	}
}

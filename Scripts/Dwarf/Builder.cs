using UnityEngine;
using System.Collections;

public class Builder : MonoBehaviour {
	public GameObject BuilderTarget;
	public bool Build;
	public float DistanceToBuilding;

	private GlobalDB _GDB;
	private Stats _st;
	private MoveComponent _agent;

	public bool StopBool;

	public string Order;
	// Use this for initialization
	void Start () {
		_st = gameObject.GetComponent<Stats>();
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_agent = gameObject.GetComponent<MoveComponent>();
	}
	
	// Update is called once per frame
	void LateUpdate(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000)) {
			if (_st.WasSelect) {
				if (!_st.AI & !_st.FreandAI) {
					if (Input.GetMouseButtonDown (1)) {
						//print ("Hit" + hit.transform.gameObject.name);
						BuilderTarget = hit.transform.gameObject;
						//_agent.ResetPath ();
					}
				}
			}
		}
	}
	void Update () {
		if (BuilderTarget != null) {
			DistanceToBuilding = Vector3.Distance (gameObject.transform.position, BuilderTarget.transform.position);
			if (!_st.AI & !_st.FreandAI) {
				if (BuilderTarget.tag == "BuildingBuilding") {
					if (Vector3.Distance(gameObject.transform.position, BuilderTarget.transform.position) > BuilderTarget.GetComponent<BuildingStationScript> ().BuildDistance) {
						Build = false;
						_agent.Movement (BuilderTarget.transform.position);
					}
					if (Vector3.Distance(gameObject.transform.position, BuilderTarget.transform.position) <= BuilderTarget.GetComponent<BuildingStationScript> ().BuildDistance) {
						if (!StopBool) {
							gameObject.GetComponent<Stats> ().targetVector = gameObject.transform.position;
							_agent.Stop ();
							//_agent.ResetPath ();
							StopBool = true;
						}
						Build = true;
					}
				}
				if (BuilderTarget.tag != "BuildingBuilding") {
					Build = false;
					BuilderTarget = null;
				}
			}
			if (_st.AI) {
				if (BuilderTarget.tag == "BuildingBuildingEnemy") {
					if (Vector3.Distance(gameObject.transform.position, BuilderTarget.transform.position) > BuilderTarget.GetComponent<BuildingStationScript> ().BuildDistance) {
						Build = false;
						_agent.Movement (BuilderTarget.transform.position);
					}
					if (Vector3.Distance(gameObject.transform.position, BuilderTarget.transform.position) <= BuilderTarget.GetComponent<BuildingStationScript> ().BuildDistance) {
						Build = true;
						if (!StopBool) {
							gameObject.GetComponent<Stats> ().targetVector = gameObject.transform.position;
							_agent.Stop ();
							//_agent.ResetPath ();
							StopBool = true;
						}
					}
				}
				if (BuilderTarget.tag != "BuildingBuildingEnemy") {
					Build = false;
					BuilderTarget = null;
				}
			}
			if (_st.FreandAI) {
					if (BuilderTarget.tag == "BuildingBuildingFreand") {
					if (Vector3.Distance(gameObject.transform.position, BuilderTarget.transform.position) > BuilderTarget.GetComponent<BuildingStationScript> ().BuildDistance) {
							Build = false;
						_agent.Movement (BuilderTarget.transform.position);
						}
					if (Vector3.Distance(gameObject.transform.position, BuilderTarget.transform.position) <= BuilderTarget.GetComponent<BuildingStationScript> ().BuildDistance) {
							Build = true;
						if (!StopBool) {
							gameObject.GetComponent<Stats> ().targetVector = gameObject.transform.position;
							_agent.Stop ();
							//_agent.ResetPath ();
							StopBool = true;
						}
						}
					}
				if (BuilderTarget.tag != "BuildingBuildingFreand") {
					Build = false;
					BuilderTarget = null;
				}
				}
			}
		if (Build) {
			if (BuilderTarget != null) {
				if (!FindInList (gameObject)) {
					BuilderTarget.GetComponent<BuildingStationScript> ().Builders.Add (gameObject);
					_GDB.gameObject.GetComponent<Select> ().PlayConstructingBeganSound(gameObject);
				}
				if (_st.WasSelect) {
					if (!_st.AI & !_st.FreandAI) {
						if (Input.GetMouseButtonDown (1)) {
							BuilderTarget.GetComponent<BuildingStationScript> ().Builders.Remove (gameObject);
							//BuilderTarget = null;
							Build = false;
						}
					}
				}
			}
		}
		if(BuilderTarget != null & !Build){
			BuilderTarget.GetComponent<BuildingStationScript> ().Builders.Remove (gameObject);
		}
		if (BuilderTarget != null) {
			if (BuilderTarget.GetComponent<BuildingStationScript> ().Timer <= 0) {
				StopBool = false;
			}
		}
		if (!Build) {
			StopBool = false;
		}
		if (BuilderTarget == null) {
			Build = false;
		} 
		if (BuilderTarget != null) {	
			if (_st.AI || _st.FreandAI){
				BuilderTarget.GetComponent<BuildingStationScript> ().Owner = _st.Owner;
			}
			if (BuilderTarget.GetComponent<BuildingStationScript> ().Timer <= 0) {
				if (!BuilderTarget.GetComponent<BuildingStationScript> ().AI && !BuilderTarget.GetComponent<BuildingStationScript> ().FreandAI) {
					_GDB.gameObject.GetComponent<Select> ().PlayConstructingEndSound(gameObject);
				}
			}
		}
		if (_st.AI || _st.FreandAI) {
			if (!gameObject.GetComponent<ShipAI> ().DilithiumMiner && !gameObject.GetComponent<ShipAI> ().TitaniumMiner) {
				if (!FindInBuildersList (gameObject)) {
					_st.Owner.GetComponent<GlobalAI> ().Builders.Add (gameObject);
				}
				if (BuilderTarget == null) {
					if (!FindInFreeBuildersList (gameObject)) {
						_st.Owner.GetComponent<GlobalAI> ().FreeBuilders.Add (gameObject);
					}
				} else {
					if (FindInFreeBuildersList (gameObject)) {
						_st.Owner.GetComponent<GlobalAI> ().FreeBuilders.Remove (gameObject);
					}
				}
			} else {
				if (FindInBuildersList(gameObject)) {
					_st.Owner.GetComponent<GlobalAI> ().Builders.Remove (gameObject);
				}
				if (FindInFreeBuildersList(gameObject)) {
					_st.Owner.GetComponent<GlobalAI> ().FreeBuilders.Remove (gameObject);
				}
			}
		}
	}
	void OnDestroy(){
		if (_st.AI || _st.FreandAI) {
			if (FindInBuildersList(gameObject)) {
				_st.Owner.GetComponent<GlobalAI> ().Builders.Remove (gameObject);
			}
			if (BuilderTarget == null) {
				if (FindInFreeBuildersList(gameObject)) {
					_st.Owner.GetComponent<GlobalAI> ().FreeBuilders.Remove (gameObject);
				}
			}
		}
	}
	bool FindInList (GameObject obj)
	{
		if (BuilderTarget != null) {
			foreach (GameObject selObj in BuilderTarget.GetComponent<BuildingStationScript>().Builders) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}

	bool FindInBuildersList (GameObject obj)
	{
		if (_st.Owner != null) {
			foreach (GameObject selObj in _st.Owner.GetComponent<GlobalAI>().Builders) {
				if (selObj == obj)
					return true;
			}
			return false;
		}
		return false;
	}

	bool FindInFreeBuildersList (GameObject obj)
	{
		foreach (GameObject selObj in _st.Owner.GetComponent<GlobalAI>().FreeBuilders) {
			if (selObj == obj)
				return true;
		}
		return false;
	}

	public void Help(GameObject Builder){
		BuilderTarget = Builder.GetComponent<Builder>().BuilderTarget;
	}
}

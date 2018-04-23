using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {
	
	public GameObject _currentBuild;
	public LayerMask mask;
	
	private GlobalDB _GDB;
	private Select _Sel;
	private float Timer = 0.01f;
	private bool TimerStart;

	public GameObject Ship;
	// Use this for initialization
	void Start () {
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_Sel = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_currentBuild != null)
		{
			Ray ray;
			RaycastHit hit;
			
			ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 10000.0f, mask))
			{
				_currentBuild.transform.position = hit.point;
				_currentBuild.GetComponent<GhostBuilding> ().BuilderShip = Ship;
			}
			
			if(Input.GetMouseButtonDown(0) && _GDB.numIntersection == 0)
			{
				TimerStart = true;
			//	_currentBuild = null;
			//	_GDB.deactivationTrigger();
			}
			if (Input.GetMouseButtonDown (1)&& _GDB.numIntersection == 0) {
			//	_currentBuild = null;
			}
		}
	}
	
	public void setBuild (GameObject go)
	{
		_currentBuild = (GameObject)Instantiate(go);
		_currentBuild.GetComponent<GhostBuilding> ().BuilderShip = Ship;
		//_GDB.activationTrigger();
	}
}

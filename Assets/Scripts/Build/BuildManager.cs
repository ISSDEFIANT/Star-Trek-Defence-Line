using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {
	
	public GameObject _currentBuild;
	public LayerMask mask;

	public GameObject Ship;
	
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
		}
	}
	
	public void setBuild (GameObject go)
	{
		_currentBuild = (GameObject)Instantiate(go);
		_currentBuild.GetComponent<GhostBuilding> ().BuilderShip = Ship;
		//_GDB.activationTrigger();
	}
}

using UnityEngine;
using System.Collections;

public class CSSensor : MonoBehaviour
{
	public GameObject ClosestShip;
	GameObject[] gos;
	GameObject closest;

	public	GameObject FindClosestEnemy()
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
	void Update(){
		if (GameObject.FindGameObjectsWithTag ("Ship") != null) {
			if (GameObject.FindGameObjectsWithTag ("Ship") != gos) {
				gos = GameObject.FindGameObjectsWithTag ("Ship");
			}
		}
		if(FindClosestEnemy() != null){
			ClosestShip = FindClosestEnemy ();
		}
	}
}
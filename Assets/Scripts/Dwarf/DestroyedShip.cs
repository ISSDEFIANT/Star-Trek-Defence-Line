using UnityEngine;
using System.Collections;

public class DestroyedShip : MonoBehaviour {
	public float Titanium;
	public float Dilitium;
	public float Humans;

	public float TitaniumMax;
	public float DilitiumMax;
	public float HumansMax;

	public float TitaniumMin;
	public float DilitiumMin;
	public float HumansMin;

	// Use this for initialization
	void Start () {
		Titanium = Random.Range (TitaniumMin, TitaniumMax);
		Dilitium = Random.Range (DilitiumMin, DilitiumMax);
		Humans = Random.Range (HumansMin, HumansMax);
	}
	
	// Update is called once per frame
	void Update () {
	//	if (checkVisible () == false) {
	//		gameObject.GetComponent<MeshRenderer> ().enabled = false;
	//	} else {
	//		gameObject.GetComponent<MeshRenderer> ().enabled = true;
	//	}
	}
	public bool checkVisible ()
	{
		if (Camera.main != null) {
			return GeometryUtility.TestPlanesAABB (GeometryUtility.CalculateFrustumPlanes (Camera.main), transform.gameObject.GetComponent<Collider> ().bounds);
		} else {
			return false;
		}
	}
}

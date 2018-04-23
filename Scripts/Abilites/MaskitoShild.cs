using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaskitoShild : MonoBehaviour {
	public bool MaskShild;
	public List<Forcefield> MeshComponents;
	public RaycastHit Hit;
	private RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (MaskShild) {
			if (Physics.Raycast (transform.position, transform.position, out hit, 10)) {
				Hit = hit;
			}
			foreach(Forcefield selObj in MeshComponents)
			{
				selObj.Shot = true;
				selObj.PhaserHit = Hit;
			}
			MaskShild = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTypeVisible : MonoBehaviour {
	public bool Ship;
	public bool Station;

	public GameObject GhostObject;

	public bool IsVisible;
	private bool OldVisible;

	public List<MeshRenderer> Meshes;
	public List<Light> Lights;

	[HideInInspector]
	public bool FirstFinded;
	// Use this for initialization
	void Start () {
		foreach (MeshRenderer obj in gameObject.GetComponentsInChildren<MeshRenderer> ()) {
			Meshes.Add (obj);
		}
		foreach (Light obj in gameObject.GetComponentsInChildren<Light> ()) {
			Lights.Add (obj);
		}

		ReloadStatus ();
	}
	
	// Update is called once per frame
	void Update () {
		if (OldVisible != IsVisible) {
			ReloadStatus ();
		}

			
		for (int i = 0; i <= Meshes.Count - 1; i++) {
			if (Meshes [i].GetComponent<FoWIgnore> ()) {
				Meshes.RemoveAt (i);
			}
		}
	}

	public void ReloadStatus(){
		if (Ship) {
			if (IsVisible) {
				foreach (MeshRenderer obj in Meshes) {
					obj.enabled = true;
				}
				foreach (Light obj in Lights) {
					obj.enabled = true;
				}
			} else {
				foreach (MeshRenderer obj in Meshes) {
					obj.enabled = false;
				}
				foreach (Light obj in Lights) {
					obj.enabled = false;
				}
			}
		}
		if (Station) {
			if (IsVisible) {
				foreach (MeshRenderer obj in Meshes) {
					obj.enabled = true;
				}
				foreach (Light obj in Lights) {
					obj.enabled = true;
				}

				if (GhostObject != null) {
					GhostObject.SetActive (false);
				}
			} else {
				foreach (MeshRenderer obj in Meshes) {
					obj.enabled = false;
				}
				foreach (Light obj in Lights) {
					obj.enabled = false;
				}

				if (FirstFinded) {
					if (GhostObject != null) {
						GhostObject.SetActive (true);
					}
				}
			}
		}

		OldVisible = IsVisible;
	}
}

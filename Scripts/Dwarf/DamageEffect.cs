using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageEffect : MonoBehaviour {
	public GameObject StarShip;
	public bool Matirials;
	public bool GLOW;
	public bool Light;
	public Material Material;
	public Material EasyMaterial;
	public Material HavyMaterial;
	private bool Ok;
	public List<Material> Materials;
	public bool Use;
	private Vector2 vec;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		HealthModule _hm = StarShip.GetComponent<HealthModule>();

		if (_hm.curHealth <= _hm.maxHealth / 2) {
			Use = true;
			if (_hm.curHealth <= _hm.maxHealth / 5) {
				Material = HavyMaterial;
			}
			if (_hm.curHealth > _hm.maxHealth / 5) {
				Material = EasyMaterial;
			}
		} else {
			Use = false;
		}
		if (!Use) {
			Ok = false;
			if (!Light) {
				//gameObject.GetComponent<MeshRenderer> ().enabled = false;
				if (!GLOW) {
					if (gameObject.GetComponent<MeshRenderer> ().materials.Length == 3) {
						if (gameObject.GetComponent<Assimilated> ().Materials [2] == Material) {
							gameObject.GetComponent<Assimilated> ().Materials.RemoveAt (2);
						}
					}
					if (gameObject.GetComponent<MeshRenderer> ().materials.Length == 2) {
						if (gameObject.GetComponent<Assimilated> ().Materials [1] == Material) {
							gameObject.GetComponent<Assimilated> ().Materials.RemoveAt (1);
						}
					}
				}
			}
		}
		if (Use) {
			if (Matirials) {
				if (!GLOW) {
					if (gameObject.GetComponent<MeshRenderer> ().materials.Length == 1 || gameObject.GetComponent<MeshRenderer> ().materials.Length == 2) {
						Material mat = Material;
						mat.mainTextureOffset = vec;
						gameObject.GetComponent<Assimilated> ().Materials.Add (mat);
					} 
					if(gameObject.GetComponent<MeshRenderer> ().materials.Length >= 3) {
						Material mat = Material;
						mat.mainTextureOffset = vec;
						gameObject.GetComponent<Assimilated> ().Materials [2] = mat;
						if (gameObject.GetComponent<Assimilated> ().Materials [1] != gameObject.GetComponent<Assimilated> ().Material) {
							gameObject.GetComponent<Assimilated> ().Materials [1] = null;
						}
					}
				}
				if (!Ok) {
					if (!GLOW) {
						vec = new Vector2 (Random.Range (0f, 100f), Random.Range (0f, 100f));
					}
					Ok = true;
				}
			}
		}
		if (FindInMaterialList (null)) {
			if (gameObject.GetComponent<Assimilated> ().Materials[1] == null) {
				gameObject.GetComponent<Assimilated> ().Materials[1] = gameObject.GetComponent<Assimilated> ().Materials [0];
			}
		}
	}
	bool FindInMaterialList (Material obj)
	{
		foreach (Material selObj in gameObject.GetComponent<Assimilated> ().Materials) {
			if (selObj == obj)
				return true;
		}
		return false;
	}
}
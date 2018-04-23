using UnityEngine;
using System.Collections;

public class GhostErrorColor : MonoBehaviour {
	public Color BaseColor;
	public Color ErrorColor;
	public GhostBuilding Building;
	public bool error;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		error = Building.Error;
		if (Building.Error) {
			Renderer rend = gameObject.GetComponent<Renderer>();
			rend.material.shader = Shader.Find("SF_EFFECTS/TransparentRim");
			rend.material.SetColor("_RimColor", ErrorColor);
			rend.material.SetColor("_InnerColor", ErrorColor);
		} else {
			Renderer rend = GetComponent<Renderer>();
			rend.material.shader = Shader.Find("SF_EFFECTS/TransparentRim");
			rend.material.SetColor("_RimColor", BaseColor);
			rend.material.SetColor("_InnerColor", BaseColor);
		}
	}
}

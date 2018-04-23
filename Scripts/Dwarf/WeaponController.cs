using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour {
	public List<GameObject> Phasers;
	public List<GameObject> Torpidoses;
	public List<GameObject> EverActivePhasers;
	public List<GameObject> EverActiveTorpidoses;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<Stats> ().targetTransform != null) {
			if(Phasers.Count == 1){
				Phasers[0].GetComponent<Phaser>().enabled = true;
			}
			if(Phasers.Count == 2){
				if(Phasers[0].GetComponent<Distanser>().DistanceToTarget < Phasers[1].GetComponent<Distanser>().DistanceToTarget){
					Phasers[0].GetComponent<Phaser>().enabled = true;
					Phasers[1].GetComponent<Phaser>().enabled = false;
				}
				if(Phasers[0].GetComponent<Distanser>().DistanceToTarget > Phasers[1].GetComponent<Distanser>().DistanceToTarget){
					Phasers[1].GetComponent<Phaser>().enabled = true;
					Phasers[0].GetComponent<Phaser>().enabled = false;
				}
			}
			if(Phasers.Count == 3){
				if(Phasers[0].GetComponent<Distanser>().DistanceToTarget < Phasers[1].GetComponent<Distanser>().DistanceToTarget & Phasers[0].GetComponent<Distanser>().DistanceToTarget < Phasers[2].GetComponent<Distanser>().DistanceToTarget){
					Phasers[0].GetComponent<Phaser>().enabled = true;
					Phasers[1].GetComponent<Phaser>().enabled = false;
					Phasers[2].GetComponent<Phaser>().enabled = false;
				}
				if(Phasers[1].GetComponent<Distanser>().DistanceToTarget < Phasers[0].GetComponent<Distanser>().DistanceToTarget & Phasers[1].GetComponent<Distanser>().DistanceToTarget < Phasers[2].GetComponent<Distanser>().DistanceToTarget){
					Phasers[1].GetComponent<Phaser>().enabled = true;
					Phasers[0].GetComponent<Phaser>().enabled = false;
					Phasers[2].GetComponent<Phaser>().enabled = false;
				}
				if(Phasers[2].GetComponent<Distanser>().DistanceToTarget < Phasers[1].GetComponent<Distanser>().DistanceToTarget & Phasers[2].GetComponent<Distanser>().DistanceToTarget < Phasers[0].GetComponent<Distanser>().DistanceToTarget){
					Phasers[2].GetComponent<Phaser>().enabled = true;
					Phasers[0].GetComponent<Phaser>().enabled = false;
					Phasers[1].GetComponent<Phaser>().enabled = false;
				}
			}
			if(Torpidoses.Count == 1){
				Torpidoses[0].GetComponent<TopidoLounser>().enabled = true;
				Torpidoses[0].GetComponent<TopidoLounser>().Active = true;
			}
			if(Torpidoses.Count == 2){
				if(Torpidoses[0].GetComponent<Distanser>().DistanceToTarget < Torpidoses[1].GetComponent<Distanser>().DistanceToTarget){
					Torpidoses[0].GetComponent<TopidoLounser>().enabled = true;
					Torpidoses[0].GetComponent<TopidoLounser>().Active = true;
					Torpidoses[1].GetComponent<TopidoLounser>().Active = false;
					Torpidoses[1].GetComponent<TopidoLounser>().enabled = false;
				}
				if(Torpidoses[0].GetComponent<Distanser>().DistanceToTarget > Torpidoses[1].GetComponent<Distanser>().DistanceToTarget){
					Torpidoses[1].GetComponent<TopidoLounser>().enabled = true;
					Torpidoses[1].GetComponent<TopidoLounser>().Active = true;
					Torpidoses[0].GetComponent<TopidoLounser>().Active = false;
					Torpidoses[0].GetComponent<TopidoLounser>().enabled = false;
				}
			}
			if(Torpidoses.Count == 3){
				if(Torpidoses[0].GetComponent<Distanser>().DistanceToTarget < Torpidoses[1].GetComponent<Distanser>().DistanceToTarget & Torpidoses[0].GetComponent<Distanser>().DistanceToTarget < Torpidoses[2].GetComponent<Distanser>().DistanceToTarget){
					Torpidoses[0].GetComponent<TopidoLounser>().enabled = true;
					Torpidoses[0].GetComponent<TopidoLounser>().Active = true;

					Torpidoses[1].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[1].GetComponent<TopidoLounser>().Active = false;

					Torpidoses[2].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[2].GetComponent<TopidoLounser>().Active = false;
				}
				if(Torpidoses[1].GetComponent<Distanser>().DistanceToTarget < Torpidoses[0].GetComponent<Distanser>().DistanceToTarget & Torpidoses[1].GetComponent<Distanser>().DistanceToTarget < Torpidoses[2].GetComponent<Distanser>().DistanceToTarget){
					Torpidoses[1].GetComponent<TopidoLounser>().enabled = true;
					Torpidoses[1].GetComponent<TopidoLounser>().Active = true;

					Torpidoses[0].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[0].GetComponent<TopidoLounser>().Active = false;

					Torpidoses[2].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[2].GetComponent<TopidoLounser>().Active = false;
				}
				if(Torpidoses[2].GetComponent<Distanser>().DistanceToTarget < Torpidoses[1].GetComponent<Distanser>().DistanceToTarget & Torpidoses[2].GetComponent<Distanser>().DistanceToTarget < Torpidoses[0].GetComponent<Distanser>().DistanceToTarget){
					Torpidoses[2].GetComponent<TopidoLounser>().enabled = true;
					Torpidoses[2].GetComponent<TopidoLounser>().Active = true;

					Torpidoses[0].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[0].GetComponent<TopidoLounser>().Active = false;

					Torpidoses[1].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[1].GetComponent<TopidoLounser>().Active = false;
				}
			}
			if(Torpidoses.Count == 4){
				if(Torpidoses[0].GetComponent<Distanser>().DistanceToTarget < Torpidoses[1].GetComponent<Distanser>().DistanceToTarget & Torpidoses[0].GetComponent<Distanser>().DistanceToTarget < Torpidoses[2].GetComponent<Distanser>().DistanceToTarget & Torpidoses[0].GetComponent<Distanser>().DistanceToTarget < Torpidoses[3].GetComponent<Distanser>().DistanceToTarget){
					Torpidoses[0].GetComponent<TopidoLounser>().enabled = true;
					Torpidoses[0].GetComponent<TopidoLounser>().Active = true;
					
					Torpidoses[1].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[1].GetComponent<TopidoLounser>().Active = false;
					
					Torpidoses[2].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[2].GetComponent<TopidoLounser>().Active = false;

					Torpidoses[3].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[3].GetComponent<TopidoLounser>().Active = false;
				}
				if(Torpidoses[1].GetComponent<Distanser>().DistanceToTarget < Torpidoses[0].GetComponent<Distanser>().DistanceToTarget & Torpidoses[1].GetComponent<Distanser>().DistanceToTarget < Torpidoses[2].GetComponent<Distanser>().DistanceToTarget & Torpidoses[1].GetComponent<Distanser>().DistanceToTarget < Torpidoses[3].GetComponent<Distanser>().DistanceToTarget){
					Torpidoses[1].GetComponent<TopidoLounser>().enabled = true;
					Torpidoses[1].GetComponent<TopidoLounser>().Active = true;
					
					Torpidoses[0].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[0].GetComponent<TopidoLounser>().Active = false;
					
					Torpidoses[2].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[2].GetComponent<TopidoLounser>().Active = false;

					Torpidoses[3].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[3].GetComponent<TopidoLounser>().Active = false;
				}
				if(Torpidoses[2].GetComponent<Distanser>().DistanceToTarget < Torpidoses[1].GetComponent<Distanser>().DistanceToTarget & Torpidoses[2].GetComponent<Distanser>().DistanceToTarget < Torpidoses[0].GetComponent<Distanser>().DistanceToTarget & Torpidoses[2].GetComponent<Distanser>().DistanceToTarget < Torpidoses[3].GetComponent<Distanser>().DistanceToTarget){
					Torpidoses[2].GetComponent<TopidoLounser>().enabled = true;
					Torpidoses[2].GetComponent<TopidoLounser>().Active = true;
					
					Torpidoses[0].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[0].GetComponent<TopidoLounser>().Active = false;
					
					Torpidoses[1].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[1].GetComponent<TopidoLounser>().Active = false;

					Torpidoses[3].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[3].GetComponent<TopidoLounser>().Active = false;
				}
				if(Torpidoses[3].GetComponent<Distanser>().DistanceToTarget < Torpidoses[0].GetComponent<Distanser>().DistanceToTarget & Torpidoses[3].GetComponent<Distanser>().DistanceToTarget < Torpidoses[1].GetComponent<Distanser>().DistanceToTarget & Torpidoses[3].GetComponent<Distanser>().DistanceToTarget < Torpidoses[2].GetComponent<Distanser>().DistanceToTarget){
					Torpidoses[3].GetComponent<TopidoLounser>().enabled = true;
					Torpidoses[3].GetComponent<TopidoLounser>().Active = true;
					
					Torpidoses[0].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[0].GetComponent<TopidoLounser>().Active = false;
					
					Torpidoses[1].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[1].GetComponent<TopidoLounser>().Active = false;
					
					Torpidoses[2].GetComponent<TopidoLounser>().enabled = false;
					Torpidoses[2].GetComponent<TopidoLounser>().Active = false;
				}
			}
			if(EverActivePhasers.Count > 0){
				foreach (GameObject obj in EverActivePhasers) {
					obj.GetComponent<Phaser> ().enabled = true;
				}
			}
			if(EverActiveTorpidoses.Count > 0){
				foreach (GameObject obj in EverActiveTorpidoses) {
					obj.GetComponent<TopidoLounser> ().enabled = true;
					obj.GetComponent<TopidoLounser> ().Active = true;
				}
			}
			}
		}
	}
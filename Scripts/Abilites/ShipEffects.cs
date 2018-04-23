using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEffects : MonoBehaviour {
	public enum ShipEffect
	{
		WarpCoreDisable,
		OmegaDisable,
		WarpSensor
	}
	public ShipEffect Effect;

	public bool DestroyWithTime;
	public float DestroyTimer;


	private HealthModule _hp;
	// Use this for initialization
	void Start () {
		if (gameObject.GetComponent<HealthModule> ()) {
			_hp = gameObject.GetComponent<HealthModule> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (DestroyWithTime) {
			if (DestroyTimer > 0) {
				DestroyTimer -= Time.deltaTime;
			} else {
				Destroy (this);
			}
		}
		if (Effect == ShipEffect.WarpCoreDisable) {
			_hp.ActiveWarpEnging = true;
		}
		if (Effect == ShipEffect.OmegaDisable) {
			_hp.ActiveWarpEnging = true;
		}
		if (Effect == ShipEffect.WarpSensor) {
			
		}
	}

	void OnDestroy(){
		if (Effect == ShipEffect.WarpCoreDisable) {
			_hp.ActiveWarpEnging = false;
		}
		if (Effect == ShipEffect.OmegaDisable) {
			_hp.ActiveWarpEnging = false;
		}
		if (Effect == ShipEffect.WarpSensor) {
			
		}
	}
}

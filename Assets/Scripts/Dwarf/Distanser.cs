using UnityEngine;
using System.Collections;

public class Distanser : MonoBehaviour
{
	public float DistanceToTarget;
	private Stats ShipStat;
	private WeaponModule _wm;

	private bool Phaser;
	private bool Torpedo;

	private bool Ship;
	private bool Station;

	// Use this for initialization
	void Start()
	{
		if (gameObject.GetComponent<Phaser>())
		{
			Phaser = true;
		    if (gameObject.GetComponent<Phaser>().Owner.GetComponent<Stats>())
		    {
		        Ship = true;
		        ShipStat = gameObject.GetComponent<Phaser>().Owner.GetComponent<Stats>();
		    }
		    if (gameObject.GetComponent<Phaser>().Owner.GetComponent<Station>())
		    {
		        Station = true;
		        _wm = gameObject.GetComponent<Phaser>().Owner.GetComponent<WeaponModule>();
		    }
        }
		if (gameObject.GetComponent<TopidoLounser>())
		{
			Torpedo = true;
		    if (gameObject.GetComponent<TopidoLounser>().Owner.GetComponent<Stats>())
		    {
		        Ship = true;
		        ShipStat = gameObject.GetComponent<TopidoLounser>().Owner.GetComponent<Stats>();
		    }
		    if (gameObject.GetComponent<TopidoLounser>().Owner.GetComponent<Station>())
		    {
		        Station = true;
		        _wm = gameObject.GetComponent<TopidoLounser>().Owner.GetComponent<WeaponModule>();
		    }
        }
	}

	// Update is called once per frame
	void Update()
	{
		if (Ship)
		{
			if (ShipStat.targetTransform != null)
			{
				DistanceToTarget = Vector3.Distance(gameObject.transform.position, ShipStat.targetTransform.position);
			}
		}
		if (Station)
		{
			if (_wm.target != null)
			{
				DistanceToTarget = Vector3.Distance(gameObject.transform.position, _wm.target.transform.position);
			}
		}
	}
}
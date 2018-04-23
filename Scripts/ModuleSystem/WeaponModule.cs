using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModule : MonoBehaviour
{
	public GameObject target;

	public bool isAttack;

	public float radiuse;

	private Station _sb;
	private SensorModule _sm;

	public bool ImpulseSystemAttack;
	public bool LifeSupportSystemAttack;
	public bool PrimaryWeaponSystemAttack;
	public bool SensorsSystemAttack;
	public bool TractorBeamSystemAttack;
	public bool WarpEngingSystemAttack;
	public bool WarpCoreAttack;
	public bool SecondaryWeaponSystemAttack;
	// Use this for initialization
	void Start()
	{
		_sb = gameObject.GetComponent<Station>();
		_sm = gameObject.GetComponent<SensorModule>();
	}

	// Update is called once per frame
	void Update()
	{
		if (target != null)
		{
			if (Vector3.Distance(target.transform.position, transform.position) <= radiuse)
			{
				isAttack = true;
			}
			if (target == null)
			{
				isAttack = false;
			}
			if (Vector3.Distance(target.transform.position, transform.position) > radiuse)
			{
				isAttack = false;
				target = null;
			}
		}
		else
		{
			if (_sm.Target != null)
			{
				target = _sm.Target;
			}
		}
	}
}
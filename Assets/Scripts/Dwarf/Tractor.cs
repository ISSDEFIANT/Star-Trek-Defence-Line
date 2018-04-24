using UnityEngine;
using System.Collections;

public class Tractor : MonoBehaviour
{
	public GameObject Ship;
	public GameObject StarShipOwner;
	public GameObject Target;
	public float ReloadTime;
	public float ReloadTimer;
	[HideInInspector]
	public float Radius;
	public float TractorRadius;
	private Stats _st;
	public bool Use;
	public float Speed;

	public Lightbeam _lbObject;
	public Color32 OffColor;
	public Color32 OnColor;

	public float curColorAmount;

	public AudioSource OnEffect;
	public AudioSource OffEffect;
	public AudioSource WorkingEffect;

	private bool OnActive;

	private HealthModule _ohm;
	private float Tspeed;
	// Use this for initialization
	void Start()
	{
		_st = Ship.GetComponent<Stats>();
		_ohm = Ship.GetComponent<HealthModule>();

	}
	Vector3 lastPosition = Vector3.zero;
	void FixedUpdate()
	{
		if (Target != null)
		{
			Tspeed = (Target.transform.position - lastPosition).magnitude;
			lastPosition = Target.transform.position;
		}
	}
	// Update is called once per frame
	void Update()
	{
		if (Target != null)
		{
			Vector3 LookVector = (Target.transform.position - this.transform.position);

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(LookVector), 360);

			if (StarShipOwner != null)
			{
				Radius = TractorRadius + _ohm.ShipRadius + StarShipOwner.GetComponent<HealthModule>().ShipRadius;
			}
			else
			{
				Radius = TractorRadius + _ohm.ShipRadius;
			}
		}
		if (Target != null)
		{
			if (Vector3.Distance(gameObject.transform.position, Target.transform.position) > Radius)
			{
				Use = false;
				Target = null;
				if (StarShipOwner != null)
				{
					StarShipOwner.GetComponent<HealthModule>().Catched = false;
				}
				StarShipOwner = null;
			}
		}
		if (Use)
		{
			if (Target != null)
			{
				if (curColorAmount < 1)
				{
					curColorAmount += Time.deltaTime;
				}
				if (!WorkingEffect.isPlaying)
				{
					if (!OnActive)
					{
						OnEffect.Play();
						OnActive = true;
					}
					WorkingEffect.Play();
				}


				_lbObject.GenerateBeam();
				_lbObject.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.Lerp(OffColor, OnColor, curColorAmount));
				_lbObject.gameObject.SetActive(true);
				_lbObject.Length = Vector3.Distance(gameObject.transform.position, StarShipOwner.transform.position) + StarShipOwner.GetComponent<HealthModule>().ShipRadius;
				_lbObject.RadiusBottom = StarShipOwner.GetComponent<HealthModule>().ShipRadius*2;
				if (StarShipOwner != null)
				{
					if (Vector3.Distance(gameObject.transform.position, StarShipOwner.transform.position) < Radius - 7)
					{
						StarShipOwner.GetComponent<Rigidbody>().AddForce((Target.GetComponent<Rigidbody>().velocity * (Tspeed+2) * Target.GetComponent<Rigidbody>().mass) * -1);
					}
					if (Vector3.Distance(gameObject.transform.position, StarShipOwner.transform.position) > Radius - 7)
					{
						Vector3 LookVector = (StarShipOwner.transform.position - this.transform.position);
						StarShipOwner.GetComponent<Rigidbody>().AddForce(((LookVector * (Tspeed+1)) * (Target.GetComponent<Rigidbody>().mass / 2)) * -1);
					}
					if (Vector3.Distance(gameObject.transform.position, StarShipOwner.transform.position) > Radius - 5)
					{
						Vector3 LookVector = (StarShipOwner.transform.position - this.transform.position);
						StarShipOwner.GetComponent<Rigidbody>().AddForce(((LookVector * (Tspeed+1)) * (Target.GetComponent<Rigidbody>().mass)) * -1);
					}
					StarShipOwner.GetComponent<HealthModule>().Catched = true;
				}
				else
				{
					if (Vector3.Distance(gameObject.transform.position, Target.transform.position) < Radius - 7)
					{
						Target.GetComponent<Rigidbody>().AddForce((Target.GetComponent<Rigidbody>().velocity * (Tspeed+2) * Target.GetComponent<Rigidbody>().mass) * -1);
					}
					if (Vector3.Distance(gameObject.transform.position, Target.transform.position) > Radius - 7)
					{
						Vector3 LookVector = (Target.transform.position - this.transform.position);
						Target.GetComponent<Rigidbody>().AddForce(((LookVector * (Tspeed+1)) * (Target.GetComponent<Rigidbody>().mass / 2)) * -1);
					}
					if (Vector3.Distance(gameObject.transform.position, Target.transform.position) > Radius - 5)
					{
						Vector3 LookVector = (Target.transform.position - this.transform.position);
						Target.GetComponent<Rigidbody>().AddForce(((LookVector * (Tspeed+1)) * (Target.GetComponent<Rigidbody>().mass)) * -1);
					}
				}
			}
		}
		else
		{
			if (curColorAmount > 0)
			{
				curColorAmount -= Time.deltaTime;
			}

			if (WorkingEffect.isPlaying)
			{
				OffEffect.Play();
				WorkingEffect.Stop();
			}
			OnActive = false;

			Target = null;
			_lbObject.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.Lerp(OffColor, OnColor, curColorAmount));
			if (_lbObject.gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color") == OffColor)
			{
				_lbObject.gameObject.SetActive(false);
			}
		}
	}
}
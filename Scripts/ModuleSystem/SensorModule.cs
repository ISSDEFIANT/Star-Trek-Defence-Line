using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SensorModule : MonoBehaviour
{
	public float VisionRadius;

	public GameObject Target;

	private bool IsShip;
	private Stats _st;

	private bool IsStation;
	private Station _sb;

	public List<GameObject> VisibleObjectList;

	void Awake()
	{
		VisibleObjectList = new List<GameObject>();

		if (gameObject.GetComponent<Stats>())
		{
			_st = gameObject.GetComponent<Stats>();
			IsShip = true;
			IsStation = false;
		}
		if (gameObject.GetComponent<Station>())
		{
			_sb = gameObject.GetComponent<Station>();
			IsShip = false;
			IsStation = true;
		}
	}


	void Update()
	{
		if (VisibleObjectList.Count > 0)
		{
			List<GameObject> InvisibleShips = new List<GameObject>();
			foreach (GameObject obj in VisibleObjectList)
			{
				if (obj != null)
				{
					if (Vector3.Distance(gameObject.transform.position, obj.transform.position) > (VisionRadius + 3 + obj.GetComponent<HealthModule>().ShipRadius))
					{
						if (IsShip)
						{
							if (!_st.AI && !_st.Neutral)
							{
								if (obj != gameObject)
								{
									obj.GetComponent<ObjectTypeVisible>().IsVisible = false;
								}
							}
						}
						if (IsStation)
						{
							if (!_sb.AI && !_sb.Neutral)
							{
								if (obj != gameObject)
								{
									obj.GetComponent<ObjectTypeVisible>().IsVisible = false;
								}
							}
						}
						InvisibleShips.Add(obj);
					}
				}
				else
				{
					InvisibleShips.Add(obj);
				}
			}
			foreach (GameObject obj in InvisibleShips)
			{
				VisibleObjectList.Remove(obj);
			}
			InvisibleShips.Clear();
		}

		List<Collider> colls = Physics.OverlapSphere(transform.position, VisionRadius).ToList();

		foreach (Collider C in colls)
		{
			if (C != null)
			{
				if (C.GetComponent<HealthModule>())
				{
					if (VisibleObjectList.Count != 0)
					{
						if (!FindInVisibleObjectList(C.gameObject))
						{
							VisibleObjectList.Add(C.gameObject);
						}
					}
					else
					{
						VisibleObjectList.Add(C.gameObject);
					}


					HealthModule CurScanTarget = C.GetComponent<HealthModule>();

					if (IsShip)
					{
						if (!_st.AI && !_st.Neutral)
						{
							if (C.GetComponent<ObjectTypeVisible>())
							{
								C.GetComponent<ObjectTypeVisible>().IsVisible = true;
							}
							if (!CurScanTarget.Ship)
							{
								C.GetComponent<ObjectTypeVisible>().FirstFinded = true;
							}
						}

						if (!_st.AI && !_st.Neutral)
						{
							if (CurScanTarget.tag == "Enemy")
							{
								Target = C.gameObject;
							}
						}
						if (_st.AI)
						{
							if (CurScanTarget.tag == "Dwarf" || CurScanTarget.tag == "Freand")
							{
								Target = C.gameObject;
							}
						}
					}
					if (IsStation)
					{
						if (!_sb.AI && !_sb.Neutral)
						{
							if (C.GetComponent<ObjectTypeVisible>())
							{
								C.GetComponent<ObjectTypeVisible>().IsVisible = true;
							}
							if (!CurScanTarget.Ship)
							{
								C.GetComponent<ObjectTypeVisible>().FirstFinded = true;
							}
						}

						if (!_sb.AI && !_sb.Neutral)
						{
							if (CurScanTarget.tag == "Enemy")
							{
								Target = C.gameObject;
							}
						}
						if (_sb.AI)
						{
							if (CurScanTarget.tag == "Dwarf" || CurScanTarget.tag == "Freand")
							{
								Target = C.gameObject;
							}
						}
					}
				}
			}
		}
	}

	bool FindInVisibleObjectList(GameObject obj)
	{
		foreach (GameObject selObj in VisibleObjectList)
		{
			if (selObj == obj)
				return true;
		}
		return false;
	}
}
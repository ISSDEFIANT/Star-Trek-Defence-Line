using UnityEngine;
using System.Collections;

public class EnterSelectPlaneActive : MonoBehaviour
{
	public GameObject Owner;

	private Stats _st;
	private Station _sb;
	private MeshRenderer _mr;
	private LineRenderer _lr;

	private bool IsShip;
	private bool IsStation;
	// Use this for initialization
	void Start()
	{
		if (Owner.GetComponent<Stats>())
		{
			_st = Owner.GetComponent<Stats>();
			if (gameObject.GetComponent<MeshRenderer>())
			{
				_mr = gameObject.GetComponent<MeshRenderer>();
			}
			else
			{
				_mr = null;
			}
			if (gameObject.GetComponent<LineRenderer>())
			{
				_lr = gameObject.GetComponent<LineRenderer>();
			}
			else
			{
				_lr = null;
			}
			IsShip = true;
			IsStation = false;
		}
		if (Owner.GetComponent<Station>())
		{
			_sb = Owner.GetComponent<Station>();
			if (gameObject.GetComponent<MeshRenderer>())
			{
				_mr = gameObject.GetComponent<MeshRenderer>();
			}
			else
			{
				_mr = null;
			}
			if (gameObject.GetComponent<LineRenderer>())
			{
				_lr = gameObject.GetComponent<LineRenderer>();
			}
			else
			{
				_lr = null;
			}
			IsShip = false;
			IsStation = true;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (IsShip)
		{
			if (_st.ShadowProjectorActive)
			{
				if (_mr != null)
				{
					_mr.enabled = true;
				}
				if (_lr != null)
				{
					_lr.enabled = true;
				}
			}
			else
			{
				if (_mr != null)
				{
					_mr.enabled = false;
				}
				if (_lr != null)
				{
					_lr.enabled = false;
				}
			}
		}
		if (IsStation)
		{
			if (_sb.StationOutLineActive)
			{
				if (_mr != null)
				{
					_mr.enabled = true;
				}
				if (_lr != null)
				{
					_lr.enabled = true;
				}
			}
			else
			{
				if (_mr != null)
				{
					_mr.enabled = false;
				}
				if (_lr != null)
				{
					_lr.enabled = false;
				}
			}
		}
	}
}
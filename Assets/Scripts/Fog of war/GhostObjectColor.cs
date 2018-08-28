using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObjectColor : MonoBehaviour
{

	public GameObject Owner;
	public Color NeutralColor;

	private GameObject LateOwner;
	private MeshRenderer _mr;
	private PlayerColour _pc;

	private Stats _st;
	private bool IsShip;
	private Station _sb;
	private bool IsStation;

	private bool Check;
	private bool Paint;

	// Use this for initialization

	void Awake()
	{
		_mr = gameObject.GetComponent<MeshRenderer>();
		_pc = GameObject.FindGameObjectWithTag("MainUI").GetComponent<PlayerColour>();
		if (Owner.GetComponent<Stats>())
		{
			_st = Owner.GetComponent<Stats>();
			IsShip = true;
			IsStation = false;
		}
		if (Owner.GetComponent<Station>())
		{
			_sb = Owner.GetComponent<Station>();
			IsShip = false;
			IsStation = true;
		}
	}
	void Start()
	{
		NeutralColor = Color.grey;
		Check = true;
	}
	// Update is called once per frame
	void Update()
	{
		if (IsShip)
		{
			if (_st.Owner != LateOwner)
			{
				Check = true;
			}
			if (Check)
			{
				LateOwner = _st.Owner;
				Paint = true;

				Check = false;
			}
		}
		if (IsStation)
		{
			if (_sb.Owner != LateOwner)
			{
				Check = true;
			}
			if (Check)
			{
				LateOwner = _sb.Owner;
				Paint = true;

				Check = false;
			}
		}


		if (Paint)
		{
			if (IsShip)
			{
				if (_st.Owner != null)
				{
					if (_mr.material.GetColor("_InnerColor") != _st.Owner.GetComponent<GlobalAI>().PlayerColor)
					{
						_mr.material.SetColor("_InnerColor", _st.Owner.GetComponent<GlobalAI>().PlayerColor);
					}
				}
				else
				{
					if (!_st.Neutral)
					{
						if (_mr.material.GetColor("_InnerColor") != _pc.PlayerColor1)
						{
							_mr.material.SetColor("_InnerColor", _pc.PlayerColor1);
						}
					}
					else
					{
						if (_mr.material.GetColor("_InnerColor") != NeutralColor)
						{
							_mr.material.SetColor("_InnerColor", NeutralColor);
						}
					}
				}
			}
			if (IsStation)
			{
				if (_sb.Owner != null)
				{
					if (_mr.material.GetColor("_InnerColor") != _sb.Owner.GetComponent<GlobalAI>().PlayerColor)
					{
						_mr.material.SetColor("_InnerColor", _sb.Owner.GetComponent<GlobalAI>().PlayerColor);
					}
				}
				else
				{
					if (!_sb.Neutral)
					{
						if (_mr.material.GetColor("_InnerColor") != _pc.PlayerColor1)
						{
							_mr.material.SetColor("_InnerColor", _pc.PlayerColor1);
						}
					}
					else
					{
						if (_mr.material.GetColor("_InnerColor") != NeutralColor)
						{
							_mr.material.SetColor("_InnerColor", NeutralColor);
						}
					}
				}
			}
			Paint = false;
		}
		gameObject.GetComponent<MeshRenderer>().enabled = true;
	}
}
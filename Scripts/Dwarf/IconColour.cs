using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconColour : MonoBehaviour
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

	public bool OutLine;

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

			if (!OutLine)
			{
				if (Owner.GetComponent<ObjectTypeVisible>().FirstFinded)
				{
					gameObject.GetComponent<MeshRenderer>().enabled = true;
				}
			}
		}


		if (Paint)
		{
			if (!OutLine)
			{
				if (IsShip)
				{
					if (_st.Owner != null)
					{
						if (_mr.material.color != _st.Owner.GetComponent<GlobalAI>().PlayerColor)
						{
							_mr.material.color = _st.Owner.GetComponent<GlobalAI>().PlayerColor;
						}
					}
					else
					{
						if (!_st.Neutral)
						{
							if (_mr.material.color != _pc.PlayerColor1)
							{
								_mr.material.color = _pc.PlayerColor1;
							}
						}
						else
						{
							if (_mr.material.color != NeutralColor)
							{
								_mr.material.color = NeutralColor;
							}
						}
					}
				}
				if (IsStation)
				{
					if (_sb.Owner != null)
					{
						if (_mr.material.color != _sb.Owner.GetComponent<GlobalAI>().PlayerColor)
						{
							_mr.material.color = _sb.Owner.GetComponent<GlobalAI>().PlayerColor;
						}
					}
					else
					{
						if (!_sb.Neutral)
						{
							if (_mr.material.color != _pc.PlayerColor1)
							{
								_mr.material.color = _pc.PlayerColor1;
							}
						}
						else
						{
							if (_mr.material.color != NeutralColor)
							{
								_mr.material.color = NeutralColor;
							}
						}
					}
				}
				Paint = false;
			}
			else
			{
				if (IsStation)
				{
					if (_sb.Owner != null)
					{
						if (_mr.material.GetColor("_OutlineColor") != _sb.Owner.GetComponent<GlobalAI>().PlayerColor)
						{
							_mr.material.SetColor("_OutlineColor", _sb.Owner.GetComponent<GlobalAI>().PlayerColor);
						}
					}
					else
					{
						if (!_sb.Neutral)
						{
							if (_mr.material.GetColor("_OutlineColor") != _pc.PlayerColor1)
							{
								_mr.material.SetColor("_OutlineColor", _pc.PlayerColor1);
							}
						}
						else
						{
							if (_mr.material.GetColor("_OutlineColor") != NeutralColor)
							{
								_mr.material.SetColor("_OutlineColor", NeutralColor);
							}
						}
					}
				}
				Paint = false;
			}
		}
	}
}
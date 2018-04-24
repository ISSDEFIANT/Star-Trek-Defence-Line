using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Assimilated : MonoBehaviour
{
	public GameObject StarShip;
	public bool Matirials;
	public bool GLOW;
	public bool Light;
	public bool Effect;
	public Color StartColor;
	public Color AssimilatColor;
	public Material Material;
	private bool Ok;
	private bool MaterialReset;
	public List<Material> Materials;
	public Vector2 vec;

	public Gradient EffectStart = new Gradient();
	public Gradient EffectAssimilated = new Gradient();

	private Stats _st;
	private MeshRenderer _mr;
	private Light _l;

	private Station _sb;

	private bool isShip;
	private bool isStation;
	// Use this for initialization
	void Start()
	{
		if (StarShip.GetComponent<Stats>())
		{
			_st = StarShip.GetComponent<Stats>();
			isShip = true;
			isStation = false;
		}
		if (StarShip.GetComponent<Station>())
		{
			_sb = StarShip.GetComponent<Station>();
			isShip = false;
			isStation = true;
		}
		if (Matirials)
		{
			_mr = gameObject.GetComponent<MeshRenderer>();
			foreach (Material mat in _mr.materials)
			{
				Materials.Add(mat);
			}
		}
		if (Light)
		{
			_l = gameObject.GetComponent<Light>();
		}
	}
	void LateUpdate()
	{

	}
	// Update is called once per frame
	void Update()
	{
		if (isShip)
		{
			if (!_st.Assimilated)
			{
				Ok = false;
			}
			if (Matirials)
			{
				_mr.materials = Materials.ToArray();
			}
			if (_st.Assimilated)
			{
				if (Matirials)
				{
					if (!GLOW)
					{
						if (!Ok)
						{
							if (!GLOW)
							{
								vec = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));
							}
							Ok = true;
						}
						if (_mr.materials.Length == 1)
						{
							Material mat = Material;
							mat.mainTextureOffset = vec;
							Materials.Add(mat);
						}
						if (_mr.materials.Length >= 2)
						{
							Material mat = Material;
							mat.mainTextureOffset = vec;
							Materials[1] = mat;
						}
					}
					else
					{
						_mr.materials[0].color = AssimilatColor;
					}
				}
				if (Light)
				{
					_l.color = AssimilatColor;
				}
				if (Effect)
				{
					ParticleSystem Target = gameObject.GetComponent<ParticleSystem>();
					var col = Target.colorOverLifetime;
					col.enabled = true;
					col.color = EffectAssimilated;
				}
			}
			if (!_st.Assimilated)
			{
				Ok = false;
				if (Matirials)
				{
					if (GLOW)
					{
						_mr.materials[0].color = StartColor;
					}
					else
					{
						if (_mr.materials.Length == 2)
						{
							if (Materials[1] == Material)
							{
								Materials.RemoveAt(1);
							}
						}
						if (_mr.materials.Length >= 2)
						{
							if (Materials[1] == Material)
							{
								Materials[1] = Materials[0];
							}
						}
					}
				}
				if (Light)
				{
					_l.color = StartColor;
				}
				if (Effect)
				{
					ParticleSystem Target = gameObject.GetComponent<ParticleSystem>();
					var col = Target.colorOverLifetime;
					col.enabled = true;
					col.color = EffectStart;
				}
			}
		}

		if (isStation)
		{
			if (!_sb.Assimilated)
			{
				Ok = false;
			}
			if (Matirials)
			{
				_mr.materials = Materials.ToArray();
			}
			if (_sb.Assimilated)
			{
				if (Matirials)
				{
					if (!GLOW)
					{
						if (!Ok)
						{
							if (!GLOW)
							{
								vec = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));
							}
							Ok = true;
						}
						if (_mr.materials.Length == 1)
						{
							Material mat = Material;
							mat.mainTextureOffset = vec;
							Materials.Add(mat);
						}
						if (_mr.materials.Length >= 2)
						{
							Material mat = Material;
							mat.mainTextureOffset = vec;
							Materials[1] = mat;
						}
					}
					else
					{
						_mr.materials[0].color = AssimilatColor;
					}
				}
				if (Light)
				{
					_l.color = AssimilatColor;
				}
				if (Effect)
				{
					ParticleSystem Target = gameObject.GetComponent<ParticleSystem>();
					var col = Target.colorOverLifetime;
					col.enabled = true;
					col.color = EffectAssimilated;
				}
			}
			if (!_sb.Assimilated)
			{
				Ok = false;
				if (Matirials)
				{
					if (GLOW)
					{
						_mr.materials[0].color = StartColor;
					}
					else
					{
						if (_mr.materials.Length == 2)
						{
							if (Materials[1] == Material)
							{
								Materials.RemoveAt(1);
							}
						}
						if (_mr.materials.Length >= 2)
						{
							if (Materials[1] == Material)
							{
								Materials[1] = Materials[0];
							}
						}
					}
				}
				if (Light)
				{
					_l.color = StartColor;
				}
				if (Effect)
				{
					ParticleSystem Target = gameObject.GetComponent<ParticleSystem>();
					var col = Target.colorOverLifetime;
					col.enabled = true;
					col.color = EffectStart;
				}
			}
		}
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CtrlNum : MonoBehaviour
{
	public List<GameObject> Num1;
	public List<GameObject> Num2;
	public List<GameObject> Num3;
	public List<GameObject> Num4;
	public List<GameObject> Num5;
	public List<GameObject> Num6;
	public List<GameObject> Num7;
	public List<GameObject> Num8;
	public List<GameObject> Num9;
	public List<GameObject> Num0;

	private GlobalDB _GDB;

	private int ClickNumber;
	private int ClickCount;
	private float ClickDelay;
	// Use this for initialization
	void Start()
	{
		_GDB = gameObject.GetComponent<GlobalDB>();
		ClickDelay = 0.2f;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		foreach (GameObject obj in Num1)
		{
			obj.GetComponent<HealthModule>().Team1 = true;
		}
		foreach (GameObject obj in Num2)
		{
			obj.GetComponent<HealthModule>().Team2 = true;
		}
		foreach (GameObject obj in Num3)
		{
			obj.GetComponent<HealthModule>().Team3 = true;
		}
		foreach (GameObject obj in Num4)
		{
			obj.GetComponent<HealthModule>().Team4 = true;
		}
		foreach (GameObject obj in Num5)
		{
			obj.GetComponent<HealthModule>().Team5 = true;
		}
		foreach (GameObject obj in Num6)
		{
			obj.GetComponent<HealthModule>().Team6 = true;
		}
		foreach (GameObject obj in Num7)
		{
			obj.GetComponent<HealthModule>().Team7 = true;
		}
		foreach (GameObject obj in Num8)
		{
			obj.GetComponent<HealthModule>().Team8 = true;
		}
		foreach (GameObject obj in Num9)
		{
			obj.GetComponent<HealthModule>().Team9 = true;
		}
		foreach (GameObject obj in Num0)
		{
			obj.GetComponent<HealthModule>().Team0 = true;
		}
	}

	void Update()
	{
		GameObject Ccamera = GameObject.FindGameObjectWithTag("CAMERAMOVE");
		if (ClickCount > 0)
		{
			if (ClickDelay > 0)
			{
				ClickDelay -= Time.deltaTime;
			}
			else
			{
				ClickDelay = 0.2f;
				ClickCount = 0;
			}
		}
		if (ClickCount == 2)
		{
			if (ClickNumber == 1)
			{
				Ccamera.transform.position = new Vector3(Num1[0].transform.position.x, Ccamera.transform.position.y, Num1[0].transform.position.z);
			}
			if (ClickNumber == 2)
			{
				Ccamera.transform.position = new Vector3(Num2[0].transform.position.x, Ccamera.transform.position.y, Num2[0].transform.position.z);
			}
			if (ClickNumber == 3)
			{
				Ccamera.transform.position = new Vector3(Num3[0].transform.position.x, Ccamera.transform.position.y, Num3[0].transform.position.z);
			}
			if (ClickNumber == 4)
			{
				Ccamera.transform.position = new Vector3(Num4[0].transform.position.x, Ccamera.transform.position.y, Num4[0].transform.position.z);
			}
			if (ClickNumber == 5)
			{
				Ccamera.transform.position = new Vector3(Num5[0].transform.position.x, Ccamera.transform.position.y, Num5[0].transform.position.z);
			}
			if (ClickNumber == 6)
			{
				Ccamera.transform.position = new Vector3(Num6[0].transform.position.x, Ccamera.transform.position.y, Num6[0].transform.position.z);
			}
			if (ClickNumber == 7)
			{
				Ccamera.transform.position = new Vector3(Num7[0].transform.position.x, Ccamera.transform.position.y, Num7[0].transform.position.z);
			}
			if (ClickNumber == 8)
			{
				Ccamera.transform.position = new Vector3(Num8[0].transform.position.x, Ccamera.transform.position.y, Num8[0].transform.position.z);
			}
			if (ClickNumber == 9)
			{
				Ccamera.transform.position = new Vector3(Num9[0].transform.position.x, Ccamera.transform.position.y, Num9[0].transform.position.z);
			}
			if (ClickNumber == 0)
			{
				Ccamera.transform.position = new Vector3(Num0[0].transform.position.x, Ccamera.transform.position.y, Num0[0].transform.position.z);
			}
		}
		if (Input.GetKey(KeyCode.LeftAlt))
		{
			foreach (GameObject obj in _GDB.selectList)
			{
				foreach (GameObject obj1 in _GDB.dwarfList)
				{
					if (Input.GetKey("1"))
					{
						if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
						{
							Num1 = _GDB.selectList.ToList();
							obj1.GetComponent<HealthModule>().Team1 = false;
						}
						else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
						{
							Num1.Clear();
							Num1.Add(_GDB.activeObjectInterface);
						}
					}
					if (Input.GetKey("2"))
					{
						if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
						{
							Num2 = _GDB.selectList.ToList();
							obj1.GetComponent<HealthModule>().Team2 = false;
						}
						else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
						{
							Num2.Clear();
							Num2.Add(_GDB.activeObjectInterface);
						}
					}
					if (Input.GetKey("3"))
					{
						if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
						{
							Num3 = _GDB.selectList.ToList();
							obj1.GetComponent<HealthModule>().Team3 = false;
						}
						else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
						{
							Num3.Clear();
							Num3.Add(_GDB.activeObjectInterface);
						}
					}
					if (Input.GetKey("4"))
					{
						if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
						{
							Num4 = _GDB.selectList.ToList();
							obj1.GetComponent<HealthModule>().Team4 = false;
						}
						else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
						{
							Num4.Clear();
							Num4.Add(_GDB.activeObjectInterface);
						}
					}
					if (Input.GetKey("5"))
					{
						if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
						{
							Num5 = _GDB.selectList.ToList();
							obj1.GetComponent<HealthModule>().Team5 = false;
						}
						else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
						{
							Num5.Clear();
							Num5.Add(_GDB.activeObjectInterface);
						}
					}
					if (Input.GetKey("6"))
					{
						if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
						{
							Num6 = _GDB.selectList.ToList();
							obj1.GetComponent<HealthModule>().Team6 = false;
						}
						else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
						{
							Num6.Clear();
							Num6.Add(_GDB.activeObjectInterface);
						}
					}
					if (Input.GetKey("7"))
					{
						if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
						{
							Num7 = _GDB.selectList.ToList();
							obj1.GetComponent<HealthModule>().Team7 = false;
						}
						else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
						{
							Num7.Clear();
							Num7.Add(_GDB.activeObjectInterface);
						}
					}
					if (Input.GetKey("8"))
					{
						if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
						{
							Num8 = _GDB.selectList.ToList();
							obj1.GetComponent<HealthModule>().Team8 = false;
						}
						else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
						{
							Num8.Clear();
							Num8.Add(_GDB.activeObjectInterface);
						}
					}
					if (Input.GetKey("9"))
					{
						if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
						{
							Num9 = _GDB.selectList.ToList();
							obj1.GetComponent<HealthModule>().Team9 = false;
						}
						else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
						{
							Num9.Clear();
							Num9.Add(_GDB.activeObjectInterface);
						}
					}
					if (Input.GetKey("0"))
					{
						if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
						{
							Num0 = _GDB.selectList.ToList();
							obj1.GetComponent<HealthModule>().Team0 = false;
						}
						else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
						{
							Num0.Clear();
							Num0.Add(_GDB.activeObjectInterface);
						}
					}
				}
			}
		}
		else
		{
			if (Input.GetKeyDown("1"))
			{
				if (Num1.Count > 0 && Num1[0].GetComponent<Stats>())
				{
					foreach (GameObject obj in Num1)
					{
						_GDB.selectList = Num1.ToList();
						obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
						obj.GetComponent<Stats>().BoxSelected = true;
						obj.GetComponent<Stats>().WasSelect = true;
						obj.GetComponent<Stats>().isSelect = true;
						if (!gameObject.GetComponent<AudioSource>().isPlaying)
						{
							gameObject.GetComponent<AudioSource>().clip = Num1[0].GetComponent<Captan>().CurSelect[Random.Range(0, Num1[0].GetComponent<Captan>().CurSelect.Count)];
							gameObject.GetComponent<AudioSource>().Play();
						}
					}
				}
				if (Num1.Count > 0 && !Num1[0].GetComponent<Stats>())
				{
					_GDB.selectList.Clear();
					if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
					_GDB.activeObjectInterface = Num1[0];
					Num1[0].GetComponent<HealthModule>().TeamActivate();
				}
				ClickCount += 1;
				ClickNumber = 1;
			}
			if (Input.GetKeyDown("2"))
			{
				if (Num2.Count > 0 && Num2[0].GetComponent<Stats>())
				{
					foreach (GameObject obj in Num2)
					{
						_GDB.selectList = Num2.ToList();
						obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
						obj.GetComponent<Stats>().BoxSelected = true;
						obj.GetComponent<Stats>().WasSelect = true;
						obj.GetComponent<Stats>().isSelect = true;
						if (!gameObject.GetComponent<AudioSource>().isPlaying)
						{
							gameObject.GetComponent<AudioSource>().clip = Num2[0].GetComponent<Captan>().CurSelect[Random.Range(0, Num2[0].GetComponent<Captan>().CurSelect.Count)];
							gameObject.GetComponent<AudioSource>().Play();
						}
					}
				}
				if (Num2.Count > 0 && !Num2[0].GetComponent<Stats>())
				{
					_GDB.selectList.Clear();
					if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
					_GDB.activeObjectInterface = Num2[0];
					Num2[0].GetComponent<HealthModule>().TeamActivate();
				}
				ClickCount += 1;
				ClickNumber = 2;
			}
			if (Input.GetKeyDown("3"))
			{
				if (Num3.Count > 0 && Num3[0].GetComponent<Stats>())
				{
					foreach (GameObject obj in Num3)
					{
						_GDB.selectList = Num3.ToList();
						obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
						obj.GetComponent<Stats>().BoxSelected = true;
						obj.GetComponent<Stats>().WasSelect = true;
						obj.GetComponent<Stats>().isSelect = true;
						if (!gameObject.GetComponent<AudioSource>().isPlaying)
						{
							gameObject.GetComponent<AudioSource>().clip = Num3[0].GetComponent<Captan>().CurSelect[Random.Range(0, Num3[0].GetComponent<Captan>().CurSelect.Count)];
							gameObject.GetComponent<AudioSource>().Play();
						}
					}
				}
				if (Num3.Count > 0 && !Num3[0].GetComponent<Stats>())
				{
					_GDB.selectList.Clear();
					if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
					_GDB.activeObjectInterface = Num3[0];
					Num3[0].GetComponent<HealthModule>().TeamActivate();
				}
				ClickCount += 1;
				ClickNumber = 3;
			}
			if (Input.GetKeyDown("4"))
			{
				if (Num4.Count > 0 && Num4[0].GetComponent<Stats>())
				{
					foreach (GameObject obj in Num4)
					{
						_GDB.selectList = Num4.ToList();
						obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
						obj.GetComponent<Stats>().BoxSelected = true;
						obj.GetComponent<Stats>().WasSelect = true;
						obj.GetComponent<Stats>().isSelect = true;
						if (!gameObject.GetComponent<AudioSource>().isPlaying)
						{
							gameObject.GetComponent<AudioSource>().clip = Num4[0].GetComponent<Captan>().CurSelect[Random.Range(0, Num4[0].GetComponent<Captan>().CurSelect.Count)];
							gameObject.GetComponent<AudioSource>().Play();
						}
					}
				}
				if (Num4.Count > 0 && !Num4[0].GetComponent<Stats>())
				{
					_GDB.selectList.Clear();
					if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
					_GDB.activeObjectInterface = Num4[0];
					Num4[0].GetComponent<HealthModule>().TeamActivate();
				}
				ClickCount += 1;
				ClickNumber = 4;
			}
			if (Input.GetKeyDown("5"))
			{
				if (Num5.Count > 0 && Num5[0].GetComponent<Stats>())
				{
					foreach (GameObject obj in Num5)
					{
						_GDB.selectList = Num5.ToList();
						obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
						obj.GetComponent<Stats>().BoxSelected = true;
						obj.GetComponent<Stats>().WasSelect = true;
						obj.GetComponent<Stats>().isSelect = true;
						if (!gameObject.GetComponent<AudioSource>().isPlaying)
						{
							gameObject.GetComponent<AudioSource>().clip = Num5[0].GetComponent<Captan>().CurSelect[Random.Range(0, Num5[0].GetComponent<Captan>().CurSelect.Count)];
							gameObject.GetComponent<AudioSource>().Play();
						}
					}
				}
				if (Num5.Count > 0 && !Num5[0].GetComponent<Stats>())
				{
					_GDB.selectList.Clear();
					if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
					_GDB.activeObjectInterface = Num5[0];
					Num5[0].GetComponent<HealthModule>().TeamActivate();
				}
				ClickCount += 1;
				ClickNumber = 5;
			}
			if (Input.GetKeyDown("6"))
			{
				if (Num6.Count > 0 && Num6[0].GetComponent<Stats>())
				{
					foreach (GameObject obj in Num6)
					{
						_GDB.selectList = Num6.ToList();
						obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
						obj.GetComponent<Stats>().BoxSelected = true;
						obj.GetComponent<Stats>().WasSelect = true;
						obj.GetComponent<Stats>().isSelect = true;
						if (!gameObject.GetComponent<AudioSource>().isPlaying)
						{
							gameObject.GetComponent<AudioSource>().clip = Num6[0].GetComponent<Captan>().CurSelect[Random.Range(0, Num6[0].GetComponent<Captan>().CurSelect.Count)];
							gameObject.GetComponent<AudioSource>().Play();
						}
					}
				}
				if (Num6.Count > 0 && !Num6[0].GetComponent<Stats>())
				{
					_GDB.selectList.Clear();
					if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
					_GDB.activeObjectInterface = Num6[0];
					Num6[0].GetComponent<HealthModule>().TeamActivate();
				}
				ClickCount += 1;
				ClickNumber = 6;
			}
			if (Input.GetKeyDown("7"))
			{
				if (Num7.Count > 0 && Num7[0].GetComponent<Stats>())
				{
					foreach (GameObject obj in Num7)
					{
						_GDB.selectList = Num7.ToList();
						obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
						obj.GetComponent<Stats>().BoxSelected = true;
						obj.GetComponent<Stats>().WasSelect = true;
						obj.GetComponent<Stats>().isSelect = true;
						if (!gameObject.GetComponent<AudioSource>().isPlaying)
						{
							gameObject.GetComponent<AudioSource>().clip = Num7[0].GetComponent<Captan>().CurSelect[Random.Range(0, Num7[0].GetComponent<Captan>().CurSelect.Count)];
							gameObject.GetComponent<AudioSource>().Play();
						}
					}
				}
				if (Num7.Count > 0 && !Num7[0].GetComponent<Stats>())
				{
					_GDB.selectList.Clear();
					if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
					_GDB.activeObjectInterface = Num7[0];
					Num7[0].GetComponent<HealthModule>().TeamActivate();
				}
				ClickCount += 1;
				ClickNumber = 7;
			}
			if (Input.GetKeyDown("8"))
			{
				if (Num8.Count > 0 && Num8[0].GetComponent<Stats>())
				{
					foreach (GameObject obj in Num8)
					{
						_GDB.selectList = Num8.ToList();
						obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
						obj.GetComponent<Stats>().BoxSelected = true;
						obj.GetComponent<Stats>().WasSelect = true;
						obj.GetComponent<Stats>().isSelect = true;
						if (!gameObject.GetComponent<AudioSource>().isPlaying)
						{
							gameObject.GetComponent<AudioSource>().clip = Num8[0].GetComponent<Captan>().CurSelect[Random.Range(0, Num8[0].GetComponent<Captan>().CurSelect.Count)];
							gameObject.GetComponent<AudioSource>().Play();
						}
					}
				}
				if (Num8.Count > 0 && !Num8[0].GetComponent<Stats>())
				{
					_GDB.selectList.Clear();
					if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
					_GDB.activeObjectInterface = Num8[0];
					Num8[0].GetComponent<HealthModule>().TeamActivate();
				}
				ClickCount += 1;
				ClickNumber = 8;
			}
			if (Input.GetKeyDown("9"))
			{
				if (Num9.Count > 0 && Num9[0].GetComponent<Stats>())
				{
					foreach (GameObject obj in Num9)
					{
						_GDB.selectList = Num9.ToList();
						obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
						obj.GetComponent<Stats>().BoxSelected = true;
						obj.GetComponent<Stats>().WasSelect = true;
						obj.GetComponent<Stats>().isSelect = true;
						if (!gameObject.GetComponent<AudioSource>().isPlaying)
						{
							gameObject.GetComponent<AudioSource>().clip = Num9[0].GetComponent<Captan>().CurSelect[Random.Range(0, Num9[0].GetComponent<Captan>().CurSelect.Count)];
							gameObject.GetComponent<AudioSource>().Play();
						}
					}
				}
				if (Num9.Count > 0 && !Num9[0].GetComponent<Stats>())
				{
					_GDB.selectList.Clear();
					if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
					_GDB.activeObjectInterface = Num9[0];
					Num9[0].GetComponent<HealthModule>().TeamActivate();
				}
				ClickCount += 1;
				ClickNumber = 9;
			}
			if (Input.GetKeyDown("0"))
			{
				if (Num0.Count > 0 && Num0[0].GetComponent<Stats>())
				{
					foreach (GameObject obj in Num0)
					{
						_GDB.selectList = Num0.ToList();
						obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
						obj.GetComponent<Stats>().BoxSelected = true;
						obj.GetComponent<Stats>().WasSelect = true;
						obj.GetComponent<Stats>().isSelect = true;
						if (!gameObject.GetComponent<AudioSource>().isPlaying)
						{
							gameObject.GetComponent<AudioSource>().clip = Num0[0].GetComponent<Captan>().CurSelect[Random.Range(0, Num0[0].GetComponent<Captan>().CurSelect.Count)];
							gameObject.GetComponent<AudioSource>().Play();
						}
					}
				}
				if (Num0.Count > 0 && !Num0[0].GetComponent<Stats>())
				{
					_GDB.selectList.Clear();
					if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
					_GDB.activeObjectInterface = Num0[0];
					Num0[0].GetComponent<HealthModule>().TeamActivate();
				}
				ClickCount += 1;
				ClickNumber = 0;
			}
		}
	}
}
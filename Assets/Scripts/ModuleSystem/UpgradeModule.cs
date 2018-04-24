using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeModule : MonoBehaviour
{
	public int lvl;

	public int[] costUp;
	public int MaxLvl;

	public GameObject[] LevelModels;

	private Station _sbm;

	private GlobalDB _GDB;
	private Select _SEL;
	// Use this for initialization
	void Start()
	{
		_sbm = gameObject.GetComponent<Station>();
		_GDB = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalDB>();
		_SEL = GameObject.FindGameObjectWithTag("MainUI").GetComponent<Select>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Upgrade()
	{		if (lvl < MaxLvl)
		{
			if (!_sbm.AI && !_sbm.FreandAI)
			{
				if (_GDB.Dilithium >= costUp[lvl]) {
					_GDB.Dilithium -= costUp[lvl];
					LevelModels[lvl].SetActive(true);
					LevelModels[lvl-1].SetActive(false);
					lvl++;
				}
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIsBuildAnalisyngScript : MonoBehaviour
{
	public bool IsFederationStarBase;
	public bool IsFederationMiningStation;
	public bool IsFederationDock1;
	public bool IsFederationDock2;
	public bool IsFederationSciStation;
	public bool IsFederationDefenceStation;
	public bool IsFederationTradingStation;

	public bool IsBorgNexus;
	public bool IsBorgMiningStation;
	public bool IsBorgDock1;
	public bool IsBorgDock2;
	public bool IsBorgSciStation;
	public bool IsBorgDefenceStation;
	public bool IsBorgTorpedoTurret;
	public bool IsBorgCutterTurret;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (IsFederationStarBase)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsFederationStarBase = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsFederationStarBase = true;
			}
		}
		if (IsFederationMiningStation)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsFederationMiningStation = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsFederationMiningStation = true;
			}
		}
		if (IsFederationDock1)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsFederationDock1 = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsFederationDock1 = true;
			}
		}
		if (IsFederationDock2)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsFederationDock2 = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsFederationDock2 = true;
			}
		}
		if (IsFederationSciStation)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsFederationSciStation = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsFederationSciStation = true;
			}
		}
		if (IsFederationDefenceStation)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsFederationDefenceStation = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsFederationDefenceStation = true;
			}
		}
		if (IsFederationTradingStation)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsFederationTradingStation = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsFederationTradingStation = true;
			}
		}

		if (IsBorgNexus)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsBorgNexus = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsBorgNexus = true;
			}
		}
		if (IsBorgMiningStation)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsBorgMiningStation = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsBorgMiningStation = true;
			}
		}
		if (IsBorgDock1)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsBorgDock1 = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsBorgDock1 = true;
			}
		}
		if (IsBorgDock2)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsBorgDock2 = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsBorgDock2 = true;
			}
		}
		if (IsBorgSciStation)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsBorgSciStation = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsBorgSciStation = true;
			}
		}
		if (IsBorgDefenceStation)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsBorgDefenceStation = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsBorgDefenceStation = true;
			}
		}
		if (IsBorgTorpedoTurret)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsBorgTorpedoTurret = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsBorgTorpedoTurret = true;
			}
		}
		if (IsBorgCutterTurret)
		{
			Station scr = gameObject.GetComponent<Station>();
			if (!scr.AI && !scr.FreandAI && !scr.Neutral && !scr.NeutralAgrass)
			{
				GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().IsBorgCutterTurret = true;
			}
			else if (scr.AI && scr.FreandAI)
			{
				scr.Owner.GetComponent<GlobalLockingSystem>().IsBorgCutterTurret = true;
			}
		}
	}
	void OnDestroy()
	{
		if (GameObject.FindGameObjectWithTag("MainUI") != null)
		{
			GameObject.FindGameObjectWithTag("MainUI").GetComponent<GlobalLockingSystem>().Check();
		}
	}
}
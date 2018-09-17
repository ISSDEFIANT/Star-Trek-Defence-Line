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

    private GameObject Ccamera = GameObject.FindGameObjectWithTag("CAMERAMOVE");
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
		    switch (ClickNumber)
		    {
		        case 1:
		            MoveCameraToFleet(Num1);
                    break;
		        case 2:
		            MoveCameraToFleet(Num2);
                    break;
		        case 3:
		            MoveCameraToFleet(Num3);
                    break;
		        case 4:
		            MoveCameraToFleet(Num4);
                    break;
		        case 5:
		            MoveCameraToFleet(Num5);
                    break;
		        case 6:
		            MoveCameraToFleet(Num6);
                    break;
		        case 7:
		            MoveCameraToFleet(Num7);
                    break;
		        case 8:
		            MoveCameraToFleet(Num8);
                    break;
		        case 9:
		            MoveCameraToFleet(Num9);
                    break;
		        case 0:
		            MoveCameraToFleet(Num0);
                    break;
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
                        ResetTeams(obj, obj1, 1);
                    }
                    if (Input.GetKey("2"))
					{
					    ResetTeams(obj, obj1, 2);
                    }
					if (Input.GetKey("3"))
					{
					    ResetTeams(obj, obj1, 3);
                    }
					if (Input.GetKey("4"))
					{
					    ResetTeams(obj, obj1, 4);
                    }
					if (Input.GetKey("5"))
					{
					    ResetTeams(obj, obj1, 5);
                    }
					if (Input.GetKey("6"))
					{
					    ResetTeams(obj, obj1, 6);
                    }
					if (Input.GetKey("7"))
					{
					    ResetTeams(obj, obj1, 7);
                    }
					if (Input.GetKey("8"))
					{
					    ResetTeams(obj, obj1, 8);
                    }
					if (Input.GetKey("9"))
					{
					    ResetTeams(obj, obj1, 9);
                    }
					if (Input.GetKey("0"))
					{
					    ResetTeams(obj, obj1, 0);
                    }
				}
			}
		}
		else
		{
			if (Input.GetKeyDown("1"))
            {
                FleetCorrection(Num1, 1);
            }
            if (Input.GetKeyDown("2"))
			{
			    FleetCorrection(Num2, 2);
            }
			if (Input.GetKeyDown("3"))
			{
			    FleetCorrection(Num3, 3);
            }
			if (Input.GetKeyDown("4"))
			{
			    FleetCorrection(Num4, 4);
            }
			if (Input.GetKeyDown("5"))
			{
			    FleetCorrection(Num5, 5);
            }
			if (Input.GetKeyDown("6"))
			{
			    FleetCorrection(Num6, 6);
            }
			if (Input.GetKeyDown("7"))
			{
			    FleetCorrection(Num7, 7);
            }
			if (Input.GetKeyDown("8"))
			{
			    FleetCorrection(Num8, 8);
            }
			if (Input.GetKeyDown("9"))
			{
			    FleetCorrection(Num9, 9);
            }
			if (Input.GetKeyDown("0"))
			{
			    FleetCorrection(Num0, 0);
            }
		}
	}

    private void MoveCameraToFleet(List<GameObject> Fleet)
    {
        Ccamera.transform.position = new Vector3(Fleet[0].transform.position.x, Ccamera.transform.position.y, Fleet[0].transform.position.z);
    }

    private void ResetTeams(GameObject obj, GameObject obj1, int ClickNum)
    {
        if (_GDB.selectList.Count > 0 && _GDB.activeObjectInterface == null)
        {
            switch (ClickNum)
            {
                case 1:
                    Num1 = _GDB.selectList.ToList();
                    break;
                case 2:
                    Num2 = _GDB.selectList.ToList();
                    break;
                case 3:
                    Num3 = _GDB.selectList.ToList();
                    break;
                case 4:
                    Num4 = _GDB.selectList.ToList();
                    break;
                case 5:
                    Num5 = _GDB.selectList.ToList();
                    break;
                case 6:
                    Num6 = _GDB.selectList.ToList();
                    break;
                case 7:
                    Num7 = _GDB.selectList.ToList();
                    break;
                case 8:
                    Num8 = _GDB.selectList.ToList();
                    break;
                case 9:
                    Num9 = _GDB.selectList.ToList();
                    break;
                case 0:
                    Num0 = _GDB.selectList.ToList();
                    break;
            }

            if (ClickNum != 1)
            {
                if (FindInFleetList(obj, Num1)) Num1.Remove(obj);
            }

            if (ClickNum != 2)
            {
                if (FindInFleetList(obj, Num2)) Num2.Remove(obj);
            }

            if (ClickNum != 3)
            {
                if (FindInFleetList(obj, Num3)) Num3.Remove(obj);
            }

            if (ClickNum != 4)
            {
                if (FindInFleetList(obj, Num4)) Num4.Remove(obj);
            }

            if (ClickNum != 5)
            {
                if (FindInFleetList(obj, Num5)) Num5.Remove(obj);
            }

            if (ClickNum != 6)
            {
                if (FindInFleetList(obj, Num6)) Num6.Remove(obj);
            }

            if (ClickNum != 7)
            {
                if (FindInFleetList(obj, Num7)) Num7.Remove(obj);
            }

            if (ClickNum != 8)
            {
                if (FindInFleetList(obj, Num8)) Num8.Remove(obj);
            }

            if (ClickNum != 9)
            {
                if (FindInFleetList(obj, Num9)) Num9.Remove(obj);
            }

            if (ClickNum != 0)
            {
                if (FindInFleetList(obj, Num0)) Num0.Remove(obj);
            }

            obj1.GetComponent<HealthModule>().ResetTeam();
        }
        else if (_GDB.selectList.Count == 0 && _GDB.activeObjectInterface != null)
        {
            switch (ClickNum)
            {
                case 1:
                    Num1.Clear();
                    Num1.Add(_GDB.activeObjectInterface);
                    break;
                case 2:
                    Num2.Clear();
                    Num2.Add(_GDB.activeObjectInterface);
                    break;
                case 3:
                    Num3.Clear();
                    Num3.Add(_GDB.activeObjectInterface);
                    break;
                case 4:
                    Num4.Clear();
                    Num4.Add(_GDB.activeObjectInterface);
                    break;
                case 5:
                    Num5.Clear();
                    Num5.Add(_GDB.activeObjectInterface);
                    break;
                case 6:
                    Num6.Clear();
                    Num6.Add(_GDB.activeObjectInterface);
                    break;
                case 7:
                    Num7.Clear();
                    Num7.Add(_GDB.activeObjectInterface);
                    break;
                case 8:
                    Num8.Clear();
                    Num8.Add(_GDB.activeObjectInterface);
                    break;
                case 9:
                    Num9.Clear();
                    Num9.Add(_GDB.activeObjectInterface);
                    break;
                case 0:
                    Num0.Clear();
                    Num0.Add(_GDB.activeObjectInterface);
                    break;
            }
        }
    }

    private void FleetCorrection(List<GameObject> CorrectionFleet, int ClickNum)
    {
        if (CorrectionFleet.Count > 0 && CorrectionFleet[0].GetComponent<Stats>())
        {
            foreach (GameObject obj in CorrectionFleet)
            {
                _GDB.selectList = CorrectionFleet.ToList();
                obj.GetComponent<Stats>().Proector.GetComponent<MeshRenderer>().enabled = true;
                obj.GetComponent<Stats>().BoxSelected = true;
                obj.GetComponent<Stats>().WasSelect = true;
                obj.GetComponent<Stats>().isSelect = true;
                if (!gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    gameObject.GetComponent<AudioSource>().clip = CorrectionFleet[0].GetComponent<Captan>().CurCap.Select[Random.Range(0, CorrectionFleet[0].GetComponent<Captan>().CurCap.Select.Count)];
                    gameObject.GetComponent<AudioSource>().Play();
                }
            }
        }
        if (Num1.Count > 0 && !CorrectionFleet[0].GetComponent<Stats>())
        {
            _GDB.selectList.Clear();
            if (_GDB.activeObjectInterface != null) _GDB.deactivationInterface();
            _GDB.activeObjectInterface = CorrectionFleet[0];
            CorrectionFleet[0].GetComponent<HealthModule>().TeamActivate();
        }
        ClickCount += 1;
        ClickNumber = ClickNum;
    }

    bool FindInFleetList(GameObject obj, List<GameObject> Fleet)
	{
		foreach (GameObject selObj in Fleet)
		{
			if (selObj == obj)
				return true;
		}
		return false;
	}
}
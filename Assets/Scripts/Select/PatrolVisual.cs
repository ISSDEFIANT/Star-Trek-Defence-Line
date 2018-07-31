using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatrolVisual : MonoBehaviour {
	public List<Vector3> PatrolWay;

	public List<GameObject> Points;
	public GameObject PointPref;

	public LineRenderer PatrolLine;

	private Select _sel;
	private GlobalDB _GDB;
	private MoveComponent _CSMC;
	// Use this for initialization
	void Start () {
		_sel = gameObject.GetComponent<Select>();
		_GDB = gameObject.GetComponent<GlobalDB>();
	}

	// Update is called once per frame
	void Update()
	{
		if (_GDB.selectList.Count == 1)
		{			_CSMC = _GDB.selectList[0].GetComponent<MoveComponent>();

			PatrolWay = _CSMC.PatrolWay.ToList();

			if (PatrolWay.Count > 0)
			{
				if (Points.Count < PatrolWay.Count)
				{					GameObject inst = Instantiate(PointPref, Vector3.zero, Quaternion.Euler(Vector3.zero));
					Points.Add(inst);
				}

				PatrolLine.gameObject.SetActive(true);
				PatrolLine.positionCount = PatrolWay.Count;
				PatrolLine.SetPositions(PatrolWay.ToArray());

				if (Points.Count > 0)
				{
					for (int i = 0; i < Points.Count; i++)
					{
						if (i <= PatrolWay.Count)
						{
							Points[i].transform.position = PatrolWay[i];
							Points[i].SetActive(true);
						}
						else
						{
							Points[i].SetActive(false);
						}
					}
				}
			}
		}
		else
		{			foreach (GameObject obj in Points)
			{				obj.SetActive(false);
				PatrolLine.gameObject.SetActive(false);
			}
		}
	}
}
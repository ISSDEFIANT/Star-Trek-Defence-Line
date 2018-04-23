using UnityEngine;
using System.Collections;

public class SP : MonoBehaviour
{
	public GameObject Shop;
	public GameObject Target;
	public GameObject flag;

	private ShipBuildModule _sbm;
	// Use this for initialization
	void Start()
	{
		_sbm = Shop.GetComponent<ShipBuildModule>();
	}

	// Update is called once per frame
	void Update()
	{
		flag = _sbm.curFlag;
	}
}
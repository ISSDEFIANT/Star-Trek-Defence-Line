using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveComponent : MonoBehaviour
{
	public bool CUBETYPE;

	private Stats _st;
	private ActiveState _as;
	private Rigidbody _rb;
	private HealthModule _hm;
	public bool Move;
	public bool Warp;

	public Vector3 TargetVector;

	public float MovementSpeed;
	public float MaxMovementSpeed;

	public float MaxAcceleration;
	public float BrakingDistance;

	public float RotationSpeed;
	public float MaxRotationSpeed;

	public float WarpMovementSpeed;

	private Vector3 CurTargetVector;


	// Use this for initialization
	void Awake()
	{
		TargetVector = gameObject.transform.position;
		Move = false;

		_st = gameObject.GetComponent<Stats>();
		_as = gameObject.GetComponent<ActiveState>();
		_rb = gameObject.GetComponent<Rigidbody>();
		_hm = gameObject.GetComponent<HealthModule>();
	}

	// Update is called once per frame
	void LateUpdate()
	{

	}
	void Update()
	{
		TargetVector = _st.targetVector;

		CurTargetVector = TargetVector;
		if (Vector3.Distance(gameObject.transform.position, CurTargetVector) > 1)
		{
			MoveProccess();
		}
	}
	public void Stop()
	{
		gameObject.GetComponent<Stats>().instruction = Stats.enInstruction.idle;
		gameObject.GetComponent<Stats>().targetVector = gameObject.transform.position;
		TargetVector = gameObject.transform.position;
		Move = false;
	}
	public void Movement(Vector3 MovementPosition)
	{
		gameObject.GetComponent<Stats>().targetVector = MovementPosition;
	}
	public void MoveProccess()
	{
		Move = true;
		if (Vector3.Distance(gameObject.transform.position, CurTargetVector) > BrakingDistance)
		{
			if (_rb.velocity.magnitude < MaxMovementSpeed)
			{
				_rb.AddForce(transform.forward * MaxAcceleration * _rb.mass);
			}
		}
		else
		{
			if (_rb.velocity.magnitude > 0)
			{
				float f = (_rb.mass * (_rb.velocity.magnitude * _rb.velocity.magnitude)) / 2 * BrakingDistance;
				_rb.AddForce(-1 * transform.forward * f);
			}
		}
	}
}
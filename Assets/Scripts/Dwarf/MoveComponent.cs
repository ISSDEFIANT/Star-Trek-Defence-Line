using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveComponent : MonoBehaviour
{
	public bool CUBETYPE;
	public bool CubeDetectSystem;

	public Vector3 TargetVector;

	public float RotationSpeed;
	public float RotationAcceleration;
public float RotationBraking;

	public float MovementSpeed;
	public float MaxAcceleration;
	public float BrakingDistance;

	public float WarpMovementSpeed;

	public float CurRotSpeed;

	private Stats _st;
	private ActiveState _as;
	private Rigidbody _rb;
	private HealthModule _hm;
	public bool Move;

	[HideInInspector]
	public bool StabilizationActive;
	[HideInInspector]
	public Vector3 RotationVelocity;
	[HideInInspector]
	public float angleL;
	[HideInInspector]
	public float angleYL;
	[HideInInspector]
	public float angleZL;

	[HideInInspector]
	public GameObject LeftSensor;
	[HideInInspector]
	public GameObject RightSensor;

	[HideInInspector]
	public GameObject UpSensor;
	[HideInInspector]
	public GameObject DownSensor;

	[HideInInspector]
	public GameObject ForwardSensor;
	[HideInInspector]
	public GameObject BackSensor;

	private float Timer;
	[HideInInspector]
	public Vector3 OldTargetVector;

	[HideInInspector]
	public Vector3 impuls;
	[HideInInspector]
	public bool active;
	[HideInInspector]
	public bool Lock;

	[HideInInspector]
	public Vector3 localangularvelocity;

	[HideInInspector]
	public bool Maneuvers;

	[HideInInspector]
	public float ManeuversTimer;

	public bool ForwardBlocked;
	public bool LeftBlocked;
	public bool RightBlocked;

	public bool UpBlocked;
	public bool DownBlocked;

	[HideInInspector]
	private bool LeftStab;
	[HideInInspector]
	private bool RightStab;

	[HideInInspector]
	private bool UpStab;
	[HideInInspector]
	private bool DownStab;
[HideInInspector]
	public GameObject ForwardRayStart;
[HideInInspector]
	public GameObject LeftRayStart;
[HideInInspector]
	public GameObject RightRayStart;
[HideInInspector]
	public GameObject UpRayStart;
[HideInInspector]
	public GameObject DownRayStart;

	public string CollisionObject;

	[HideInInspector]
	public Vector3 RayCastBugDestroyer;

	public bool Warp;

	private float ZStabTimer = 0;
	private bool ZStabActive;

	[HideInInspector]
	public bool ShipIsBuilt;
	[HideInInspector]
	public float MovementTimer;

	public bool SensorBlocking;



	[HideInInspector]
	public bool Left;
	[HideInInspector]
	public float ChangeTimer = 10;

	private Vector3 CurTargetVector;

	private float m_OriginalDrag;
	private float m_OriginalAngularDrag;

	private float m_OriginalMaxAngular;

	// Use this for initialization
	void Awake()
	{
		TargetVector = gameObject.transform.position;
		OldTargetVector = gameObject.transform.position;
		Move = false;

		_st = gameObject.GetComponent<Stats>();
		_as = gameObject.GetComponent<ActiveState>();
		_rb = gameObject.GetComponent<Rigidbody>();
		_hm = gameObject.GetComponent<HealthModule>();

		m_OriginalDrag = _rb.drag;
		m_OriginalAngularDrag = _rb.angularDrag;
		m_OriginalMaxAngular = _rb.maxAngularVelocity;

		LeftSensor = new GameObject();
		RightSensor = new GameObject();
		UpSensor = new GameObject();
		DownSensor = new GameObject();
		ForwardSensor = new GameObject();
		BackSensor = new GameObject();

		LeftSensor.transform.parent = gameObject.transform;
		RightSensor.transform.parent = gameObject.transform;
		UpSensor.transform.parent = gameObject.transform;
		DownSensor.transform.parent = gameObject.transform;
		ForwardSensor.transform.parent = gameObject.transform;
		BackSensor.transform.parent = gameObject.transform;

		LeftSensor.transform.localPosition = new Vector3(-1, 0, 0);
		RightSensor.transform.localPosition = new Vector3(1, 0, 0);
		UpSensor.transform.localPosition = new Vector3(0, 1, 0);
		DownSensor.transform.localPosition = new Vector3(0, -1, 0);
		ForwardSensor.transform.localPosition = new Vector3(0, 0, 1);
		BackSensor.transform.localPosition = new Vector3(0, 0, -1);

		LeftSensor.name = "LeftSensor";
		RightSensor.name = "RightSensor";
		UpSensor.name = "UpSensor";
		DownSensor.name = "DownSensor";
		ForwardSensor.name = "ForwardSensor";
		BackSensor.name = "BackSensor";

		Timer = 3;

		if (ForwardRayStart == null)
		{
			ForwardRayStart = gameObject;
		}
		if (LeftRayStart == null)
		{
			LeftRayStart = gameObject;
		}
		if (RightRayStart == null)
		{
			RightRayStart = gameObject;
		}
		if (UpRayStart == null)
		{
			UpRayStart = gameObject;
		}
		if (DownRayStart == null)
		{
			DownRayStart = gameObject;
		}
	}

	// Update is called once per frame
	void LateUpdate()
	{

	}
	void FixedUpdate()
	{
		CurRotSpeed = _rb.angularVelocity.magnitude;

		localangularvelocity = transform.InverseTransformDirection(_rb.angularVelocity).normalized * _rb.angularVelocity.magnitude;

		if (!_st.IsFix)
		{
			if (gameObject.GetComponent<ActiveState>().Build)
			{
				ShipIsBuilt = false;
			}
		}
		if (!ShipIsBuilt && !SensorBlocking)
		{
			FedSensorSystem();
		}

		TargetVector = _st.targetVector;
		CurTargetVector = TargetVector;

		if (!Warp)
		{
			if (Move)
			{
                ShipRotation(RotationSpeed, RotationAcceleration);
				ForwardMovement(MovementSpeed, MaxAcceleration);
			}
			if (Vector3.Distance(gameObject.transform.position, TargetVector) > 1)
			{
				Move = true;
			}
			else
			{
				Move = false;
			}
			//_rb.AddRelativeTorque(new Vector3(0, 0, RotationVelocity.z) * _rb.mass);
		}
		if (Warp)
		{
			if (Move)
			{
				if (!ForwardBlocked && Vector3.Distance(RightSensor.transform.position, TargetVector) < Vector3.Distance(LeftSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance(LeftSensor.transform.position, TargetVector) < Vector3.Distance(RightSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance(UpSensor.transform.position, TargetVector) < Vector3.Distance(DownSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance(DownSensor.transform.position, TargetVector) < Vector3.Distance(UpSensor.transform.position, TargetVector) + (0.1f * RotationSpeed))
				{
					if (Vector3.Dot(_rb.velocity, transform.forward) < WarpMovementSpeed * MovementSpeed)
					{
						_rb.velocity += gameObject.transform.forward * (MaxAcceleration * WarpMovementSpeed);
					}
				}
				else
				{
					if (!LeftStab && !RightStab && !UpStab && !DownStab)
					{
						_rb.maxAngularVelocity = CurRotSpeed;
					}
					if (Vector3.Distance(gameObject.transform.position, TargetVector) > 1)
					{
						ForwardMovement(MovementSpeed, MaxAcceleration);
						if (angleL != 0 || angleYL != 0)
						{
							_rb.drag = Vector3.Dot(_rb.velocity, transform.forward);
						}
						else
						{
							_rb.drag = 0.5f;
						}
						if (angleL > 0)
						{
							if (angleL != 0)
							{
								if (_rb.angularVelocity.y < CurRotSpeed)
								{
									_rb.AddRelativeTorque(new Vector3(0, RotationVelocity.y, 0) * _rb.mass);
									//_rb.angularVelocity += new Vector3 (0, RotationVelocity.y, 0);
								}
							}
						}
						else
						{
							if (angleL != 0)
							{
								if (_rb.angularVelocity.y > -CurRotSpeed)
								{
									_rb.AddRelativeTorque(new Vector3(0, RotationVelocity.y, 0) * _rb.mass);
								}
							}
						}
						_rb.AddRelativeTorque(new Vector3(RotationVelocity.x, 0, 0) * _rb.mass);
					}
					else
					{
						ForwardMovement(MovementSpeed, MaxAcceleration);
						//Move = false;
					}
					_rb.AddRelativeTorque(new Vector3(0, 0, RotationVelocity.z) * _rb.mass);
				}
			}
		}
		if (active)
		{
			_rb.AddRelativeTorque(impuls * _rb.mass);
			active = false;
		}
		if (ZStabTimer > 0)
		{
			ZStabTimer -= Time.deltaTime;
		}
		else
		{
			ZStabActive = !ZStabActive;
			if (ZStabActive)
			{
				ZStabTimer = 6;
			}
			else
			{
				ZStabTimer = 3;
			}
		}
		if (ZStabActive)
		{
		//	gameObject.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(gameObject.transform.localRotation.eulerAngles), Quaternion.Euler(new Vector3(gameObject.transform.localRotation.eulerAngles.x, gameObject.transform.localRotation.eulerAngles.y, 0)), CurRotSpeed / 100);
		}
		if (!Move && !Warp)
		{
			if (gameObject.transform.position.y > 2 || gameObject.transform.position.y < -4)
			{
				Movement(new Vector3(gameObject.transform.position.x, -2.942455f, gameObject.transform.position.z));
			}
		}
	}
	public void FullStop()
	{
		_rb.velocity = Vector3.Slerp(_rb.velocity, new Vector3(0, 0, 0), 0.1f);
	}
	public void RotationStop()
	{
		_rb.angularVelocity = Vector3.Slerp(_rb.angularVelocity, new Vector3(0, 0, 0), 0.1f * RotationSpeed);
	}
	public void Stop()
	{
		gameObject.GetComponent<Stats>().instruction = Stats.enInstruction.idle;
		gameObject.GetComponent<Stats>().targetVector = gameObject.transform.position;
		FullStop();
		RotationStop();
		Move = false;
	}

	void ForwardMovement(float MaxSpeed, float MaxAccelerationIE)
	{
		if (Vector3.Distance(gameObject.transform.position, CurTargetVector) > BrakingDistance)
		{
			if (Vector3.Dot(_rb.velocity, transform.forward) < MaxSpeed)
			{
				_rb.AddForce(transform.forward * MaxAccelerationIE * _rb.mass);
			}
		}
		else
		{
			if (Vector3.Dot(_rb.velocity, transform.forward) > 0)
			{
				float f = (_rb.mass * (Vector3.Dot(_rb.velocity, transform.forward) * Vector3.Dot(_rb.velocity, transform.forward))) / 2 * BrakingDistance;
				_rb.AddForce(-1 * transform.forward * f);
			}
		}
	}

	void ShipRotation(float Power, float RAcceleration)
	{
		_rb.maxAngularVelocity = Power;


		var Factor = Vector3.Dot(transform.forward, _rb.velocity.normalized);
		Factor *= Factor;
		var newVelocity = Vector3.Lerp(_rb.velocity, transform.forward * Vector3.Dot(_rb.velocity, transform.forward), Factor * Vector3.Dot(_rb.velocity, transform.forward) * Time.deltaTime);
		_rb.velocity = newVelocity;

		if (!Maneuvers)
		{
			LeftBlocked = false;
			RightBlocked = false;
			UpBlocked = false;
			DownBlocked = false;

			if (Vector3.Distance(LeftSensor.transform.position, TargetVector) > Vector3.Distance(RightSensor.transform.position, TargetVector))
			{
				angleL = 1;
			}
			if (Vector3.Distance(RightSensor.transform.position, TargetVector) > Vector3.Distance(LeftSensor.transform.position, TargetVector))
			{
				angleL = -1;
			}
			if (Vector3.Distance(UpSensor.transform.position, TargetVector) < Vector3.Distance(DownSensor.transform.position, TargetVector))
			{
				angleYL = -1;
			}
			if (Vector3.Distance(UpSensor.transform.position, TargetVector) > Vector3.Distance(DownSensor.transform.position, TargetVector))
			{
				angleYL = 1;
			}
		}
		else
		{
			if (!RightBlocked)
			{
				angleL = -1;
			}
			else
			{
				if (!LeftBlocked)
				{
					angleL = 1;
				}
			}
			if (!UpBlocked)
			{
				angleYL = 1;
			}
			else
			{
				if (!DownBlocked)
				{
					angleYL = -1;
				}
			}
			if (ManeuversTimer > 0)
			{
				ManeuversTimer -= Time.deltaTime;
			}
			else
			{
				if (!RightBlocked && !LeftBlocked && !UpBlocked && !DownBlocked && !ForwardBlocked)
				{
					Maneuvers = false;
				}
			}
		}
		Vector3 targetDir = TargetVector - transform.position;
		float Wedge = Vector3.Angle(targetDir, transform.forward);
		Debug.Log(Wedge);
		if (Wedge < RotationBraking)
		{
			_rb.AddRelativeTorque(new Vector3(angleYL * -RAcceleration, angleL * -RAcceleration, 0) * _rb.mass);
		}
		else
		{
			_rb.AddRelativeTorque(new Vector3(angleYL * RAcceleration, angleL * RAcceleration, 0) * _rb.mass);
		}
	}

	void FedSensorSystem()
	{
		RaycastHit _rh;
		RaycastHit _rh2;
		RaycastHit _rh3;

		RaycastHit _rh4;
		RaycastHit _rh5;

		RaycastHit _rh6;
		RaycastHit _rh7;

		Vector3 ForwardR = gameObject.transform.TransformDirection(Vector3.forward);
		Vector3 LeftR = gameObject.transform.TransformDirection(Vector3.left);
		Vector3 RightR = gameObject.transform.TransformDirection(Vector3.right);

		Vector3 FLeftR = Quaternion.AngleAxis(-25, transform.up) * gameObject.transform.TransformDirection(Vector3.forward);
		Vector3 FRightR = Quaternion.AngleAxis(25, transform.up) * gameObject.transform.TransformDirection(Vector3.forward);

		Vector3 UpR = gameObject.transform.TransformDirection(Vector3.up);
		Vector3 DownR = gameObject.transform.TransformDirection(Vector3.down);

		if (Physics.Raycast(ForwardRayStart.transform.position + RayCastBugDestroyer, ForwardR, out _rh, (_hm.ShipRadius + (MovementSpeed * 2))))
		{
			if (_rh.transform.gameObject != gameObject)
			{
				if (_rh.transform.gameObject.GetComponent<HealthModule>())
				{
					ForwardBlocked = true;
					Maneuvers = true;
					ManeuversTimer = 0.2f;
					CollisionObject = _rh.transform.name;
					Debug.DrawRay(ForwardRayStart.transform.position + RayCastBugDestroyer, ForwardR * (_hm.ShipRadius + (MovementSpeed * 2)), Color.red);
				}
				else
				{
					Debug.DrawRay(ForwardRayStart.transform.position + RayCastBugDestroyer, ForwardR * (_hm.ShipRadius + (MovementSpeed * 2)), Color.yellow);
				}
			}
			else
			{
				Debug.DrawRay(ForwardRayStart.transform.position + RayCastBugDestroyer, ForwardR * (_hm.ShipRadius + (MovementSpeed * 2)), Color.yellow);
			}
		}
		else
		{
			ForwardBlocked = false;
			Debug.DrawRay(ForwardRayStart.transform.position + RayCastBugDestroyer, ForwardR * (_hm.ShipRadius + (MovementSpeed * 2)), Color.green);
		}
		if (Physics.Raycast(LeftRayStart.transform.position + RayCastBugDestroyer, LeftR, out _rh2, (_hm.ShipRadius + 3)))
		{
			if (_rh2.transform.gameObject != gameObject)
			{
				if (_rh2.transform.gameObject.GetComponent<HealthModule>())
				{
					LeftBlocked = true;
					//Maneuvers = true;
					//ManeuversTimer = 0.2f;
					CollisionObject = _rh2.transform.name;
					Debug.DrawRay(LeftRayStart.transform.position + RayCastBugDestroyer, LeftR * (_hm.ShipRadius + 3), Color.red);
				}
				else
				{
					Debug.DrawRay(LeftRayStart.transform.position + RayCastBugDestroyer, LeftR * (_hm.ShipRadius + 3), Color.yellow);
				}
			}
			else
			{
				LeftBlocked = false;
				Debug.DrawRay(LeftRayStart.transform.position + RayCastBugDestroyer, LeftR * (_hm.ShipRadius + 3), Color.yellow);
			}
		}
		else
		{
			Debug.DrawRay(LeftRayStart.transform.position + RayCastBugDestroyer, LeftR * (_hm.ShipRadius + 3), Color.green);
		}

		if (Physics.Raycast(RightRayStart.transform.position + RayCastBugDestroyer, RightR, out _rh3, (_hm.ShipRadius + 3)))
		{
			if (_rh3.transform.gameObject != gameObject)
			{
				if (_rh3.transform.gameObject.GetComponent<HealthModule>())
				{
					RightBlocked = true;
					//Maneuvers = true;
					//ManeuversTimer = 0.2f;
					CollisionObject = _rh3.transform.name;
					Debug.DrawRay(RightRayStart.transform.position + RayCastBugDestroyer, RightR * (_hm.ShipRadius + 3), Color.red);
				}
				else
				{
					Debug.DrawRay(RightRayStart.transform.position + RayCastBugDestroyer, RightR * (_hm.ShipRadius + 3), Color.yellow);
				}
			}
			else
			{
				Debug.DrawRay(RightRayStart.transform.position + RayCastBugDestroyer, RightR * (_hm.ShipRadius + 3), Color.yellow);
			}
		}
		else
		{
			RightBlocked = false;
			Debug.DrawRay(RightRayStart.transform.position + RayCastBugDestroyer, RightR * (_hm.ShipRadius + 3), Color.green);
		}

		if (Physics.Raycast(LeftRayStart.transform.position + RayCastBugDestroyer, FLeftR, out _rh4, (_hm.ShipRadius + 3)))
		{
			if (_rh4.transform.gameObject != gameObject)
			{
				if (_rh4.transform.gameObject.GetComponent<HealthModule>())
				{
					LeftBlocked = true;
					Maneuvers = true;
					ManeuversTimer = 0.2f;
					CollisionObject = _rh4.transform.name;
					Debug.DrawRay(LeftRayStart.transform.position + RayCastBugDestroyer, FLeftR * (_hm.ShipRadius + 3), Color.red);
				}
				else
				{
					Debug.DrawRay(LeftRayStart.transform.position + RayCastBugDestroyer, FLeftR * (_hm.ShipRadius + 3), Color.yellow);
				}
			}
			else
			{
				Debug.DrawRay(LeftRayStart.transform.position + RayCastBugDestroyer, FLeftR * (_hm.ShipRadius + 3), Color.yellow);
			}
		}
		else
		{
			//LeftBlocked = false;
			Debug.DrawRay(LeftRayStart.transform.position + RayCastBugDestroyer, FLeftR * (_hm.ShipRadius + 3), Color.green);
		}
		if (Physics.Raycast(RightRayStart.transform.position + RayCastBugDestroyer, FRightR, out _rh5, (_hm.ShipRadius + 3)))
		{
			if (_rh5.transform.gameObject != gameObject)
			{
				if (_rh5.transform.gameObject.GetComponent<HealthModule>())
				{
					RightBlocked = true;
					Maneuvers = true;
					ManeuversTimer = 0.2f;
					CollisionObject = _rh5.transform.name;
					Debug.DrawRay(RightRayStart.transform.position + RayCastBugDestroyer, FRightR * (_hm.ShipRadius + 3), Color.red);
				}
				else
				{
					Debug.DrawRay(RightRayStart.transform.position + RayCastBugDestroyer, FRightR * (_hm.ShipRadius + 3), Color.yellow);
				}
			}
			else
			{
				Debug.DrawRay(RightRayStart.transform.position + RayCastBugDestroyer, FRightR * (_hm.ShipRadius + 3), Color.yellow);
			}
		}
		else
		{
			//RightBlocked = false;
			Debug.DrawRay(RightRayStart.transform.position + RayCastBugDestroyer, FRightR * (_hm.ShipRadius + 3), Color.green);
		}

		if (Physics.Raycast(UpRayStart.transform.position + RayCastBugDestroyer, UpR, out _rh6, (_hm.ShipRadius + 3)))
		{
			if (_rh6.transform.gameObject != gameObject)
			{
				if (_rh6.transform.gameObject.GetComponent<HealthModule>())
				{
					UpBlocked = true;
					Maneuvers = true;
					ManeuversTimer = 0.2f;
					CollisionObject = _rh6.transform.name;
					Debug.DrawRay(UpRayStart.transform.position + RayCastBugDestroyer, UpR * (_hm.ShipRadius + 3), Color.red);
				}
				else
				{
					Debug.DrawRay(UpRayStart.transform.position + RayCastBugDestroyer, UpR * (_hm.ShipRadius + 3), Color.yellow);
				}
			}
			else
			{
				Debug.DrawRay(UpRayStart.transform.position + RayCastBugDestroyer, UpR * (_hm.ShipRadius + 3), Color.yellow);
			}
		}
		else
		{
			UpBlocked = false;
			Debug.DrawRay(UpRayStart.transform.position + RayCastBugDestroyer, UpR * (_hm.ShipRadius + 3), Color.green);
		}
		if (Physics.Raycast(DownRayStart.transform.position + RayCastBugDestroyer, DownR, out _rh7, (_hm.ShipRadius + 3)))
		{
			if (_rh7.transform.gameObject != gameObject)
			{
				if (_rh7.transform.gameObject.GetComponent<HealthModule>())
				{
					DownBlocked = true;
					Maneuvers = true;
					ManeuversTimer = 0.2f;
					CollisionObject = _rh7.transform.name;
					Debug.DrawRay(DownRayStart.transform.position + RayCastBugDestroyer, DownR * (_hm.ShipRadius + 3), Color.red);
				}
				else
				{
					Debug.DrawRay(DownRayStart.transform.position + RayCastBugDestroyer, DownR * (_hm.ShipRadius + 3), Color.yellow);
				}
			}
			else
			{
				Debug.DrawRay(DownRayStart.transform.position + RayCastBugDestroyer, DownR * (_hm.ShipRadius + 3), Color.yellow);
			}
		}
		else
		{
			DownBlocked = false;
			Debug.DrawRay(DownRayStart.transform.position + RayCastBugDestroyer, DownR * (_hm.ShipRadius + 3), Color.green);
		}
	}

	public void Movement(Vector3 MovementPosition)
	{
		gameObject.GetComponent<Stats>().targetVector = MovementPosition;
	}
}
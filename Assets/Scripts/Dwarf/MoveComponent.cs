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

	public float MovementSpeed;
	public float MaxAcceleration;

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

	private float RollAngle;
	private float PitchAngle;
	private float ForwardSpeed;

	public float pitchInput = 0;
	public float yawInput = 0;
	public float rollInput = 0;
	private float m_BankedTurnAmount;

	public List<GameObject> CurFleet;

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
	void Update()
	{
		if (_hm.Team0 || _hm.Team1 || _hm.Team2 || _hm.Team3 || _hm.Team4 || _hm.Team5 || _hm.Team6 || _hm.Team7 || _hm.Team8 || _hm.Team9)
		{
			SetCurFleet(false, null);
		}

		if (CurFleet.Count > 0)
		{
			if (CurFleet[0].GetComponent<MoveComponent>().CurFleet != CurFleet)
			{
				CurFleet.RemoveAt(0);
			}

			if (_st.GuartTarget != null)
			{				CurFleet.Clear();
			}
		}
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
				CalculateInput(TargetVector);
				ClampInputs();
				CalculateRollAndPitchAngles();
				ShipRotation(RotationSpeed, RotationAcceleration);
				ForwardMovement(MovementSpeed, MaxAcceleration);
			}
			else
			{
				StabilizeShip();
			}
		}
		else
		{
			if (!ForwardBlocked && Vector3.Distance(RightSensor.transform.position, TargetVector) < Vector3.Distance(LeftSensor.transform.position, TargetVector) + (RotationSpeed * (1 / _rb.angularVelocity.magnitude)) && Vector3.Distance(LeftSensor.transform.position, TargetVector) < Vector3.Distance(RightSensor.transform.position, TargetVector) + (RotationSpeed * (1 / _rb.angularVelocity.magnitude)) && Vector3.Distance(UpSensor.transform.position, TargetVector) < Vector3.Distance(DownSensor.transform.position, TargetVector) + (RotationSpeed * (1 / _rb.angularVelocity.magnitude)) && Vector3.Distance(DownSensor.transform.position, TargetVector) < Vector3.Distance(UpSensor.transform.position, TargetVector) + (RotationSpeed * (1 / _rb.angularVelocity.magnitude)))
			{
				if (Vector3.Dot(_rb.velocity, transform.forward) < WarpMovementSpeed * MovementSpeed)
				{
					_rb.velocity += gameObject.transform.forward * (MaxAcceleration * WarpMovementSpeed);
				}
				CalculateInput(TargetVector);
				ClampInputs();
				CalculateRollAndPitchAngles();
				ShipRotation(1, 1);
			}
			else
			{
				CalculateInput(TargetVector);
				ClampInputs();
				CalculateRollAndPitchAngles();
				ShipRotation(RotationSpeed, RotationAcceleration);
				ForwardMovement(MovementSpeed, MaxAcceleration);
			}
		}
		if (Vector3.Distance(gameObject.transform.position, TargetVector) > _hm.ShipRadius)
		{
			Move = true;
		}
		else
		{
			Move = false;
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
		if (_rb.velocity.magnitude > MovementSpeed)
		{
			_rb.velocity = _rb.velocity.normalized * MovementSpeed;
		}

		Vector3 TargetVec = TargetVector - transform.position;

		if (Vector3.Distance(gameObject.transform.position, CurTargetVector) > _hm.ShipRadius + (MaxSpeed * MaxSpeed / 2) / MaxAccelerationIE)
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
				_rb.AddForce(transform.forward * MaxAccelerationIE * _rb.mass * -1);
			}
			else
			{
				float amount = 0;

				Vector3 startvec = Vector3.zero;
				if (amount < 1)
				{
					amount += Time.deltaTime / MovementSpeed;
				}
				else
				{					amount = 1;
				}
				if (startvec == Vector3.zero)
				{
					startvec = transform.position;
				}
				gameObject.transform.position = Vector3.Lerp(startvec, TargetVector, amount);
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color32(0, 255, 0, 100);
		Gizmos.DrawSphere(TargetVector, 5);	}
	void ShipRotation(float Power, float RAcceleration)
	{
		var Factor = Vector3.Dot(transform.forward, _rb.velocity.normalized);
		Factor *= Factor;
		var newVelocity = Vector3.Lerp(_rb.velocity, transform.forward * Vector3.Dot(_rb.velocity, transform.forward), Factor * Vector3.Dot(_rb.velocity, transform.forward) * Time.deltaTime);
		_rb.velocity = newVelocity;

		_rb.rotation = Quaternion.Slerp(_rb.rotation, Quaternion.LookRotation(_rb.velocity, transform.up), 0.02f * Time.deltaTime);

		var torque = Vector3.zero;

		m_BankedTurnAmount = Mathf.Sin(RollAngle);
		_rb.angularDrag = Mathf.Abs(_rb.angularVelocity.magnitude - ((pitchInput + rollInput) / 2));

		torque += pitchInput * transform.right;
		torque += yawInput * 0.1f * transform.up;
		torque += -rollInput * transform.forward;
		torque += m_BankedTurnAmount * 0.5f * transform.up;
		_rb.AddTorque(torque * ForwardSpeed * Factor * _rb.mass);
	}

	private void ClampInputs()
	{
		rollInput = Mathf.Clamp(rollInput, -1, 1);
		pitchInput = Mathf.Clamp(pitchInput, -1, 1);
		yawInput = Mathf.Clamp(yawInput, -1, 1);	}

	private void CalculateRollAndPitchAngles()
	{
		// Calculate roll & pitch angles
		// Calculate the flat forward direction (with no y component).
		var flatForward = transform.forward;
		flatForward.y = 0;
		// If the flat forward vector is non-zero (which would only happen if the plane was pointing exactly straight upwards)
		if (flatForward.sqrMagnitude > 0)
		{
			flatForward.Normalize();
			// calculate current pitch angle
			var localFlatForward = transform.InverseTransformDirection(flatForward);
			PitchAngle = Mathf.Atan2(localFlatForward.y, localFlatForward.z);
			// calculate current roll angle
			var flatRight = Vector3.Cross(Vector3.up, flatForward);
			var localFlatRight = transform.InverseTransformDirection(flatRight);
			RollAngle = Mathf.Atan2(localFlatRight.y, localFlatRight.x);
		}
	}

	void CalculateInput(Vector3 Target)
	{
		Vector3 targetPos = Target;

		Vector3 localTarget = transform.InverseTransformPoint(targetPos);
		float targetAngleYaw = Mathf.Atan2(localTarget.x, localTarget.z);
		float targetAnglePitch = -Mathf.Atan2(localTarget.y, localTarget.z);


		targetAnglePitch = Mathf.Clamp(targetAnglePitch, -RotationSpeed * Mathf.Deg2Rad, RotationSpeed * Mathf.Deg2Rad);

		float changePitch = targetAnglePitch - PitchAngle;

		pitchInput = changePitch * RotationAcceleration;

		float desiredRoll = Mathf.Clamp(targetAngleYaw, -RotationSpeed * Mathf.Deg2Rad, RotationSpeed * Mathf.Deg2Rad);

		yawInput = targetAngleYaw;
		rollInput = -(RollAngle - desiredRoll) * RotationAcceleration * 2;

		var localVelocity = transform.InverseTransformDirection(_rb.velocity);
		ForwardSpeed = Mathf.Max(0, localVelocity.z);

		float currentSpeedEffect = 1 + ForwardSpeed * 0.01f;
		rollInput *= currentSpeedEffect;
		pitchInput *= currentSpeedEffect;
		yawInput *= currentSpeedEffect;	}

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

	void StabilizeShip()
	{
		float Amount = 0;
		if (Amount < 1)
		{
			Amount += Time.deltaTime / RotationAcceleration;
		}
		_rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, Vector3.zero, Amount);
		_rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, Amount);
		_rb.angularDrag = 0;

		Vector3 StartRot = Vector3.zero;

		if (StartRot == Vector3.zero)
		{
			StartRot = gameObject.transform.localRotation.eulerAngles;
		}

		if (_st.GuartTarget != null)
		{
			gameObject.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(StartRot), Quaternion.Euler(new Vector3(0, _st.GuartTarget.transform.localEulerAngles.y, 0)), Amount);
		}
		else
		{
			if (CurFleet.Count <= 0)
			{
				gameObject.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(StartRot), Quaternion.Euler(new Vector3(0, StartRot.y, 0)), Amount);
			}
			else
			{
				gameObject.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(StartRot), Quaternion.Euler(new Vector3(0, CurFleet[0].transform.localRotation.eulerAngles.y, 0)), Amount);
			}
		}
	}


	public void SetCurFleet(bool NotStateFleet, List<GameObject> NotStateShips)
	{
		if (!NotStateFleet)
		{
			CtrlNum _CNS = FindObjectOfType<CtrlNum>().GetComponent<CtrlNum>();

			if (_hm.Team0)
			{
				CurFleet = _CNS.Num0;
			}
			if (_hm.Team1)
			{
				CurFleet = _CNS.Num1;
			}
			if (_hm.Team2)
			{
				CurFleet = _CNS.Num2;
			}
			if (_hm.Team3)
			{
				CurFleet = _CNS.Num3;
			}
			if (_hm.Team4)
			{
				CurFleet = _CNS.Num4;
			}
			if (_hm.Team5)
			{
				CurFleet = _CNS.Num5;
			}
			if (_hm.Team6)
			{
				CurFleet = _CNS.Num6;
			}
			if (_hm.Team7)
			{
				CurFleet = _CNS.Num7;
			}
			if (_hm.Team8)
			{
				CurFleet = _CNS.Num8;
			}
			if (_hm.Team9)
			{
				CurFleet = _CNS.Num9;
			}
		}
		else
		{			CurFleet = NotStateShips;
		}
	}
}
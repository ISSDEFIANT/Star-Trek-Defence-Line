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

	public float NormalSoloMoveSpeed;
	public float MaxAcceleration;

	public float WarpMovementSpeed;

	public float CurRotSpeed;

	private Stats _st;
	private Rigidbody _rb;
	private HealthModule _hm;
	public bool Move;

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
	[HideInInspector]
	public float ForwardSpeed;

	public float pitchInput = 0;
	public float yawInput = 0;
	public float rollInput = 0;
	private float m_BankedTurnAmount;

	public List<GameObject> CurFleet;

	public List<Vector3> PatrolWay;

	public bool InPatrol;

	public int PatrolCurTarget = 0;

    private float StabAmount = 0;

    private bool IsStoping;
    private float StopAmount = 0;
    private float SpeedMoment = 0;

    // Use this for initialization
    void Awake()
	{
		TargetVector = gameObject.transform.position;
		OldTargetVector = gameObject.transform.position;
		Move = false;

		_st = gameObject.GetComponent<Stats>();
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

		LeftSensor.transform.localPosition = new Vector3(-10, 0, 0);
		RightSensor.transform.localPosition = new Vector3(10, 0, 0);
		UpSensor.transform.localPosition = new Vector3(0, 10, 0);
		DownSensor.transform.localPosition = new Vector3(0, -10, 0);
		ForwardSensor.transform.localPosition = new Vector3(0, 0, 10);
		BackSensor.transform.localPosition = new Vector3(0, 0, -10);

		LeftSensor.name = "LeftSensor";
		RightSensor.name = "RightSensor";
		UpSensor.name = "UpSensor";
		DownSensor.name = "DownSensor";
		ForwardSensor.name = "ForwardSensor";
		BackSensor.name = "BackSensor";

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
    private Vector3 StopPoint = Vector3.zero;

    void Update()
	{
		if (CurFleet.Count > 0)
		{
			float minSpeed = CurFleet.Select(shipGO => shipGO.GetComponent<MoveComponent>()).Min(moveComp => moveComp.NormalSoloMoveSpeed);

			MovementSpeed = minSpeed;

			if (CurFleet[0].GetComponent<MoveComponent>().CurFleet.ToString() != CurFleet.ToString())
			{
				CurFleet.RemoveAt(0);
			}

			if (_st.GuartTarget != null)
			{
				CurFleet.Clear();
			}
		}
		else
		{
			MovementSpeed = NormalSoloMoveSpeed;
		}

		if (InPatrol)
		{
			if (Vector3.Distance(gameObject.transform.position, PatrolWay[PatrolCurTarget]) > (_hm.ShipRadius + 2) + (ForwardSpeed * ForwardSpeed / 2) / MaxAcceleration)
			{
				Movement(PatrolWay[PatrolCurTarget]);
			}
			else
			{
				if (PatrolCurTarget < PatrolWay.Count-1)
				{
					PatrolCurTarget++;
				}
				else if (PatrolCurTarget >= PatrolWay.Count-1)
				{
					PatrolCurTarget = 0;
				}
			}
		}

	    if (IsStoping)
	    {
	        if (StopAmount < 1)
	        {
	            if (SpeedMoment == 0)
	            {
	                SpeedMoment = ForwardSpeed;
	            }
	            StopAmount += Time.deltaTime / SpeedMoment;
	        }
	        else
	        {
	            IsStoping = false;
	            StopAmount = 0;
	            SpeedMoment = 0;
	        }
        
	        if (StopPoint == Vector3.zero)
	        {
	            StopPoint = transform.position + (transform.forward * ForwardSpeed);
	        }

	        RotateShipAndMoveToTarget(transform.rotation.eulerAngles, StopPoint, StopAmount, true, true);

            if (transform.position == StopPoint)
            {
	            IsStoping = false;
                StopAmount = 0;
                SpeedMoment = 0;
            }
        }
	    else
	    {
            StopPoint = Vector3.zero;
	        StopAmount = 0;
	        SpeedMoment = 0;
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
				ClampInputs();
				CalculateRollAndPitchAngles();
			    if (ForwardSpeed > 0.5f)
			    {
			        CalculateInput(TargetVector);
                    ShipRotation(RotationSpeed, RotationAcceleration);
			        ForwardMovement(MaxAcceleration);
                }
			    else
			    {
			        Vector3 lookVector = TargetVector - transform.position;
			        if (Vector3.Angle(transform.forward, lookVector) > 20)
			        {
			            CalculateInput(TargetVector, true);
                        ShipRotation(RotationSpeed/5, RotationAcceleration/5, true);
                    }
			        else
			        {
			            ForwardMovement(MaxAcceleration);
                    }
			    }

			    StabAmount = 0;
            }
			else
			{
			    if (StabAmount < 1)
			    {
			        StabAmount += Time.deltaTime / RotationAcceleration;
			    }
			    else
			    {
			        StabAmount = 1;
			    }
                StabilizeShip(StabAmount);
			}
		}
		else
		{
			if (!ForwardBlocked && Vector3.Distance(RightSensor.transform.position, TargetVector) < Vector3.Distance(LeftSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance(LeftSensor.transform.position, TargetVector) < Vector3.Distance(RightSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance(UpSensor.transform.position, TargetVector) < Vector3.Distance(DownSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance(DownSensor.transform.position, TargetVector) < Vector3.Distance(UpSensor.transform.position, TargetVector) + (0.1f * RotationSpeed))
			{
				WarpMovement();
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

	public void AddPatrolPoint(Vector3 Point)
	{
		PatrolWay.Add(Point);
	}

	public void Stop()
	{
		_st.instruction = Stats.enInstruction.idle;
		_st.targetVector = gameObject.transform.position + transform.forward*(ForwardSpeed+_hm.ShipRadius);
	    IsStoping = true;
		Move = false;
	}

	void ForwardMovement(float MaxAccelerationIE)
	{
		float LMoveSpeed;
		if (_hm.curImpulseSystemHealth <= _hm.maxImpulseSystemHealth / 3)
		{
			LMoveSpeed = MovementSpeed / 2;
		}
		else
		{
			LMoveSpeed = MovementSpeed;
		}

		if (_rb.velocity.magnitude > MovementSpeed)
		{
			_rb.velocity = _rb.velocity.normalized * LMoveSpeed;
		}

		if (Vector3.Distance(gameObject.transform.position, CurTargetVector) > _hm.ShipRadius + (ForwardSpeed * ForwardSpeed / 2) / MaxAccelerationIE)
		{
			if (Vector3.Dot(_rb.velocity, transform.forward) < LMoveSpeed)
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
					amount += Time.deltaTime / LMoveSpeed;
				}
				else
				{
					amount = 1;
				}
				if (startvec == Vector3.zero)
				{
					startvec = transform.position;
				}
				gameObject.transform.position = Vector3.Lerp(startvec, TargetVector, amount);
			}
		}
	}

	void WarpMovement()
	{
		float LWarpSpeed;

		if (_hm.curWarpEngingSystemHealth <= _hm.maxWarpEngingSystemHealth / 2)
		{
			LWarpSpeed = WarpMovementSpeed / 2;
		}
		else
		{
			LWarpSpeed = WarpMovementSpeed;
		}
		if (Vector3.Dot(_rb.velocity, transform.forward) < LWarpSpeed)
		{
			_rb.velocity += gameObject.transform.forward * LWarpSpeed;
		}
		_rb.angularVelocity = Vector3.zero;

		Vector3 relativePos = TargetVector - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		transform.rotation = rotation;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color32(0, 255, 0, 100);
		Gizmos.DrawSphere(TargetVector, 5);
	}

    void ShipRotation(float Power, float RAcceleration, bool zerospeed = false)
    {
        var Factor = Vector3.Dot(transform.forward, _rb.velocity.normalized);
        if (!zerospeed)
        {
            Factor *= Factor;
            var newVelocity = Vector3.Lerp(_rb.velocity,
                transform.forward * Vector3.Dot(_rb.velocity, transform.forward),
                Factor * Vector3.Dot(_rb.velocity, transform.forward) * Time.deltaTime);
            _rb.velocity = newVelocity;
        }

        _rb.rotation = Quaternion.Slerp(_rb.rotation, Quaternion.LookRotation(_rb.velocity, transform.up),
            0.02f * Time.deltaTime);

        var torque = Vector3.zero;

        m_BankedTurnAmount = Mathf.Sin(RollAngle);
        _rb.angularDrag = Mathf.Abs(_rb.angularVelocity.magnitude - ((pitchInput + rollInput) / 2));

        torque += pitchInput * transform.right;
        torque += yawInput * 0.01f * transform.up;
        torque += -rollInput * transform.forward;
        torque += m_BankedTurnAmount * 0.5f * transform.up;
        if (!zerospeed)
        {
            _rb.AddTorque(torque * ForwardSpeed * Factor * _rb.mass);
        }
        else
        {
            _rb.AddTorque(torque * MovementSpeed * _rb.mass);
        }
    }

    private void ClampInputs()
	{
		rollInput = Mathf.Clamp(rollInput, -1, 1);
		pitchInput = Mathf.Clamp(pitchInput, -1, 1);
		yawInput = Mathf.Clamp(yawInput, -1, 1);
	}

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

	void CalculateInput(Vector3 Target, bool zerospeed = false)
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

	    float currentSpeedEffect;

        if (!zerospeed)
	    {
	        currentSpeedEffect = 1 + ForwardSpeed * 0.01f;
	    }
	    else
	    {
	        currentSpeedEffect = MovementSpeed * 0.01f;
        }

	    rollInput *= currentSpeedEffect;
		pitchInput *= currentSpeedEffect;
		yawInput *= currentSpeedEffect;
	}

    void ApplySensor(GameObject Start, Vector3 Vector, string Direction, bool ManeuversActive = false)
	{
		RaycastHit _rh;

		if (Physics.Raycast(Start.transform.position + RayCastBugDestroyer, Vector, out _rh, (_hm.ShipRadius + (MovementSpeed * 2))))
		{
			if (_rh.transform.gameObject != gameObject)
			{
				if (_rh.transform.gameObject.GetComponent<HealthModule>())
				{
					switch (Direction)
					{
						case "Forward":
							ForwardBlocked = true;
							break;
						case "Left":
							LeftBlocked = true;
							break;
						case "Right":
							RightBlocked = true;
							break;
						case "FLeft":
							LeftBlocked = true;
							break;
						case "FRight":
							RightBlocked = true;
							break;
						case "Up":
							UpBlocked = true;
							break;
						case "Down":
							DownBlocked = true;
							break;
					}
					if (ManeuversActive)
					{
						Maneuvers = true;
						ManeuversTimer = 0.2f;
					}
					CollisionObject = _rh.transform.name;
					Debug.DrawRay(Start.transform.position + RayCastBugDestroyer, Vector * (_hm.ShipRadius + (MovementSpeed * 2)), Color.red);
				}
				else
				{
					Debug.DrawRay(Start.transform.position + RayCastBugDestroyer, Vector * (_hm.ShipRadius + (MovementSpeed * 2)), Color.yellow);
				}
			}
			else
			{
				Debug.DrawRay(Start.transform.position + RayCastBugDestroyer, Vector * (_hm.ShipRadius + (MovementSpeed * 2)), Color.yellow);
			}
		}
		else
		{
			switch (Direction)
			{
				case "Forward":
					ForwardBlocked = false;
					break;
				case "Left":
					LeftBlocked = false;
					break;
				case "Right":
					RightBlocked = false;
					break;
				case "FLeft":
					LeftBlocked = false;
					break;
				case "FRight":
					RightBlocked = false;
					break;
				case "Up":
					UpBlocked = false;
					break;
				case "Down":
					DownBlocked = false;
					break;
			}
			Debug.DrawRay(Start.transform.position + RayCastBugDestroyer, Vector * (_hm.ShipRadius + (MovementSpeed * 2)), Color.green);
		}
	}

	void FedSensorSystem()
	{
		ApplySensor(ForwardRayStart, gameObject.transform.TransformDirection(Vector3.forward), "Forward", true);
        ApplySensor(LeftRayStart, gameObject.transform.TransformDirection(Vector3.left), "Left");
        ApplySensor(RightRayStart, gameObject.transform.TransformDirection(Vector3.right), "Right");
        ApplySensor(UpRayStart, gameObject.transform.TransformDirection(Vector3.up), "Up", true);
		ApplySensor(DownRayStart, gameObject.transform.TransformDirection(Vector3.down), "Down", true);
		ApplySensor(LeftRayStart, Quaternion.AngleAxis(-25, transform.up) * gameObject.transform.TransformDirection(Vector3.forward), "FLeft", true);
		ApplySensor(RightRayStart, Quaternion.AngleAxis(25, transform.up) * gameObject.transform.TransformDirection(Vector3.forward), "FRight", true);
       
	}

	public void Movement(Vector3 MovementPosition)
	{
		_st.targetVector = MovementPosition;
	}

	void StabilizeShip(float Amount)
	{
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

	public void RotateShipAndMoveToTarget(Vector3 Target, Vector3 MoveTarget, float Amount, bool move = false, bool rotate = false)
	{
		_rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, Vector3.zero, Amount);
		_rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, Amount);
		_rb.angularDrag = 0;

		if (rotate)
		{
			Vector3 StartRot = Vector3.zero;

			if (StartRot == Vector3.zero)
			{
				StartRot = gameObject.transform.localRotation.eulerAngles;
			}

			gameObject.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(StartRot), Quaternion.Euler(Target), Amount);
		}

		if (move)
		{
			Vector3 StartPos = Vector3.zero;

			if (StartPos == Vector3.zero)
			{
				StartPos = gameObject.transform.position;
			}

			gameObject.transform.position = Vector3.Slerp(StartPos, MoveTarget, Amount);
		}
	}


	public void SetCurFleet(List<GameObject> NotStateShips)
	{
		CurFleet = NotStateShips.ToList();
	}
}
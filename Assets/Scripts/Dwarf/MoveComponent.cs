using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveComponent : MonoBehaviour {
	public bool CUBETYPE;
	public bool CubeDetectSystem;

	public Vector3 TargetVector;

	public float RotationSpeed;
	public float MovementSpeed;
	public float SpeedUpSpeed;

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
	public float angle;
	[HideInInspector]
	public float angleY;
	[HideInInspector]
	public float angleZ;

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

	public GameObject ForwardRayStart;

	public GameObject LeftRayStart;
	public GameObject RightRayStart;

	public GameObject UpRayStart;
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

	// Use this for initialization
	void Awake () {
		_st = gameObject.GetComponent<Stats> ();
		_as = gameObject.GetComponent<ActiveState> ();
		_rb = gameObject.GetComponent<Rigidbody> ();
		_hm = gameObject.GetComponent<HealthModule> ();
		TargetVector = gameObject.transform.position;
		OldTargetVector = gameObject.transform.position;
		Move = false;

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

		LeftSensor.transform.localPosition = new Vector3(-1,0,0);
		RightSensor.transform.localPosition = new Vector3(1,0,0);
		UpSensor.transform.localPosition = new Vector3(0,1,0);
		DownSensor.transform.localPosition = new Vector3(0,-1,0);
		ForwardSensor.transform.localPosition = new Vector3(0,0,1);
		BackSensor.transform.localPosition = new Vector3(0,0,-1);

		LeftSensor.name = "LeftSensor";
		RightSensor.name = "RightSensor";
		UpSensor.name = "UpSensor";
		DownSensor.name = "DownSensor";
		ForwardSensor.name = "ForwardSensor";
		BackSensor.name = "BackSensor";

		Timer = 3;

		_rb.SetMaxAngularVelocity (10);

		if (ForwardRayStart == null) {
			ForwardRayStart = gameObject;
		}
		if (LeftRayStart == null) {
			LeftRayStart = gameObject;
		}
		if (RightRayStart == null) {
			RightRayStart = gameObject;
		}
		if (UpRayStart == null) {
			UpRayStart = gameObject;
		}
		if (DownRayStart == null) {
			DownRayStart = gameObject;
		}
	}
	
	// Update is called once per frame
	void LateUpdate(){
		
	}
	void Update () {
		if (!CUBETYPE) {
			if (!gameObject.GetComponent<Maneuvers> ()) {
				CurRotSpeed = RotationSpeed;
			} else {
				if (_st.targetTransform != null) {
					if (Vector3.Distance (gameObject.transform.position, _st.targetTransform.position) > _as.radiuse - 3) {
						CurRotSpeed = RotationSpeed;
					} else {
						CurRotSpeed = gameObject.GetComponent<Maneuvers> ().ManeuversSpeed;
					}
				} else {
					CurRotSpeed = RotationSpeed;
				}
			}

			localangularvelocity = transform.InverseTransformDirection (_rb.angularVelocity).normalized * _rb.angularVelocity.magnitude;
			RaycastHit _rh;
			RaycastHit _rh2;
			RaycastHit _rh3;

			RaycastHit _rh4;
			RaycastHit _rh5;

			RaycastHit _rh6;
			RaycastHit _rh7;

			Vector3 Forward = gameObject.transform.TransformDirection (Vector3.forward);
			Vector3 Left = gameObject.transform.TransformDirection (Vector3.left);
			Vector3 Right = gameObject.transform.TransformDirection (Vector3.right);

			Vector3 FLeft = Quaternion.AngleAxis (-25, transform.up) * gameObject.transform.TransformDirection (Vector3.forward);
			Vector3 FRight = Quaternion.AngleAxis (25, transform.up) * gameObject.transform.TransformDirection (Vector3.forward);

			Vector3 Up = gameObject.transform.TransformDirection (Vector3.up);
			Vector3 Down = gameObject.transform.TransformDirection (Vector3.down);

			if (!_st.IsFix) {
				if (gameObject.GetComponent<ActiveState> ().Build) {
					ShipIsBuilt = false;
				}
			}
			if (!ShipIsBuilt && !SensorBlocking) {
				if (Physics.Raycast (ForwardRayStart.transform.position + RayCastBugDestroyer, Forward, out _rh, (_hm.ShipRadius + (MovementSpeed * 2)))) {
					if (_rh.transform.gameObject != gameObject) {
						if (_rh.transform.gameObject.GetComponent<HealthModule> ()) {
							ForwardBlocked = true;
							Maneuvers = true;
							ManeuversTimer = 0.2f;
							CollisionObject = _rh.transform.name;
							Debug.DrawRay (ForwardRayStart.transform.position + RayCastBugDestroyer, Forward * (_hm.ShipRadius + (MovementSpeed * 2)), Color.red);
						} else {
							Debug.DrawRay (ForwardRayStart.transform.position + RayCastBugDestroyer, Forward * (_hm.ShipRadius + (MovementSpeed * 2)), Color.yellow);
						}
					} else {
						Debug.DrawRay (ForwardRayStart.transform.position + RayCastBugDestroyer, Forward * (_hm.ShipRadius + (MovementSpeed * 2)), Color.yellow);
					}
				} else {
					Debug.DrawRay (ForwardRayStart.transform.position + RayCastBugDestroyer, Forward * (_hm.ShipRadius + (MovementSpeed * 2)), Color.green);
				}
				if (Physics.Raycast (LeftRayStart.transform.position + RayCastBugDestroyer, Left, out _rh2, (_hm.ShipRadius + 3))) {
					if (_rh2.transform.gameObject != gameObject) {
						if (_rh2.transform.gameObject.GetComponent<HealthModule> ()) {
							LeftBlocked = true;
							//Maneuvers = true;
							//ManeuversTimer = 0.2f;
							CollisionObject = _rh2.transform.name;
							Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, Left * (_hm.ShipRadius + 3), Color.red);
						} else {
							Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, Left * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, Left * (_hm.ShipRadius + 3), Color.yellow);
					}
				} else {
					Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, Left * (_hm.ShipRadius + 3), Color.green);
				}
	
				if (Physics.Raycast (RightRayStart.transform.position + RayCastBugDestroyer, Right, out _rh3, (_hm.ShipRadius + 3))) {
					if (_rh3.transform.gameObject != gameObject) {
						if (_rh3.transform.gameObject.GetComponent<HealthModule> ()) {
							RightBlocked = true;
							//Maneuvers = true;
							//ManeuversTimer = 0.2f;
							CollisionObject = _rh3.transform.name;
							Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, Right * (_hm.ShipRadius + 3), Color.red);
						} else {
							Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, Right * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, Right * (_hm.ShipRadius + 3), Color.yellow);
					}
				} else {
					Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, Right * (_hm.ShipRadius + 3), Color.green);
				}

				if (Physics.Raycast (LeftRayStart.transform.position + RayCastBugDestroyer, FLeft, out _rh4, (_hm.ShipRadius + 3))) {
					if (_rh4.transform.gameObject != gameObject) {
						if (_rh4.transform.gameObject.GetComponent<HealthModule> ()) {
							LeftBlocked = true;
							Maneuvers = true;
							ManeuversTimer = 0.2f;
							CollisionObject = _rh4.transform.name;
							Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, FLeft * (_hm.ShipRadius + 3), Color.red);
						} else {
							Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, FLeft * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, FLeft * (_hm.ShipRadius + 3), Color.yellow);
					}
				} else {
					Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, FLeft * (_hm.ShipRadius + 3), Color.green);
				}
				if (Physics.Raycast (RightRayStart.transform.position + RayCastBugDestroyer, FRight, out _rh5, (_hm.ShipRadius + 3))) {
					if (_rh5.transform.gameObject != gameObject) {
						if (_rh5.transform.gameObject.GetComponent<HealthModule> ()) {
							RightBlocked = true;
							Maneuvers = true;
							ManeuversTimer = 0.2f;
							CollisionObject = _rh5.transform.name;
							Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, FRight * (_hm.ShipRadius + 3), Color.red);
						} else {
							Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, FRight * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, FRight * (_hm.ShipRadius + 3), Color.yellow);
					}
				} else {
					Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, FRight * (_hm.ShipRadius + 3), Color.green);
				}

				if (Physics.Raycast (UpRayStart.transform.position + RayCastBugDestroyer, Up, out _rh6, (_hm.ShipRadius + 3))) {
					if (_rh6.transform.gameObject != gameObject) {
						if (_rh6.transform.gameObject.GetComponent<HealthModule> ()) {
							UpBlocked = true;
							Maneuvers = true;
							ManeuversTimer = 0.2f;
							CollisionObject = _rh6.transform.name;
							Debug.DrawRay (UpRayStart.transform.position + RayCastBugDestroyer, Up * (_hm.ShipRadius + 3), Color.red);
						} else {
							Debug.DrawRay (UpRayStart.transform.position + RayCastBugDestroyer, Up * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (UpRayStart.transform.position + RayCastBugDestroyer, Up * (_hm.ShipRadius + 3), Color.yellow);
					}
				} else {
					Debug.DrawRay (UpRayStart.transform.position + RayCastBugDestroyer, Up * (_hm.ShipRadius + 3), Color.green);
				}
				if (Physics.Raycast (DownRayStart.transform.position + RayCastBugDestroyer, Down, out _rh7, (_hm.ShipRadius + 3))) {
					if (_rh7.transform.gameObject != gameObject) {
						if (_rh7.transform.gameObject.GetComponent<HealthModule> ()) {
							DownBlocked = true;
							Maneuvers = true;
							ManeuversTimer = 0.2f;
							CollisionObject = _rh7.transform.name;
							Debug.DrawRay (DownRayStart.transform.position + RayCastBugDestroyer, Down * (_hm.ShipRadius + 3), Color.red);
						} else {
							Debug.DrawRay (DownRayStart.transform.position + RayCastBugDestroyer, Down * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (DownRayStart.transform.position + RayCastBugDestroyer, Down * (_hm.ShipRadius + 3), Color.yellow);
					}
				} else {
					Debug.DrawRay (DownRayStart.transform.position + RayCastBugDestroyer, Down * (_hm.ShipRadius + 3), Color.green);
				}
			}

			TargetVector = _st.targetVector;
			if (!Lock) {
				angleZ = -1 * gameObject.transform.rotation.z;
				Vector3 targetDir = TargetVector - transform.position;
				//if (TargetVector != _st.targetVector) {
				//	TargetVector = _st.targetVector;
				//	if (OldTargetVector == TargetVector) {
				//		Move = true;
				//		Maneuvers = false;
				//	}
				//}
				if (!Maneuvers) {
					if (Vector3.Distance (ForwardSensor.transform.position, TargetVector) < Vector3.Distance (BackSensor.transform.position, TargetVector)) {
						if (Vector3.Distance (LeftSensor.transform.position, TargetVector) > Vector3.Distance (RightSensor.transform.position, TargetVector) + (0.1f * RotationSpeed)) {
							angle = 0.1f * _rb.mass;
						}
						if (Vector3.Distance (RightSensor.transform.position, TargetVector) > Vector3.Distance (LeftSensor.transform.position, TargetVector) + (0.1f * RotationSpeed)) {
							angle = -0.1f * _rb.mass;
						}
						if (Vector3.Distance (RightSensor.transform.position, TargetVector) < Vector3.Distance (LeftSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance (LeftSensor.transform.position, TargetVector) < Vector3.Distance (RightSensor.transform.position, TargetVector) + (0.1f * RotationSpeed)) {
							if (RightStab) {
								_rb.angularVelocity = new Vector3 (0, 0, 0);
								//angle = localangularvelocity.y * _rb.mass;
								if (_rb.angularVelocity == new Vector3 (0, 0, 0)) {
									RightStab = false;
								}
							}
							if (LeftStab) {
								_rb.angularVelocity = new Vector3 (0, 0, 0);
								//angle = -localangularvelocity.y * _rb.mass;
								if (_rb.angularVelocity == new Vector3 (0, 0, 0)) {
									LeftStab = false;
								}
							}
							if (!LeftStab && !RightStab) {
								angle = 0;
							}
							//RotationStop ();
						}
					} else {
						if (Vector3.Distance (LeftSensor.transform.position, TargetVector) > Vector3.Distance (RightSensor.transform.position, TargetVector)) {
							angle = 0.1f * _rb.mass;
						}
						if (Vector3.Distance (RightSensor.transform.position, TargetVector) > Vector3.Distance (LeftSensor.transform.position, TargetVector)) {
							angle = -0.1f * _rb.mass;
						}
					}
					if (gameObject.transform.position.y > -2.942455 + 1 || gameObject.transform.position.y < -2.942455 - 1) {
						if (Vector3.Distance (UpSensor.transform.position, TargetVector) < Vector3.Distance (DownSensor.transform.position, TargetVector) + (0.1f * RotationSpeed)) {
							angleY = -0.1f * _rb.mass;
						}
						if (Vector3.Distance (UpSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) > Vector3.Distance (DownSensor.transform.position, TargetVector)) {
							angleY = 0.1f * _rb.mass;
						}
						if (Vector3.Distance (UpSensor.transform.position, TargetVector) < Vector3.Distance (DownSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance (DownSensor.transform.position, TargetVector) < Vector3.Distance (UpSensor.transform.position, TargetVector) + (0.1f * RotationSpeed)) {
							if (UpStab) {
								_rb.angularVelocity = new Vector3 (0, 0, 0);
								//angleY = -0.1f*_rb.mass;
								if (_rb.angularVelocity == new Vector3 (0, 0, 0)) {
									UpStab = false;
								}
							}
							if (DownStab) {
								_rb.angularVelocity = new Vector3 (0, 0, 0);
								//angleY = 0.1f*_rb.mass;
								if (_rb.angularVelocity == new Vector3 (0, 0, 0)) {
									DownStab = false;
								}
							}
							if (!UpStab && !DownStab) {
								angleY = 0;
							}
						}
					}
					ForwardBlocked = false;
					LeftBlocked = false;
					RightBlocked = false;
					UpBlocked = false;
					DownBlocked = false;
				} else {
					if (!RightBlocked) {
						angle = 0.1f * _rb.mass / 2;
					} else {
						if (!LeftBlocked) {
							angle = -0.1f * _rb.mass / 2;
						}
					}
					if (!UpBlocked) {
						angleY = 0.1f * _rb.mass / 2;
					} else {
						if (!DownBlocked) {
							angleY = -0.1f * _rb.mass / 2;
						}
					}
					if (ManeuversTimer > 0) {
						ManeuversTimer -= Time.deltaTime;
					} else {
						Maneuvers = false;
					}
				}
			}
			//if (gameObject.transform.rotation.x < 30 && gameObject.transform.rotation.x > -30) {
			RotationVelocity = new Vector3 (angleY, angle, 0);
			if (!Warp) {
				if (Move) {
					if (!LeftStab && !RightStab && !UpStab && !DownStab) {
						_rb.maxAngularVelocity = CurRotSpeed;
					}
					if (Vector3.Distance (gameObject.transform.position, TargetVector) > 1) {
						if (Vector3.Dot (_rb.velocity, transform.forward) < MovementSpeed) {
							_rb.velocity += gameObject.transform.forward * SpeedUpSpeed;
						}
						if (angle != 0 || angleY != 0) {
							_rb.drag = Vector3.Dot (_rb.velocity, transform.forward);
						} else {
							_rb.drag = 0.5f;
						}
						if (angle > 0) {
							if (angle != 0) {
								if (_rb.angularVelocity.y < CurRotSpeed) {
									_rb.AddRelativeTorque (new Vector3 (0, RotationVelocity.y, 0) * _rb.mass);
									//_rb.angularVelocity += new Vector3 (0, RotationVelocity.y, 0);
								}
							}
						} else {
							if (angle != 0) {
								if (_rb.angularVelocity.y > -CurRotSpeed) {
									_rb.AddRelativeTorque (new Vector3 (0, RotationVelocity.y, 0) * _rb.mass);
									//	_rb.angularVelocity += new Vector3 (0, RotationVelocity.y, 0);
								}
							}
						}
						_rb.AddRelativeTorque (new Vector3 (RotationVelocity.x, 0, 0) * _rb.mass);
					} else {
						if (Vector3.Dot (_rb.velocity, transform.forward) > 0) {
							_rb.velocity -= gameObject.transform.forward * SpeedUpSpeed;
						}
						if (Vector3.Dot (_rb.velocity, transform.forward) < 0) {
							_rb.velocity = new Vector3 (0, 0, 0);
						}
						Move = false;
					}
					if (angleY == 0) {
						//	_rb.angularVelocity = new Vector3 (0, _rb.angularVelocity.y, _rb.angularVelocity.z);
					}
					if (angle == 0) {
						//	_rb.angularVelocity = new Vector3 (_rb.angularVelocity.x, 0, _rb.angularVelocity.z);
					}
					_rb.AddRelativeTorque (new Vector3 (0, 0, RotationVelocity.z) * _rb.mass);
				} else {
					//_rb.maxAngularVelocity = 360;
					FullStop ();
					RotationStop ();
					gameObject.transform.localRotation = Quaternion.Slerp (Quaternion.Euler (gameObject.transform.localRotation.eulerAngles), Quaternion.Euler (new Vector3 (0, gameObject.transform.localRotation.eulerAngles.y, gameObject.transform.localRotation.eulerAngles.z)), 0.1f);	
				}
				//	gameObject.transform.localRotation = Quaternion.Slerp (Quaternion.Euler (gameObject.transform.localRotation.eulerAngles), Quaternion.Euler (new Vector3 (gameObject.transform.localRotation.eulerAngles.x, gameObject.transform.localRotation.eulerAngles.y, 0)), 1);	
			}
			if (Timer > 0) {
				Timer -= Time.deltaTime;
			} else {
				//	Warp = false;
				Timer = 3;
			}
			if (Warp) {
				if (Move) {
					if (!ForwardBlocked && Vector3.Distance (RightSensor.transform.position, TargetVector) < Vector3.Distance (LeftSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance (LeftSensor.transform.position, TargetVector) < Vector3.Distance (RightSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance (UpSensor.transform.position, TargetVector) < Vector3.Distance (DownSensor.transform.position, TargetVector) + (0.1f * RotationSpeed) && Vector3.Distance (DownSensor.transform.position, TargetVector) < Vector3.Distance (UpSensor.transform.position, TargetVector) + (0.1f * RotationSpeed)) {
						if (Vector3.Dot (_rb.velocity, transform.forward) < WarpMovementSpeed * MovementSpeed) {
							_rb.velocity += gameObject.transform.forward * (SpeedUpSpeed * WarpMovementSpeed);
						}
					} else {
						if (!LeftStab && !RightStab && !UpStab && !DownStab) {
							_rb.maxAngularVelocity = CurRotSpeed;
						}
						if (Vector3.Distance (gameObject.transform.position, TargetVector) > 1) {
							if (Vector3.Dot (_rb.velocity, transform.forward) < MovementSpeed) {
								_rb.velocity += gameObject.transform.forward * SpeedUpSpeed;
							}
							if (angle != 0 || angleY != 0) {
								_rb.drag = Vector3.Dot (_rb.velocity, transform.forward);
							} else {
								_rb.drag = 0.5f;
							}
							if (angle > 0) {
								if (angle != 0) {
									if (_rb.angularVelocity.y < CurRotSpeed) {
										_rb.AddRelativeTorque (new Vector3 (0, RotationVelocity.y, 0) * _rb.mass);
										//_rb.angularVelocity += new Vector3 (0, RotationVelocity.y, 0);
									}
								}
							} else {
								if (angle != 0) {
									if (_rb.angularVelocity.y > -CurRotSpeed) {
										_rb.AddRelativeTorque (new Vector3 (0, RotationVelocity.y, 0) * _rb.mass);
									}
								}
							}
							_rb.AddRelativeTorque (new Vector3 (RotationVelocity.x, 0, 0) * _rb.mass);
						} else {
							if (Vector3.Dot (_rb.velocity, transform.forward) > 0) {
								_rb.velocity -= gameObject.transform.forward * SpeedUpSpeed;
							}
							Move = false;
						}
						_rb.AddRelativeTorque (new Vector3 (0, 0, RotationVelocity.z) * _rb.mass);
					}
				}
			}
			if (active) {
				_rb.AddRelativeTorque (impuls * _rb.mass);
				active = false;
			}
			if (localangularvelocity.y > 0) {
				LeftStab = true;
			}
			if (localangularvelocity.y < 0) {
				RightStab = true;
			}
			if (localangularvelocity.x > 0) {
				DownStab = true;
			}
			if (localangularvelocity.x < 0) {
				UpStab = true;
			}
			if (ZStabTimer > 0) {
				ZStabTimer -= Time.deltaTime;
			} else {
				ZStabActive = !ZStabActive;
				if (ZStabActive) {
					ZStabTimer = 6;
				} else {
					ZStabTimer = 3;
				}
			}
			if (ZStabActive) {
				gameObject.transform.localRotation = Quaternion.Slerp (Quaternion.Euler (gameObject.transform.localRotation.eulerAngles), Quaternion.Euler (new Vector3 (gameObject.transform.localRotation.eulerAngles.x, gameObject.transform.localRotation.eulerAngles.y, 0)), CurRotSpeed / 100);
			}
			if (!Move && !Warp) {
				Movement (new Vector3 (gameObject.transform.position.x, -2.942455f, gameObject.transform.position.z));
			}
		} else {
			CurRotSpeed = RotationSpeed;
			if (!_st.IsFix) {
				if (gameObject.GetComponent<ActiveState> ().Build) {
					ShipIsBuilt = false;
				}
			}
			if (!CubeDetectSystem) {
				Vector3 localangularvelocity = transform.InverseTransformDirection (_rb.angularVelocity).normalized * _rb.angularVelocity.magnitude;
				RaycastHit _rh;
				RaycastHit _rh2;
				RaycastHit _rh3;

				RaycastHit _rh4;
				RaycastHit _rh5;

				RaycastHit _rh6;
				RaycastHit _rh7;

				Vector3 Forward = gameObject.transform.TransformDirection (Vector3.forward);
				Vector3 Left = gameObject.transform.TransformDirection (Vector3.left);
				Vector3 Right = gameObject.transform.TransformDirection (Vector3.right);

				Vector3 FLeft = Quaternion.AngleAxis (-25, transform.up) * gameObject.transform.TransformDirection (Vector3.forward);
				Vector3 FRight = Quaternion.AngleAxis (25, transform.up) * gameObject.transform.TransformDirection (Vector3.forward);

				Vector3 Up = gameObject.transform.TransformDirection (Vector3.up);
				Vector3 Down = gameObject.transform.TransformDirection (Vector3.down);
				if (!ShipIsBuilt && !SensorBlocking) {
					if (Physics.Raycast (ForwardRayStart.transform.position + RayCastBugDestroyer, Forward, out _rh, (_hm.ShipRadius + (MovementSpeed * 2)))) {
						if (_rh.transform.gameObject != gameObject) {
							if (_rh.transform.gameObject.GetComponent<HealthModule> ()) {
								ForwardBlocked = true;
								Maneuvers = true;
								ManeuversTimer = 0.2f;
								CollisionObject = _rh.transform.name;
								Debug.DrawRay (ForwardRayStart.transform.position + RayCastBugDestroyer, Forward * (_hm.ShipRadius + (MovementSpeed * 2)), Color.red);
							} else {
								Debug.DrawRay (ForwardRayStart.transform.position + RayCastBugDestroyer, Forward * (_hm.ShipRadius + (MovementSpeed * 2)), Color.yellow);
							}
						} else {
							Debug.DrawRay (ForwardRayStart.transform.position + RayCastBugDestroyer, Forward * (_hm.ShipRadius + (MovementSpeed * 2)), Color.yellow);
						}
					} else {
						Debug.DrawRay (ForwardRayStart.transform.position + RayCastBugDestroyer, Forward * (_hm.ShipRadius + (MovementSpeed * 2)), Color.green);
					}
					if (Physics.Raycast (LeftRayStart.transform.position + RayCastBugDestroyer, Left, out _rh2, (_hm.ShipRadius + 3))) {
						if (_rh2.transform.gameObject != gameObject) {
							if (_rh2.transform.gameObject.GetComponent<HealthModule> ()) {
								LeftBlocked = true;
								//Maneuvers = true;
								//ManeuversTimer = 0.2f;
								CollisionObject = _rh2.transform.name;
								Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, Left * (_hm.ShipRadius + 3), Color.red);
							} else {
								Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, Left * (_hm.ShipRadius + 3), Color.yellow);
							}
						} else {
							Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, Left * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, Left * (_hm.ShipRadius + 3), Color.green);
					}

					if (Physics.Raycast (RightRayStart.transform.position + RayCastBugDestroyer, Right, out _rh3, (_hm.ShipRadius + 3))) {
						if (_rh3.transform.gameObject != gameObject) {
							if (_rh3.transform.gameObject.GetComponent<HealthModule> ()) {
								RightBlocked = true;
								//Maneuvers = true;
								//ManeuversTimer = 0.2f;
								CollisionObject = _rh3.transform.name;
								Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, Right * (_hm.ShipRadius + 3), Color.red);
							} else {
								Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, Right * (_hm.ShipRadius + 3), Color.yellow);
							}
						} else {
							Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, Right * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, Right * (_hm.ShipRadius + 3), Color.green);
					}

					if (Physics.Raycast (LeftRayStart.transform.position + RayCastBugDestroyer, FLeft, out _rh4, (_hm.ShipRadius + 3))) {
						if (_rh4.transform.gameObject != gameObject) {
							if (_rh4.transform.gameObject.GetComponent<HealthModule> ()) {
								LeftBlocked = true;
								Maneuvers = true;
								ManeuversTimer = 0.2f;
								CollisionObject = _rh4.transform.name;
								Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, FLeft * (_hm.ShipRadius + 3), Color.red);
							} else {
								Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, FLeft * (_hm.ShipRadius + 3), Color.yellow);
							}
						} else {
							Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, FLeft * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (LeftRayStart.transform.position + RayCastBugDestroyer, FLeft * (_hm.ShipRadius + 3), Color.green);
					}
					if (Physics.Raycast (RightRayStart.transform.position + RayCastBugDestroyer, FRight, out _rh5, (_hm.ShipRadius + 3))) {
						if (_rh5.transform.gameObject != gameObject) {
							if (_rh5.transform.gameObject.GetComponent<HealthModule> ()) {
								RightBlocked = true;
								Maneuvers = true;
								ManeuversTimer = 0.2f;
								CollisionObject = _rh5.transform.name;
								Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, FRight * (_hm.ShipRadius + 3), Color.red);
							} else {
								Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, FRight * (_hm.ShipRadius + 3), Color.yellow);
							}
						} else {
							Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, FRight * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (RightRayStart.transform.position + RayCastBugDestroyer, FRight * (_hm.ShipRadius + 3), Color.green);
					}

					if (Physics.Raycast (UpRayStart.transform.position + RayCastBugDestroyer, Up, out _rh6, (_hm.ShipRadius + 3))) {
						if (_rh6.transform.gameObject != gameObject) {
							if (_rh6.transform.gameObject.GetComponent<HealthModule> ()) {
								UpBlocked = true;
								Maneuvers = true;
								ManeuversTimer = 0.2f;
								CollisionObject = _rh6.transform.name;
								Debug.DrawRay (UpRayStart.transform.position + RayCastBugDestroyer, Up * (_hm.ShipRadius + 3), Color.red);
							} else {
								Debug.DrawRay (UpRayStart.transform.position + RayCastBugDestroyer, Up * (_hm.ShipRadius + 3), Color.yellow);
							}
						} else {
							Debug.DrawRay (UpRayStart.transform.position + RayCastBugDestroyer, Up * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (UpRayStart.transform.position + RayCastBugDestroyer, Up * (_hm.ShipRadius + 3), Color.green);
					}
					if (Physics.Raycast (DownRayStart.transform.position + RayCastBugDestroyer, Down, out _rh7, (_hm.ShipRadius + 3))) {
						if (_rh7.transform.gameObject != gameObject) {
							if (_rh7.transform.gameObject.GetComponent<HealthModule> ()) {
								DownBlocked = true;
								Maneuvers = true;
								ManeuversTimer = 0.2f;
								CollisionObject = _rh7.transform.name;
								Debug.DrawRay (DownRayStart.transform.position + RayCastBugDestroyer, Down * (_hm.ShipRadius + 3), Color.red);
							} else {
								Debug.DrawRay (DownRayStart.transform.position + RayCastBugDestroyer, Down * (_hm.ShipRadius + 3), Color.yellow);
							}
						} else {
							Debug.DrawRay (DownRayStart.transform.position + RayCastBugDestroyer, Down * (_hm.ShipRadius + 3), Color.yellow);
						}
					} else {
						Debug.DrawRay (DownRayStart.transform.position + RayCastBugDestroyer, Down * (_hm.ShipRadius + 3), Color.green);
					}
				}
			} else {
			
			}

			TargetVector = _st.targetVector;
			Vector3 targetDir = TargetVector - transform.position;
			if (!Lock) {
				angleZ = -1 * gameObject.transform.rotation.z;
				if (!Maneuvers) {
					if (Left) {
						angle = 0.1f * _rb.mass;
					} else {
						angle = -0.1f * _rb.mass;
					}
					if (ChangeTimer > 0) {
						ChangeTimer -= Time.deltaTime;
					} else {
						Left = !Left;
						ChangeTimer = Random.Range (1, 10);
					}
					ForwardBlocked = false;
					LeftBlocked = false;
					RightBlocked = false;
					UpBlocked = false;
					DownBlocked = false;
				} else {
					if (!RightBlocked) {
						angle = 0.1f * _rb.mass / 2;
					} else {
						if (!LeftBlocked) {
							angle = -0.1f * _rb.mass / 2;
						}
					}
					if (!UpBlocked) {
						angleY = 0.1f * _rb.mass / 2;
					} else {
						if (!DownBlocked) {
							angleY = -0.1f * _rb.mass / 2;
						}
					}
					if (ManeuversTimer > 0) {
						ManeuversTimer -= Time.deltaTime;
					} else {
						Maneuvers = false;
					}
				}
			}
			RotationVelocity = new Vector3 (0, angle, 0);
			if (!Warp) {
				if (Move) {
					if (!LeftStab && !RightStab && !UpStab && !DownStab) {
						_rb.maxAngularVelocity = CurRotSpeed;
					}
					if (Vector3.Distance (gameObject.transform.position, TargetVector) > 1) {
						_rb.velocity = targetDir.normalized * MovementSpeed;

						if (angle > 0) {
							if (angle != 0) {
								if (_rb.angularVelocity.y < CurRotSpeed) {
									_rb.AddRelativeTorque (new Vector3 (0, RotationVelocity.y, 0) * _rb.mass);
									//_rb.angularVelocity += new Vector3 (0, RotationVelocity.y, 0);
								}
							}
						} else {
							if (angle != 0) {
								if (_rb.angularVelocity.y > -CurRotSpeed) {
									_rb.AddRelativeTorque (new Vector3 (0, RotationVelocity.y, 0) * _rb.mass);
									//	_rb.angularVelocity += new Vector3 (0, RotationVelocity.y, 0);
								}
							}
						}
						//_rb.AddRelativeTorque (new Vector3 (RotationVelocity.x, 0, 0) * _rb.mass);
					} else {
						_rb.velocity = Vector3.Lerp(_rb.velocity, new Vector3 (0, 0, 0), 30);
						Move = false;
					}
					_rb.AddRelativeTorque (new Vector3 (0, 0, RotationVelocity.z) * _rb.mass);
				} else {
					//_rb.maxAngularVelocity = 360;
					FullStop ();
					RotationStop ();
					gameObject.transform.localRotation = Quaternion.Slerp (Quaternion.Euler (gameObject.transform.localRotation.eulerAngles), Quaternion.Euler (new Vector3 (0, gameObject.transform.localRotation.eulerAngles.y, gameObject.transform.localRotation.eulerAngles.z)), 0.1f);	
				}
				//	gameObject.transform.localRotation = Quaternion.Slerp (Quaternion.Euler (gameObject.transform.localRotation.eulerAngles), Quaternion.Euler (new Vector3 (gameObject.transform.localRotation.eulerAngles.x, gameObject.transform.localRotation.eulerAngles.y, 0)), 1);	
			} else {
				if (Move) {
					_rb.velocity = targetDir.normalized * WarpMovementSpeed;
				}
			}
			if (active) {
				_rb.AddRelativeTorque (impuls * _rb.mass);
				active = false;
			}
			if (localangularvelocity.y > 0) {
				LeftStab = true;
			}
			if (localangularvelocity.y < 0) {
				RightStab = true;
			}
			if (localangularvelocity.x > 0) {
				DownStab = true;
			}
			if (localangularvelocity.x < 0) {
				UpStab = true;
			}
			if (ZStabTimer > 0) {
				ZStabTimer -= Time.deltaTime;
			} else {
				ZStabActive = !ZStabActive;
				if (ZStabActive) {
					ZStabTimer = 6;
				} else {
					ZStabTimer = 3;
				}
			}
			if (ZStabActive) {
				gameObject.transform.localRotation = Quaternion.Slerp (Quaternion.Euler (gameObject.transform.localRotation.eulerAngles), Quaternion.Euler (new Vector3 (gameObject.transform.localRotation.eulerAngles.x, gameObject.transform.localRotation.eulerAngles.y, 0)), CurRotSpeed / 100);
			}
			if (!Move && !Warp) {
				Movement (new Vector3 (gameObject.transform.position.x, -2.942455f, gameObject.transform.position.z));
			}
		}
	}
	public void FullStop(){
		_rb.velocity = Vector3.Slerp(_rb.velocity, new Vector3 (0, 0, 0), 0.1f);
	}
	public void RotationStop(){
		_rb.angularVelocity = Vector3.Slerp(_rb.angularVelocity, new Vector3 (0, 0, 0), 0.1f*RotationSpeed);
	}
	public void Stop(){
		gameObject.GetComponent<Stats>().instruction = Stats.enInstruction.idle;
		gameObject.GetComponent<Stats> ().targetVector = gameObject.transform.position;
		FullStop();
		RotationStop();
		Move = false;
	}
	public void Movement(Vector3 MovementPosition){
		//gameObject.GetComponent<Stats>().instruction = Stats.enInstruction.move;
		gameObject.GetComponent<Stats> ().targetVector = MovementPosition;
		Move = true;
	}
}
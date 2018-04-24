using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour
{
	public Camera itsMinimapCamera;
	public GameObject itsMainCamera;

	public Vector3 LeftUpPoint;
	public Vector3 RightUpPoint;
	public Vector3 LeftDownPoint;
	public Vector3 RightDownPoint;

	public LineRenderer MinimapLine;

	public LayerMask _minimapmask;

	public CameraMove _cms;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float X;
		float Y;
		X = Screen.width / 100;
		Y = Screen.height / 100;
		if (!gameObject.GetComponent<GameMenu>().pause)
		{
			if (Input.mousePosition.y < Y * 25 & Input.mousePosition.y > Y * 0 & Input.mousePosition.x < X * 14 & Input.mousePosition.x > X * 0)
			{
				if (Input.GetMouseButtonDown(0))
				{ // if left button pressed...
					RaycastHit hit;
					Ray ray = itsMinimapCamera.ScreenPointToRay(Input.mousePosition);
					if (Physics.Raycast(ray, out hit)/* && hit.transform.name=="MinimapBackground"*/)
					{
						itsMainCamera.transform.position = new Vector3(hit.point.x, itsMainCamera.transform.position.y, hit.point.z);
						// hit.point contains the point where the ray hits the
						// object named "MinimapBackground"
					}
					//itsMainCamera.transform.position = Vector3.Lerp(itsMainCamera.transform.position, hit.point, 0.1f);
				}
			}
		}
		Camera _mc = Camera.main;
		RaycastHit tlhit1;
		Ray tlray1 = _mc.ScreenPointToRay(new Vector3(0, 0, 0));
		if (Physics.Raycast(tlray1, out tlhit1, 10000.0f, _minimapmask))
		{
			LeftUpPoint = tlhit1.point;
		}
		RaycastHit tlhit2;
		Ray tlray2 = _mc.ScreenPointToRay(new Vector3(Screen.width, 0, 0));
		if (Physics.Raycast(tlray2, out tlhit2, 10000.0f, _minimapmask))
		{
			RightUpPoint = tlhit2.point;
		}
		RaycastHit tlhit3;
		Ray tlray3 = _mc.ScreenPointToRay(new Vector3(0, Screen.height, 0));
		if (Physics.Raycast(tlray3, out tlhit3, 10000.0f, _minimapmask))
		{
			LeftDownPoint = tlhit3.point;
		}
		RaycastHit tlhit4;
		Ray tlray4 = _mc.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));
		if (Physics.Raycast(tlray4, out tlhit4, 10000.0f, _minimapmask))
		{
			RightDownPoint = tlhit4.point;
		}

		MinimapLine.SetPosition(0, LeftDownPoint);
		MinimapLine.SetPosition(1, LeftUpPoint);
		MinimapLine.SetPosition(2, RightUpPoint);
		MinimapLine.SetPosition(3, RightDownPoint);

		if (_cms.LocalMinimap.activeSelf)
		{
			MinimapLine.startWidth = 3;
			MinimapLine.endWidth = 3;
		}
		else
		{
			MinimapLine.startWidth = 20;
			MinimapLine.endWidth = 20;
		}
	}
}
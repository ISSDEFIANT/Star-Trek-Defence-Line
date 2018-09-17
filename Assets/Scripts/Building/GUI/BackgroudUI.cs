using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackgroudUI : MonoBehaviour 
{
	
	public GUISkin mainUI;
	public int numDepth = 0;
	
	public string nameWindow;
	public Sprite pictureSelectObject;
	public Texture2D Texture1;
	public Texture2D Texture2;
	public Texture2D Texture3;

	public Texture2D DilithiumTex;
	public Texture2D TiteniumTex;
	public Texture2D CrewTex;

	public float money;
	public int score;
	
	public Texture2D pictureDefault;
	public RenderTexture map;
	public Material mat;

	private GlobalDB _GDB;

	public GUIStyle Style;

	void Start () 
	{
		_GDB = gameObject.GetComponent<GlobalDB>();
	}
	
	void Update () 
	{
		float X;
		float Y;
		X = Screen.width / 100;
		Y = Screen.height / 100;

		if (!gameObject.GetComponent<GameMenu> ().pause) {
			if (_GDB.selectList.Count == 1 || _GDB.activeObjectInterface != null) {
				if (_GDB.activeObjectInterface == null) {
					if (Input.mousePosition.y < Y * 3 & Input.mousePosition.y > (Y * 3 - Y * 1 / 2) & Input.mousePosition.x < (X * 30) & Input.mousePosition.x > (X * 24)) {
						if (_GDB.selectList [0].GetComponent<HealthModule> ().CurShilds > 0) {
							gameObject.GetComponent<InfoPlane> ().UpdateText("Shields effectiveness – " + _GDB.selectList [0].GetComponent<HealthModule> ().CurShilds / _GDB.selectList [0].GetComponent<HealthModule> ().Shilds * 100 + "%",10);
						} else {
							gameObject.GetComponent<InfoPlane> ().UpdateText("Shields effectiveness – " + 0 + "%",10);
						}
					}
				} else {
					if (Input.mousePosition.y < Y * 3 & Input.mousePosition.y > (Y * 3 - Y * 1 / 2) & Input.mousePosition.x < (X * 30) & Input.mousePosition.x > (X * 24)) {
						if (_GDB.activeObjectInterface.GetComponent<HealthModule> ().CurShilds > 0) {
							gameObject.GetComponent<InfoPlane> ().UpdateText("Shields effectiveness – " + _GDB.activeObjectInterface.GetComponent<HealthModule> ().CurShilds / _GDB.activeObjectInterface.GetComponent<HealthModule> ().Shilds * 100 + "%",10);
						} else {
							gameObject.GetComponent<InfoPlane> ().UpdateText("Shields effectiveness – " + 0 + "%",10);
						}
					}
				}
			}
		}
	}
}

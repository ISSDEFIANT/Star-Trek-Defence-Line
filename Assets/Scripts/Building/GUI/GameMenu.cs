using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Xml.Linq;
using System.IO;
public class GameMenu : MonoBehaviour
{
	public bool Save;
	public bool Load;
	public bool Delete;

    public GUISkin mainUI;
    public int numDepth = 1;
    public bool pause = false;
	public string GameRule;
    public bool SaveMenu;

    public bool SaveAccapt;
    public bool DeleteAccapt;

    public string _saveName = string.Empty;

    public GUIStyle _saveInfoStyle;

	//private float timer = 3;

	public int SaveSystemSelect;
	public GameObject SsP;

	public Vector2 scrollPosition;

	public static string LoadName;
    // Use this for initialization
    void Start()
    {
		gameObject.GetComponent<Select> ().enabled = true;
		gameObject.GetComponent<Select>().isSelect = false;
		//pause = false;
		//Time.timeScale = 1;
    }

    // Update is called once per frame
	void LateUpdate(){
		if (Save) {
			Save = false;
		}
	}

    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pause = !pause;
		}
		if (pause) {
			gameObject.GetComponent<Select>().enabled = false;
			Time.timeScale = 0;
		} else {
			gameObject.GetComponent<Select>().enabled = true;
			Time.timeScale = 1;
		}
    }

    void OnGUI()
    {
		float X;
		float Y;
		X = Screen.width / 100;
		Y = Screen.height / 100;
        GUI.depth = numDepth;
        GUI.skin = mainUI;
		_saveInfoStyle.fontSize = Screen.height/25;
		if (Input.GetKeyDown(KeyCode.R)) {
			Quaternion GoRotation = Quaternion.Euler (0, 0, 0);
			GameObject.FindGameObjectWithTag ("CAMERAMOVE").transform.rotation = GoRotation;
			GameObject.FindGameObjectWithTag ("CAMERAMOVE").GetComponent<CameraMove>().Camera.transform.rotation = GoRotation;
		}
        if (pause)
		{
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), System.String.Empty, GUI.skin.GetStyle("BackgroudMenu"));
            if (!SaveMenu)
            {
				if (GUI.Button(new Rect(X * 85, Y * 90, X * 10, Y * 8), System.String.Empty)){
                    pause = false;
                }
				if (GUI.Button(new Rect(X * 75, Y * 90, X * 10, Y * 8), System.String.Empty))
                {
                    Time.timeScale = 1;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
				if (GUI.Button(new Rect(X * 65, Y * 90, X * 10, Y * 8), System.String.Empty))
                {
					SaveMenu = true;
                }
				if (GUI.Button(new Rect(X * 45, Y * 90, X * 10, Y * 8), System.String.Empty))
                {
                    SceneManager.LoadScene(1);
                    Time.timeScale = 1;
                }
				if (GUI.Button(new Rect(X * 35, Y * 90, X * 10, Y * 8), System.String.Empty))
                {
                    Time.timeScale = 1;
                    Application.Quit();
                }
				GUI.Label(new Rect(X * 1, Y * 1, X * 99, Y * 99), GameRule);

				GUI.depth = -2;
				GUI.Label(new Rect(X * 85, Y * 90, X * 10, Y * 8), "Continue", _saveInfoStyle);
				GUI.Label(new Rect(X * 75, Y * 90, X * 10, Y * 8), "Restart", _saveInfoStyle);
				GUI.Label(new Rect(X * 65, Y * 90, X * 10, Y * 8), "Save / Load", _saveInfoStyle);
				GUI.Label(new Rect(X * 45, Y * 90, X * 10, Y * 8), "Exit to main menu", _saveInfoStyle);
				GUI.Label(new Rect(X * 35, Y * 90, X * 10, Y * 8), "Exit to windows", _saveInfoStyle);
				GUI.depth = numDepth;
            }
            if (SaveMenu)
            {
				GUI.depth = -5;
				GUI.depth = numDepth;
				if (GUI.Button(new Rect(X * 85, Y * 90, X * 10, Y * 8), System.String.Empty))
                {
						SaveAccapt = true;
                }
				if (GUI.Button(new Rect(X * 75, Y * 90, X * 10, Y * 8), System.String.Empty))
                {
					XElement root = null;

					if (!File.Exists ("Data/Saves/" + LoadName)) {
					} else {
						root = XDocument.Parse (File.ReadAllText ("Data/Saves/" + LoadName)).Element ("root");
					}
					//Выставление элементов на сцену.
					GenerateScene (root);
                        pause = false;
                }
				if (GUI.Button(new Rect(X * 65, Y * 90, X * 10, Y * 8), System.String.Empty))
                {
                    DeleteAccapt = true;
                }
				if (GUI.Button(new Rect(X * 55, Y * 90, X * 10, Y * 8), System.String.Empty))
                {
                    SaveMenu = false;
                }
				//if (GUI.Button(new Rect(Screen.width - 1020, Screen.height - 100, 200, 80), "Clear"))
				//{
				//	
				//}
				GUI.depth = 1;
				GUI.Box(new Rect(X * 5, Y * 5, X * 40, Y * 60), System.String.Empty);
				GUI.Label(new Rect(X * 6, Y * 5, X * 5, Y * 60), "Saves");
				ShowLoadDialog ();
				if (SaveAccapt)
				{
					GUI.depth = -6;
					GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 150, 300, 300), "Save this game?");
					GUI.Label(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 130, 300, 300), "Save name");
//			FindObjectOfType<SameGameSystem> ().SaveName = GUI.TextField(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 110, 300, 100), FindObjectOfType<SameGameSystem> ().SaveName);
					if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 100, 25), "Yes"))
					{
						Save = true;
						SaveAccapt = false;
//						FindObjectOfType<SameGameSystem> ().Save();
					}
					if (GUI.Button(new Rect(Screen.width / 2 + 75, Screen.height / 2, 100, 25), "No"))
					{
						SaveAccapt = false;
					}
					GUI.depth = numDepth;
				}
				if (DeleteAccapt)
				{
					GUI.depth = -6;
					GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 150, 300, 300), "Delete this game?");
					if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 100, 25), "Yes"))
					{
						File.Delete ("Data/Saves/" + LoadName);
						DeleteAccapt = false;
					}
					if (GUI.Button(new Rect(Screen.width / 2 + 75, Screen.height / 2, 100, 25), "No"))
					{
						DeleteAccapt = false;
					}
					GUI.depth = numDepth;
				}
				GUI.depth = numDepth;
				//scrollPosition = new Vector2 (X * 60, Y * 10);
				GUI.depth = -2;
				GUI.Label(new Rect(X * 85, Y * 90, X * 10, Y * 8), "Save", _saveInfoStyle);
				GUI.Label(new Rect(X * 75, Y * 90, X * 10, Y * 8), "Load", _saveInfoStyle);
				GUI.Label(new Rect(X * 65, Y * 90, X * 10, Y * 8), "Delete", _saveInfoStyle);
				GUI.Label(new Rect(X * 55, Y * 90, X * 10, Y * 8), "Back", _saveInfoStyle);
				GUI.depth = numDepth;
            }
        }
    }
	private void GenerateScene(XElement root){
		foreach (XElement instance in root.Elements("instanceGDB")) {
		    SceneManager.LoadScene(int.Parse(instance.Attribute("LvlNum").Value));
	//		GlobalLoadSystem.load = true;
		}
	}
	private void ShowLoadDialog(){
		float X;
		float Y;
		X = Screen.width / 100;
		Y = Screen.height / 100;
		GUI.depth = 2;
		if (!Directory.Exists ("Data/Saves/")) {
			Directory.CreateDirectory ("Data/Saves/");
		}
		string[] files1 = Directory.GetFiles ("Data/Saves/"); // список всех файлов в директории C:\temp
		string nameButton;
		int LenghtScroll = files1.Length * 30;
		int y = 0;

		scrollPosition = GUI.BeginScrollView (new Rect (X * 6, Y * 10, X * 35, Y * 55), scrollPosition, new Rect (0, 0, 182, LenghtScroll), false, false);
		for (int i = 0; i < files1.Length; i++) {
			nameButton = Path.GetFileName (files1 [i]);
			if (GUI.Button (new Rect (0, y, 282, 30), nameButton)) {
				LoadName = nameButton;
			}
			y = y + 30;
		}
		GUI.EndScrollView ();
		GUI.depth = numDepth;
	}
}
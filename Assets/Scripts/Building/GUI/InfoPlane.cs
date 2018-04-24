using UnityEngine;
using System.Collections;

public class InfoPlane : MonoBehaviour {
	public GUIStyle style;
	public int numDepth = 1;
	public string Text;
	public int YScale;

	[HideInInspector]
	public bool Stop;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Stop) {
			Text = System.String.Empty;
			Stop = false;
		}
	}
	void OnGUI(){
		float X;
		float Y;
		X = Screen.width / 100;
		Y = Screen.height / 100;
		GUI.depth = numDepth;
		if (Text != System.String.Empty) {
			GUI.Box (new Rect (0, Y * (75 - YScale), X * 14, Y * YScale), System.String.Empty);
		}
		style.fontSize = Screen.height/40;
		GUI.Label (new Rect (0, Y * (75 - YScale), X * 14, Y * 25), Text, style);
	}
	public void UpdateText(string NewText, int Percent){
		Text = NewText;
		YScale = Percent;
	}
}

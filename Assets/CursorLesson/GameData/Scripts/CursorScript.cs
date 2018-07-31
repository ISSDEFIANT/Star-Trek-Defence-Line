using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{
	public int numDepth = 1;
	public Texture2D Static_cursor;
	public Texture2D[] Cursors;
	private int i;
	public float TimerChange;
	private Texture2D cur;
	public bool UseStaticCursor;
	private float TimerDown;

	public float MinusX;
	public float MinusY;

	public float SizeX = 25;
	public float SizeY = 25;

	// Use this for initialization
	void Start()
	{
		TimerDown = TimerChange;
		//if(!UseStaticCursor)cur = Cursors[i];
	}

	// Update is called once per frame
	void Update()
	{
		UnityEngine.Cursor.visible = false;
		if (i >= Cursors.Length)
		{
			i = 0;
		}
	}



	void OnGUI()
	{
		GUI.depth = numDepth;
		Vector2 MP = Input.mousePosition;
		MP.y = Screen.height - MP.y;
		MP.x -= MinusX;
		MP.y -= MinusY;
		if (UseStaticCursor)
		{
			GUI.DrawTexture(new Rect(MP.x, MP.y, SizeX, SizeY), Static_cursor);
		}
		else
		{

			GUI.DrawTexture(new Rect(MP.x, MP.y, SizeX, SizeY), cur);
			TimerDown -= Time.deltaTime;
			if (TimerDown <= 0)
			{
				if (Cursors[i] != null)
				{
					cur = Cursors[i];
				}
				i++;
				TimerDown = TimerChange;
			}
		}
	}
}
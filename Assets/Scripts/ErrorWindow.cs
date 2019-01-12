using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorWindow : MonoBehaviour
{
    private static bool messageWindowActive;
    private static string curMessage;

    public static void ShowError(string message)
    {
        ErrorWindow.curMessage = message;
        ErrorWindow.messageWindowActive = true;
    }

    void OnGUI()
    {
        float x = Screen.width / 100;
        float y = Screen.height / 100;

        GUIStyle[] styles = new GUIStyle[3];
        styles[0] = new GUIStyle(GUI.skin.box);
        styles[1] = new GUIStyle(GUI.skin.label);
        styles[2] = new GUIStyle(GUI.skin.button);


        foreach (GUIStyle style in styles)
        {
            style.fontSize = (int)(y * 3);
        }

        if (ErrorWindow.messageWindowActive)
        {
            GUI.color = Color.red;
            GUI.Box(new Rect(x * 25, y * 5, x * 60, y * 90), "Error", styles[0]);
            GUI.Label(new Rect(x * 30, y * 10, x * 50, y * 5),
                "Произошла ошибка. Пожалуйста, напишите нам о ней в багтрекер:", styles[1]);
            GUI.Label(new Rect(x * 30, y * 15, x * 50, y * 80), ErrorWindow.curMessage, styles[1]);
            GUI.color = Color.white;
            if (GUI.Button(new Rect(x * 30, y * 85, x * 5, y * 3), "Close", styles[2]))
            {
                ErrorWindow.messageWindowActive = false;
            }

            if (GUI.Button(new Rect(x * 60, y * 85, x * 20, y * 5), "Open bugtracker", styles[2]))
            {
                Application.OpenURL("https://github.com/ISSDEFIANT/Star-Trek-Defence-Line/issues");
            }
        }
    }
}
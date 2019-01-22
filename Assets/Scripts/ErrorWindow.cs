using System;
using UnityEngine;

//TODO localize
public sealed class ErrorWindow : MonoBehaviour
{
    private static readonly object Lock = new object();
    private static bool _messageWindowActive;
    private static string _curMessage;

    /// <summary>
    /// Show error message to the user
    /// </summary>
    /// <param name="message">the message</param>
    public static void ShowErrorMessage(string message)
    {
        lock (Lock)
        {
            _curMessage = message;
            _messageWindowActive = true;
        }
    }

    /// <summary>
    /// Show exception to the user
    /// </summary>
    /// <param name="e">the exception</param>
    public static void ShowException(Exception e)
    {
        lock (Lock)
        {
            _curMessage = e.ToString();
            _messageWindowActive = true;
        }
    }

    void OnGUI()
    {
        string msg;
        lock (Lock)
        {
            if (!_messageWindowActive)
                return;
            msg = _curMessage;
        }

        var x = Screen.width / 100F;
        var y = Screen.height / 100F;
        var fontSize = (int) (y * 3);

        var box = new GUIStyle(GUI.skin.box)
        {
            fontSize = fontSize
        };
        var label = new GUIStyle(GUI.skin.label)
        {
            fontSize = fontSize
        };
        var button = new GUIStyle(GUI.skin.button)
        {
            fontSize = fontSize
        };

        GUI.color = Color.red;
        GUI.Box(new Rect(x * 25, y * 5, x * 60, y * 90), "Error", box);
        GUI.Label(new Rect(x * 30, y * 10, x * 50, y * 5),
            "Произошла ошибка. Пожалуйста, напишите нам о ней в багтрекер:", label);
        GUI.Label(new Rect(x * 30, y * 15, x * 50, y * 80), msg, button);
        GUI.color = Color.white;
        if (GUI.Button(new Rect(x * 30, y * 85, x * 5, y * 3), "Close", button))
            lock (Lock)
                _messageWindowActive = false;

        if (GUI.Button(new Rect(x * 60, y * 85, x * 20, y * 5), "Open bugtracker", button))
            Application.OpenURL("https://github.com/ISSDEFIANT/Star-Trek-Defence-Line/issues");
    }
}
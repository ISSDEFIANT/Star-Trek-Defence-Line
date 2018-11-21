using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu2 : MonoBehaviour
{
    public GameObject PlaneX;
    public GameObject NextPlane;
    public bool Button;
    public bool Exit;
    public bool Faster;
    public bool Good;
    public bool Beautiful;
    public bool Fantastic;
    public bool AudioOn;
    public bool AudioOff;
    public bool Play;
    public bool Load;
    public int Level;
    public Color Color1;
    public Color Color2;
    public AudioClip clip1;
    public AudioClip clip2;
    public bool X;
    public bool Down;
    private float Timer;
    private bool TimerSet;

    public GameObject LoadScreen;

    public GameObject GameName;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color2;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1;
        if (Down)
        {
            if (TimerSet)
            {
                Timer += Time.deltaTime;
            }

            if (Timer > 0.8f)
            {
                TimerSet = false;
                Timer = 0;
                if (Exit)
                {
                    Application.Quit();
                }

                if (Button)
                {
                    NextPlane.SetActive(true);
                    PlaneX.SetActive(false);
                }

                if (Faster)
                    Settings.Settings.Instance.Graphics = Settings.Settings.GraphicsMode.Faster;
                else if (Good)
                    Settings.Settings.Instance.Graphics = Settings.Settings.GraphicsMode.Good;
                else if (Beautiful)
                    Settings.Settings.Instance.Graphics = Settings.Settings.GraphicsMode.Beautiful;
                else if (Fantastic)
                    Settings.Settings.Instance.Graphics = Settings.Settings.GraphicsMode.Fantastic;

                if (AudioOn)
                    Settings.Settings.Instance.SoundLevel = 1.0F;
                else if (AudioOff)
                    Settings.Settings.Instance.SoundLevel = 0.0F;

                if (Play)
                {
                    LoadScreen.SetActive(true);
                    GameName.SetActive(false);
                    SceneManager.LoadScene(Level);
                }
            }
        }
    }

    public void PanelSwinch()
    {
        NextPlane.SetActive(true);
        PlaneX.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void FastestButton()
    {
        Settings.Settings.Instance.Graphics = Settings.Settings.GraphicsMode.Fastest;
    }

    public void FasterButton()
    {
        Settings.Settings.Instance.Graphics = Settings.Settings.GraphicsMode.Faster;
    }

    public void SimpleButton()
    {
        Settings.Settings.Instance.Graphics = Settings.Settings.GraphicsMode.Simple;
    }

    public void GoodButton()
    {
        Settings.Settings.Instance.Graphics = Settings.Settings.GraphicsMode.Good;
    }

    public void BeautifulButton()
    {
        Settings.Settings.Instance.Graphics = Settings.Settings.GraphicsMode.Beautiful;
    }

    public void FantasticButton()
    {
        Settings.Settings.Instance.Graphics = Settings.Settings.GraphicsMode.Fantastic;
    }

    public void AudioOnButton()
    {
        Settings.Settings.Instance.SoundLevel = 1.0F;
    }

    public void AudioOffButton()
    {
        Settings.Settings.Instance.SoundLevel = 0.0F;
    }

    public void PlayButton()
    {
        LoadScreen.SetActive(true);
        GameName.SetActive(false);
        SceneManager.LoadScene(Level);
    }

    void OnMouseEnter()
    {
        if (gameObject.GetComponent<Renderer>())
        {
            gameObject.GetComponent<Renderer>().material.color = Color1;
        }

        if (X)
        {
            AudioSource.PlayClipAtPoint(clip1, gameObject.transform.position);
        }
    }

    void OnMouseExit()
    {
        if (gameObject.GetComponent<Renderer>())
        {
            gameObject.GetComponent<Renderer>().material.color = Color2;
        }
    }

    void OnMouseDown()
    {
        if (X)
        {
            AudioSource.PlayClipAtPoint(clip2, gameObject.transform.position);
        }

        TimerSet = true;
        Down = true;
        if (gameObject.GetComponent<Renderer>())
        {
            gameObject.GetComponent<Renderer>().material.color = Color2;
        }
    }
}
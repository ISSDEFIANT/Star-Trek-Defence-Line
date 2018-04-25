using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bl_PauseMenu : MonoBehaviour {

    public static PauseState m_PauseState = PauseState.None;
    /// <summary>
    /// Global var for know is pause game
    /// </summary>
    public static bool m_Pause = false;
    [Header("Pause Main")]
    public GameObject PauseUI = null;
    public string m_PauseShowAnim = "PauseMenuShow";
    public string m_PauseHideAnim = "PauseMenuHide";
    public string m_PauseMovedHideAnim = "PauseMenuMovedHide";
    public string m_PauseMoveAnim = "PauseMenuToLeft";
    public string m_PauseMoveReturnAnim = "PauseMenuToCenter";
    [Space(5)]
    [Header("Pause Options")]
    public GameObject OptionsUI = null;
    public string OptionsHideAnim = "OptionsHide";
    [Space(5)]
    [Header("Pause Credits")]
    public GameObject CreditsUI = null;
    public string CreditsHideAnim = "CreditsHide";
    [Space(5)]
    public Image SemiBack = null;
    [Range(0.0f,1.0f)]
    public float MaxAlpha = 0.75f;
    //private 
    private bool isMoved = false;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        if (PauseUI != null)
        {
            PauseUI.SetActive(false);
        }
        if (OptionsUI != null)
        {
            OptionsUI.SetActive(false);
        }
        if (CreditsUI != null)
        {
            CreditsUI.SetActive(false);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DoPause();
        }
        //Fade effect
        if (SemiBack != null)
        {
            Color a = SemiBack.color;
            if (m_Pause && a.a < MaxAlpha)
            {
                a.a = Mathf.Lerp(a.a, MaxAlpha, Time.deltaTime * 5);
            }
            else if (a.a > 0.0f)
            {
                a.a = Mathf.Lerp(a.a, 0.0f, Time.deltaTime * 5);
            }
            SemiBack.color = a;
        }
        
    }
    /// <summary>
    /// 
    /// </summary>
    public void DoPause()
    {
        if (PauseUI != null)
        {
            //True or False
            m_Pause = !m_Pause;
            if (m_Pause)
            {
                //Active Pause UI with animation
                PauseUI.SetActive(true);
                PauseUI.GetComponent<Animation>().Play(m_PauseShowAnim);
                m_PauseState = PauseState.Main;
            }
            else
            {
                //This animation content a event for auto desactive
                //when animation finished
                if (isMoved)
                {
                    PauseUI.GetComponent<Animation>().Play(m_PauseMovedHideAnim);
                }
                else
                {
                    PauseUI.GetComponent<Animation>().Play(m_PauseHideAnim);
                }
                //If options active, then hide too
                if (OptionsUI.activeSelf)
                {
                    OptionsUI.GetComponent<Animation>().Play(OptionsHideAnim);
                }
                //If you do not want to disable animation for event
                //use this:
                //StartCoroutine(DesactiveInTime(PauseUI,2f);
                if (CreditsUI.activeSelf)
                {
                    CreditsUI.GetComponent<Animation>().Play(CreditsHideAnim);
                }

                m_PauseState = PauseState.None;
            }
        }else{
        
            Debug.LogError("Pause UI is Emty please add this in inspector");
        }
        isMoved = false;
    }
    /// <summary>
    /// 
    /// </summary>
    public void DoMain()
    {
        if (OptionsUI.activeSelf)
        {
            OptionsUI.GetComponent<Animation>().Play(OptionsHideAnim);
        }
        if (CreditsUI.activeSelf)
        {
            CreditsUI.GetComponent<Animation>().Play(CreditsHideAnim);
        }
        PauseUI.GetComponent<Animation>().Play(m_PauseMoveReturnAnim);
        isMoved = false;
        m_PauseState = PauseState.Main;

    }
    /// <summary>
    /// 
    /// </summary>
    public void DoOptions()
    {
        if (!OptionsUI.activeSelf )
        {
           
            if (CreditsUI.activeSelf)
            {
                CreditsUI.GetComponent<Animation>().Play(CreditsHideAnim);
            }
            //This animation have a event to call Options UI show
            if (!isMoved)
            {
                PauseUI.GetComponent<Animation>().Play(m_PauseMoveAnim);
            }
            else
            {
                OptionsUI.SetActive(true);
            }
            isMoved = true;
            //If you do not want to disable animation for event
            //use this:
            m_PauseState = PauseState.Options;
        }
        else
        {
            OptionsUI.GetComponent<Animation>().Play(OptionsHideAnim);
            PauseUI.GetComponent<Animation>().Play(m_PauseMoveReturnAnim);
            isMoved = false;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void DoCredits()
    {
        if (!CreditsUI.activeSelf )
        {
            
            if (OptionsUI.activeSelf)
            {
                OptionsUI.GetComponent<Animation>().Play(OptionsHideAnim);
            }
            //This animation have a event to call Options UI show
            if (!isMoved)
            {
                PauseUI.GetComponent<Animation>().Play(m_PauseMoveAnim);
            }
            else
            {
                CreditsUI.SetActive(true);
            }
            isMoved = true;
            m_PauseState = PauseState.Credits;
        }
        else
        {
            CreditsUI.GetComponent<Animation>().Play(CreditsHideAnim);
            PauseUI.GetComponent<Animation>().Play(m_PauseMoveReturnAnim);
            isMoved = false;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
    /// <summary>
    /// Simple Restart Scene
    /// </summary>
    public void SimpleRestart()
    {
        m_Pause = false;
        m_PauseState = PauseState.None;
        Application.LoadLevel(Application.loadedLevelName);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="go"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator DesactiveInTime(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(false);
    }
}
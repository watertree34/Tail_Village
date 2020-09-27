using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;  // 싱글톤
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    /*--------------------스타트버튼 클릭--------------------*/
    public bool clickStart = false;
    public void OnClickStart()
    {
        clickStart = true;
        Soundmanager.Instance.ButtonSound();
    }

    /*--------------------스킵버튼 클릭-------------------*/
    public bool clickSkip = false;
    public void OnClickSkip()
    {
        clickSkip = true;
        Soundmanager.Instance.ButtonSound();
    }

    /*--------------------엑시트버튼 클릭--------------------*/
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Soundmanager.Instance.ButtonSound();
#else
          Application.Quit();
#endif
    }

    /////다시시작///
   
    public void OnClickRestart()
    {
        SceneManager.LoadScene("GameScene1");
       
    }
}

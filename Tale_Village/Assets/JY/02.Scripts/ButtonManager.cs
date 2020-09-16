using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;  // 싱글톤
    private void Awake()
    {
        Instance = this;
    }

    /*--------------------스타트버튼 클릭--------------------*/
    public bool clickStart = false;
    public void OnClickStart()
    {
        clickStart = true;
    }

    /*--------------------스킵버튼 클릭-------------------*/
    public bool clickSkip = false;
    public void OnClickSkip()
    {
        clickSkip = true;
    }

    /*--------------------엑시트버튼 클릭--------------------*/
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
          Application.Quit();
#endif
    }
}

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

    public bool clickStart = false;
    public void OnClickStart()
    {
        clickStart = true;
    }

    public bool clickSkip = false;
    public void OnClickSkip()
    {
        clickSkip = true;
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
          Application.Quit();
#endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{

    //사용-UIText.Instance.UITEXT="넣을 문구";
    public static UIText Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Text uiText;
    public string text;
    

    public string UITEXT
    {
        get { return text; }
        set
        {
            text = value;
            uiText.text = text;

        }
    }

    private void FixedUpdate()
    {
        UITEXT = "";
    }






}

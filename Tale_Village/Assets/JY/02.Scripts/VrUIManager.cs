using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VrUIManager : MonoBehaviour
{
    public GameObject GameTitleUI;     //게임타이틀UI
    public GameObject InvertedSphere;  //게임스크립트배경
    public Text OpeningTxt;            //오프닝텍스트
    public GameObject Btn_OpeningSkip; //오프닝 스킵 버튼
    public GameObject GamePlayUI;      //게임플레이중UI
    public GameObject GameOverUI;      //게임오버UI
    
    bool isOpeningEnd = false;         //스크립트 엔딩 판별
    bool isFadeMax = false;            //글자 알파값 판별
    bool timeToTxtChange = false;      //스크립트 변경

    int txtLineIdx = 0;                //인트로 스크립트 변경용

    float fade = 0.0f;                 //페이드인/아웃용 상수
    float curTime = 0.0f;              //현재 시간

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene1") //씬이름으로 현재씬 찾기
        {
            GameTitleUI.SetActive(true);
            InvertedSphere.SetActive(false);
            OpeningTxt.enabled = false;
            OpeningTxt.color = new Color(OpeningTxt.color.r, OpeningTxt.color.g, OpeningTxt.color.b, 0);
            Btn_OpeningSkip.SetActive(false);
            GamePlayUI.SetActive(false);
            GameOverUI.SetActive(false);
        }
        else
        {
            GameTitleUI = null;
            InvertedSphere = null;
            OpeningTxt = null;
            Btn_OpeningSkip = null;
            GamePlayUI.SetActive(true);
            GameOverUI.SetActive(false);
        }
    }

    void Update()
    {
        /*--------------------스타트 버튼 누르면--------------------*/
        if (ButtonManager.Instance.clickStart == true)
        {
            GameTitleUI.SetActive(false);
            InvertedSphere.SetActive(true);
            OpeningTxt.enabled = true;
            Btn_OpeningSkip.SetActive(true);
            if (isFadeMax == false)
            {
                FadeIn(OpeningTxt);
            }
            if (isFadeMax == true)
            {
                FadeOut(OpeningTxt);
            }
        }

        /*--------------------텍스트 변경할 때가 오면 (페이드아웃 함수 안에서 관리)--------------------*/
        if (timeToTxtChange)
        {
            txtLineIdx += 1;
            ChangeTxt(txtLineIdx);
            timeToTxtChange = false;
            if (txtLineIdx > 4)
            {
                isOpeningEnd = true;
            }
        }

        /*--------------------스킵버튼으로 스킵하기--------------------*/
        if (ButtonManager.Instance.clickSkip == true)
        {
            isOpeningEnd = true;
            ButtonManager.Instance.clickSkip = false;
        }

        /*--------------------오프닝 끝나면 스타트이미지, 텍스트 사라지게 하기--------------------*/
        if (isOpeningEnd == true)
        {
            InvertedSphere.SetActive(false);
            OpeningTxt.enabled = false;
            Btn_OpeningSkip.SetActive(false);
            GamePlayUI.SetActive(true);
            ButtonManager.Instance.clickStart = false;
        }

        /*--------------------라이프 0되면 게임오버창 띄우기--------------------*/
        if (LifeManager.Instance.LIFE == 0)
        {
            GamePlayUI.SetActive(false);
            GameOverUI.SetActive(true);
        }
    }

    /*--------------------페이드인 함수--------------------*/
    void FadeIn(Text text)
    {
        fade += 0.02f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, fade);
        if (fade >= 1.0f)
        {
            fade = 1.0f;
            curTime += Time.deltaTime;
            print("time " + curTime);
            if (curTime >= 2.3f)
            {
                curTime = 0;
                isFadeMax = true;
            }
        }
    }

    /*--------------------페이드아웃 함수--------------------*/
    void FadeOut(Text text)
    {
        fade -= 0.02f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, fade);
        if (fade <= 0.0f)
        {
            fade = 0.0f;
            curTime += Time.deltaTime;
            print("time " + curTime);
            if (curTime >= 0.7f)
            {
                curTime = 0;
                timeToTxtChange = true;
                isFadeMax = false;
            }
        }
    }

    /*--------------------오프닝 스크립트 바꿔주는 함수--------------------*/
    void ChangeTxt(int idx)
    {
        switch (idx)
        {
            default:
                OpeningTxt.text = "어느날 마을 한구석에 자라난, 거대한 콩나무.";
                break;
            case 1:
                OpeningTxt.text = "길게 뻗은 콩나무는 하늘에 있는 거인의 집까지 닿았고,";
                break;
            case 2:
                OpeningTxt.text = "이를 발견한 거인은 마을에 내려와 난동을 피우다\n황금알을 낳는 거위를 훔쳐가버렸어요.";
                break;
            case 3:
                OpeningTxt.text = "다행히도 다친 사람은 없었지만 소중한 거위를 빼앗겼으니 큰일이에요.";
                break;
            case 4:
                OpeningTxt.text = "자 그럼, 거인이 잠든 틈을 타 거위를 구출하러 가볼까요?";
                break;
        }
    }
}

/* 어느날 마을 한구석에 자라난, 거대한 콩나무.
길게 뻗은 콩나무는 하늘에 있는 거인의 집까지 닿았고,
이를 발견한 거인은 마을에 내려와 난동을 피우다
황금알을 낳는 거위를 훔쳐가버렸어요.

다행히도 다친 사람은 없었지만 소중한 거위를 빼았겼으니 큰일이에요.
자 그럼, 거인이 잠든 틈을 타 거위를 구출하러 가볼까요?*/

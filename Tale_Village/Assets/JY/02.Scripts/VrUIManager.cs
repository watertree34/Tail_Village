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

    float curTime = 0.0f;          //현재시간
    float fade = 1.0f;             //페이드인/아웃용 상수

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene1" || SceneManager.GetActiveScene().name == "GameScene1_UI조정씬") //씬이름으로 현재씬 찾기
        {
            GameTitleUI.SetActive(true);
            InvertedSphere.SetActive(false);
            OpeningTxt.enabled = false;
            Btn_OpeningSkip.SetActive(false);
            GamePlayUI.SetActive(true);
            GameOverUI.SetActive(false);
        }
        else
        {
            GameTitleUI = null;
            InvertedSphere.SetActive(false);
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
            //시간 쌓이다가
            curTime += Time.deltaTime;
        }

        /*--------------------스타트이미지, 텍스트 사라지게 하기--------------------*/
        if (fade > 0.0f && curTime >= 6.0f)
        {
            InvertedSphere.SetActive(false);
            OpeningTxt.enabled = false;
            Btn_OpeningSkip.SetActive(false);
            fade -= 0.01f;
            //StartImg.color = new Color(1, 1, 1, fade);
            if (fade < 0.0f)
            {
                fade = 0.0f;
                curTime = 0;
            }
        }

        /*--------------------스킵버튼으로 스킵하기--------------------*/
        if (ButtonManager.Instance.clickSkip == true)
        {
            InvertedSphere.SetActive(false);
            OpeningTxt.enabled = false;
            Btn_OpeningSkip.SetActive(false);
            ButtonManager.Instance.clickSkip = false;
        }

        /*--------------------라이프 0되면 게임오버창 띄우기--------------------*/
        if (LifeManager.Instance.LIFE == 0)
        {
            GamePlayUI.SetActive(false);
            GameOverUI.SetActive(true);
        }
    }
}

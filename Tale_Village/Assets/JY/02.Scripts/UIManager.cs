using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject GameTitleUI;  //게임타이틀UI
    public Image StartImg;         //시작UI이미지
    public Text OpeningTxt;        //오프닝텍스트
    public GameObject GamePlayUI;  //게임플레이중UI
    public GameObject GameOverUI;  //게임오버UI

    float curTime = 0.0f;          //현재시간
    float fade = 1.0f;             //페이드인/아웃용 상수

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene1") //씬이름으로 현재씬 찾기
        {
            GameTitleUI.SetActive(true);
            StartImg.enabled = true;
            OpeningTxt.enabled = true;
            GamePlayUI.SetActive(true);
            GameOverUI.SetActive(false);
        }
        else
        {
            GameTitleUI = null;
            StartImg = null;
            OpeningTxt = null;
            GamePlayUI.SetActive(true);
            GameOverUI.SetActive(false);
        }
    }

    void Update()
    {
        if (ButtonManager.Instance.clickStart == true)
        {
            GameTitleUI.SetActive(false);
            //시간 쌓이다가
            curTime += Time.deltaTime;
        }

        /*--------------------스타트이미지, 텍스트 사라지게 하기--------------------*/
        if (fade > 0.0f && curTime >= 6.0f)
        {
            OpeningTxt.enabled = false;
            fade -= 0.01f;
            StartImg.color = new Color(1, 1, 1, fade);
            if (fade < 0.0f)
            {
                fade = 0.0f;
                curTime = 0;
            }
        }

        /*--------------------라이프 0되면 게임오버창 띄우기--------------------*/
        if (LifeManager.Instance.LIFE == 0)
        {
            GamePlayUI.SetActive(false);
            GameOverUI.SetActive(true);
        }
    }
}

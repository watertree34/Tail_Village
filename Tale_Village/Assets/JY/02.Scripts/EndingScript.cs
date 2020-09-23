using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour
{
    public GameObject InvertedSphere;  //인버티드 스피어
    public Image backGroundImg;        //배경이미지
    public Text endingTxt;             //오프닝텍스트
    public GameObject Btn_Exit;        //종료 버튼

    bool isEndingEnd = false;         //스크립트 엔딩 판별
    bool isFadeMax = false;            //글자 알파값 판별
    bool timeToTxtChange = false;      //스크립트 변경

    int txtLineIdx = 0;                //인트로 스크립트 변경용

    float fade = 0.0f;                 //페이드인/아웃용 상수
    float curTime = 0.0f;              //현재 시간

    void Start()
    {
        InvertedSphere.SetActive(true);
        backGroundImg.enabled = false;
        endingTxt.enabled = true;
        endingTxt.color = new Color(endingTxt.color.r, endingTxt.color.g, endingTxt.color.b, 0);
        Btn_Exit.SetActive(false);
    }

    void Update()
    {
        /*--------------------페이드 인 / 아웃--------------------*/
        if (isFadeMax == false)
        {
            FadeIn(endingTxt);
        }
        if (isFadeMax == true)
        {
            FadeOut(endingTxt);
        }

        /*--------------------텍스트 변경할 때가 오면 (페이드아웃 함수 안에서 관리)--------------------*/
        if (timeToTxtChange)
        {
            txtLineIdx += 1;
            ChangeTxt(txtLineIdx);
            timeToTxtChange = false;
            if (txtLineIdx > 5)
            {
                isEndingEnd = true;
            }
        }

        /*--------------------오프닝 끝나면 스타트이미지, 텍스트 사라지게 하기--------------------*/
        if (isEndingEnd == true)
        {
            Btn_Exit.SetActive(true);
            endingTxt.enabled = false;
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
            if (curTime >= 0.7f)
            {
                curTime = 0;
                timeToTxtChange = true;
                isFadeMax = false;
            }
        }
    }

    /*--------------------엔딩 스크립트 바꿔주는 함수--------------------*/
    void ChangeTxt(int idx)
    {
        switch (idx)
        {
            default:
                endingTxt.text = "위험한 순간도 있었지만\n무사히 밖으로 빠져나오는데 성공했네요!";
                break;
            case 1:
                endingTxt.text = "이제 가족과 이웃이 기다리는 마을로 돌아갈 시간이에요.";
                break;
            case 2:
                InvertedSphere.SetActive(false);
                backGroundImg.enabled = true;
                endingTxt.text = "거인의 집에서 재빨리 도망쳐 나와 땅에 내려온 후엔\n마을 사람들과 함께 힘을 합쳐 콩나무를 잘라냈어요.";
                break;
            case 3:
                endingTxt.text = "하늘과 땅을 연결하던 거대한 콩나무는\n큰 소리를 내며 무너져 사라져버렸습니다.";
                break;
            case 4:
                endingTxt.text = "이제 거인이 다시 마을에 찾아올 일은 없겠죠?";
                break;
            case 5:
                endingTxt.text = "성공적인 구출을 기념하며\n오늘 밤은 안심하고 푹 잘 수 있겠어요.";
                break;
        }
    }
}

/*위험한 순간도 있었지만 무사히 밖으로 빠져나오는데 성공했네요!
이제 따뜻한 가족과 이웃이 기다리는 마을로 돌아갈 시간이에요.

 (윗단락 출력 이후, 해당 지문 다 지우고 새 단락으로 시작)

 재빨리 땅에 내려온 후엔 마을 사람들과 힘을 합쳐 콩나무를 잘라냈어요.
하늘과 땅을 연결하던 거대한 콩나무는 큰 소리를 내며 무너져 사라져버렸습니다.
이제 거인이 다시 마을에 찾아올 일은 없겠죠?
성공적인 구출을 기념하며 오늘 밤은 안심하고 푹 잘 수 있겠어요.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TxtFade : MonoBehaviour
{
    public Text text;

    void Awake()
    {
        StartCoroutine(FadeTextToFullAlpha());
    }

    public IEnumerator FadeTextToFullAlpha() // 알파값 0에서 1로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
        //StartCoroutine(FadeTextToZero());
    }

    public IEnumerator FadeTextToZero()  // 알파값 1에서 0으로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        //StartCoroutine(FadeTextToFullAlpha());
    }

    private void Update()
    {
        StartCoroutine(FadeTextToZero());
        //text.text = "길게 뻗은 콩나무는 하늘에 있는 거인의 집까지 닿았고,";
        //StartCoroutine(FadeTextToFullAlpha());
        //StartCoroutine(FadeTextToZero());
        //text.text = "이를 발견한 거인은 마을에 내려와 난동을 피우다\n황금알을 낳는 거위를 훔쳐가버렸어요.";
        //StartCoroutine(FadeTextToFullAlpha());
        //StartCoroutine(FadeTextToZero());
        //text.text = "다행히도 다친 사람은 없었지만 소중한 거위를 빼았겼으니 큰일이에요.";
        //StartCoroutine(FadeTextToFullAlpha());
        //StartCoroutine(FadeTextToZero());
        //text.text = "자 그럼, 거인이 잠든 틈을 타 거위를 구출하러 가볼까요?";
        //StartCoroutine(FadeTextToFullAlpha());
        //StartCoroutine(FadeTextToZero());

    }
}

/* 어느날 마을 한구석에 자라난, 거대한 콩나무.
길게 뻗은 콩나무는 하늘에 있는 거인의 집까지 닿았고,
이를 발견한 거인은 마을에 내려와 난동을 피우다
황금알을 낳는 거위를 훔쳐가버렸어요.

다행히도 다친 사람은 없었지만 소중한 거위를 빼았겼으니 큰일이에요.
자 그럼, 거인이 잠든 틈을 타 거위를 구출하러 가볼까요?*/

/*엔딩멘트*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//라이프 사용방법: LifeManager.Instance.LIFE +=( 감소 or 증가하고 싶은 라이프 숫자);   예) LifeManager.Instance.LIFE -= 1 ;--> 1만큼 라이프 감소

//라이프 감소할때: 플레이어 낙하, 벌레 닿을때, 거인에게 어택당할때
//라이프 증가할때: 플레이어가 콩, 음식 먹을때

///////////////////////////////////////////////////

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;  // 싱글톤

    public Text lifeUI;
    float playerLife;

    public float LIFE
    {
        get { return playerLife; }
        set
        {
            playerLife = value;
            playerLife = Mathf.Clamp(playerLife, 0, 50);   // life는 0~50까지
            if (playerLife == 0)
                lifeUI.text = "Game Over";
            else
                lifeUI.text = "Life : " + playerLife.ToString();
        }
    }

    private void Awake()
    {
        Instance = this;
        LIFE = 50;
    }
   
}

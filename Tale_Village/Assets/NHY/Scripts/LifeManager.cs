using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//라이프 사용방법: LifeManager.Instance.LIFE +=( 감소 or 증가하고 싶은 라이프 숫자);   예) LifeManager.Instance.LIFE -= 1 ;--> 1만큼 라이프 감소

//라이프 감소할때: 플레이어 낙하, 벌레 닿을때, 거인에게 어택당할때
//라이프 증가할때: 플레이어가 콩, 음식 먹을때

///////////////////////////////////////////////////

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;  // 싱글톤
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

    //public Text lifeUI;
    public Slider lifeUI;
    static float playerLife = 40;


    public float LIFE
    {
        get { return playerLife; }
        set
        {
            playerLife = value;
            playerLife = Mathf.Clamp(playerLife, 0, 100);   // life는 0~100까지


            lifeUI.value = (playerLife);

        }
    }

    private void Start()
    {
        lifeUI.maxValue = 100;
        lifeUI.minValue = 0;
        LIFE = playerLife;
    }

}

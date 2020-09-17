using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerTouch : MonoBehaviour
{
    CharacterController cc;
    float delayTime;
    bool mouseTouch;

    private void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(mouseTouch)
        {
            delayTime += Time.deltaTime;

            if (delayTime <= 1)  //뒤로 살짝 밀려나기
            {
                cc.Move( Vector3.Lerp(transform.position,-Camera.main.transform.forward, 1));
            }
            else
            {
                delayTime = 0;
                mouseTouch = false;
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag.Contains("finish") && Duck.Instance.openCage == true)  // 플레이어가 거위를 잡았고 출구에 닿으면
            SceneManager.LoadScene("ClearScene");   // 게임씬2->클리어씬 전환

        if (other.gameObject.tag.Contains("mouse"))  // 쥐에 닿으면
        {
            mouseTouch = true;
          
            
            LifeManager.Instance.LIFE -= 5f; //플레이어 라이프 감소
        }

        if (other.gameObject.tag.Contains("GiantHouseEntry"))   // 플레이어가 거인의 집 입구에 닿으면
        {
            SceneManager.LoadScene("GameScene2");   // 게임씬1->게임씬2전환
        }
    }
}

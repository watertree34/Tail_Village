using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTouch : MonoBehaviour
{
    CharacterController cc;
    private void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

   
    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag.Contains("finish") && Duck.Instance.openCage == true)  // 플레이어가 거위를 잡았고 출구에 닿으면
            SceneManager.LoadScene("ClearScene");   // 게임씬2->클리어씬 전환

        if (other.gameObject.tag.Contains("mouse"))  // 쥐에 닿으면
        { 
            cc.Move(transform.forward * 10);  // 살짝 뒤로 밀려나고
            LifeManager.Instance.LIFE -= 5f; //플레이어 라이프 감소
        }
        if (other.gameObject.tag.Contains("GiantHouseEntry"))   // 플레이어가 거인의 집 입구에 닿으면
        {
            SceneManager.LoadScene("GameScene2");   // 게임씬1->게임씬2전환
        }
    }
}

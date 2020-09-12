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
        if (other.gameObject.tag.Contains("finish") && Duck.Instance.openCage == true)
            SceneManager.LoadScene("ClearScene");

        if (other.gameObject.tag.Contains("mouse"))
        { 
            cc.Move(transform.forward * 10);
            LifeManager.Instance.LIFE -= 5f; //플레이어 라이프 감소
        }
        if (other.gameObject.tag.Contains("GiantHouseEntry"))
        {
            SceneManager.LoadScene("GameScene2");
        }
    }
}

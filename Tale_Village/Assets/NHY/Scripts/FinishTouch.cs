using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTouch : MonoBehaviour
{
    CharacterController cc;
    private void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

   
    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag.Contains("finish") && Duck.Instance.duckTouch == true)
            UIText.Instance.UITEXT = "GameClear";
    }
}

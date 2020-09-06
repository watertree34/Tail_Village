using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Spider : MonoBehaviour
{

    public float speed = 6.0f;
    public Transform[] bugTarget;
    Vector3 dir;
    int i = 0;
    bool attacked;
    float gravity = -9.8f;

    CharacterController controller;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (attacked)
        {
            controller.Move(Vector3.up * gravity * Time.deltaTime); // 떨어짐

        }
        else
        {
            //벌레 패트롤 하기

            dir = bugTarget[i].position - transform.position;
            if (dir.magnitude < 2)
            {
                if (i >= bugTarget.Length - 1)
                    i = 0;
                else
                    i++;
            }
            dir.Normalize();
            controller.Move(dir * speed * Time.deltaTime);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        //벌레 공격 당함
        if (other.gameObject.tag.Contains("tool"))
        {
            attacked = true;
            Destroy(gameObject, 3);
        }
        //if (other.gameObject.tag.Contains("Player"))   ==> RaycastFind에서 마우스에 닿으면 안되는걸로
        //{

        //    LifeManager.Instance.LIFE -= 1; //플레이어 라이프 감소
        //}
    }
}
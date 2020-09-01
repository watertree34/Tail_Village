using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    //벌레 패트롤 하기
    public float speed = 6.0F;
    public Transform[] bugTarget;
    Vector3 dir;
    int i = 0;
    CharacterController controller;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {

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

    void OnControllerColliderHit(ControllerColliderHit other)
    {
    //벌레 공격 당함
        if (other.gameObject.name.Contains("axe"))
        {

            Destroy(gameObject, 3);
        }
    }
}
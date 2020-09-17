﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    CharacterController cc;
    Animator anim;
    public RaycastFind playerRay;
    Vector3 cheeseDir;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerRay.mouseGo)
        {
            anim.SetTrigger("Walk");
            cheeseDir = (playerRay.cheeseObj.transform.position - transform.position);
            cc.Move(cheeseDir * 1 * Time.deltaTime);
            transform.forward = cheeseDir;

            if(cheeseDir.magnitude<=2)
            {
                cc.Move(cheeseDir*0*Time.deltaTime);
            }
        }
    }

    


}

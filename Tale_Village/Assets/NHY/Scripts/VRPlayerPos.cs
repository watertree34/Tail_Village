﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class VRPlayerPos : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;

    public bool grab=false;

    public static VRPlayerPos Instance;
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
    pcPlayerMove moveScript;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<pcPlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grab)
        {
            moveScript.enabled = false;
            GetComponent<CharacterController>().enabled = false;
            //transform.position = new Vector3(leftHand.position.x , leftHand.position.y , transform.position.z);
        }
        else
        {
            //moveScript.enabled = true;

        }
    }

    public void MoveTargetPoint(Vector3 targetPos)
    {
        

        transform.position = targetPos;
    }
}
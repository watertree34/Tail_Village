using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCam : MonoBehaviour
{
    public Transform butterflySpot;
    void Update()
    {
        transform.position = butterflySpot.position;
        transform.LookAt(Camera.main.transform); //메인카메라를 항상 바라보게 한다 
    }
}

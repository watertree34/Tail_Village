using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

//콩-도끼에 닿으면 콩나무에서 떨어지면서 밑으로 떨어진다 
public class Bean : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            rb.useGravity = true;
            transform.parent = null;
        }
    }
   
}

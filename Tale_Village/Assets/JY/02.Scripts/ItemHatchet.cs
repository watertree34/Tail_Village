using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHatchet : MonoBehaviour
{
    //public GameObject Hatchet;
    public bool isHatchetActive = false;

    private void Start()
    {
        //Hatchet = GameObject.Find("Hatchet");
        isHatchetActive = false;
}

    public void OnClickButton()
    {
        isHatchetActive = !isHatchetActive;
        //Hatchet.SetActive(isHatchetActive);
    }
}

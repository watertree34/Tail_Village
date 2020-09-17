using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject PcCam;
    public GameObject VrObj;

    void Start()
    {
#if UNITY_EDITOR                         //Unity 에디터 or Windows에선 PCcam 사용
        PcCam.SetActive(true);
        VrObj.SetActive(false);

#else                                    //아니면 VR 사용
        PcCam.SetActive(false);
        VrObj.SetActive(true);
#endif
    }

    void Update()
    {
#if UNITY_EDITOR                         //Unity 에디터 or Windows에선 PCcam 사용
        PcCam.SetActive(true);
        VrObj.SetActive(false);

#else                                    //아니면 VR 사용
        PcCam.SetActive(false);
        VrObj.SetActive(true);
#endif
    }
}

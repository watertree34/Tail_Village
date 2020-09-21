
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class VR_ray_PlayerPos : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;

    public bool grab = false;

    public static VR_ray_PlayerPos Instance;
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
    OVRPlayerController moveScript;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<OVRPlayerController>();
        moveScript.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (grab)
        {
            moveScript.enabled = false;
            //GetComponent<CharacterController>().enabled = false;
            //transform.position = new Vector3(leftHand.position.x , leftHand.position.y , transform.position.z);
        }
        else
        {
            moveScript.enabled = true;

        }
    }

    public void MoveTargetPoint(Vector3 targetPos)
    {

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 6);   // 타겟으로 러프이동
       
    }
}

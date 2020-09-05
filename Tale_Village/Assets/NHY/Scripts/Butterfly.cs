using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
[RequireComponent (typeof(CharacterController))]
public class Butterfly : MonoBehaviour
{
    public static Butterfly Instance;
    private void Awake()
    {
        Instance = this;
    }
    public bool look;
    public float speed = 1.0f;
    public Transform[] bugTarget;
    Vector3 dir;
    int i = 0;
    public GameObject guideText;
    CharacterController controller;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        //나비 왔다갔다

        dir = bugTarget[i].position - transform.position;
        if (dir.magnitude < 0.5f)
        {
            if (i >= bugTarget.Length - 1)
                i = 0;
            else
                i++;
        }
        dir.Normalize();
        controller.Move(dir * speed * Time.deltaTime);


        //플레이어 레이에서 받아온 글자
        if (look)
            guideText.SetActive(true);
        else
            guideText.SetActive(false);

    }

   
}
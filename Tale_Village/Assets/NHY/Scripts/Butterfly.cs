using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
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
    public TextMesh guideText;
    CharacterController controller;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        guideText.text = "";
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
        {

            if (SceneManager.GetActiveScene().name == "GameScene1") //씬이름으로 현재씬 찾기
            {
                guideText.text = "콩나무를 타고 거인의 집에 가야해!";
            }
            if (SceneManager.GetActiveScene().name == "GameScene2") //씬이름으로 현재씬 찾기
            {
                guideText.text = "쉿! 자고있는 거인을 피해서 \n거위를 구해오자!";
            }

        }



    }
}
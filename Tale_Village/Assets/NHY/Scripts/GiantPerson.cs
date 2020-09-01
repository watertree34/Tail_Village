using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.WSA.Input;

// 거인이 움직이는 스크립트

//거인은 거위에 플레이어가 닿기 전까지 잠을 자고있다
//거인은 플레이어를 따라다니고 일정 거리에 다다르면 공격 시행
//만약 플레이어가 거인에게 공격을 시행하면 움직임에 일정시간 딜레이 발생
//거인의 집에서 빠져나와 올라왔던 콩나무에 다다르면 게임 클리어 씬으로 이동

public class GiantPerson : MonoBehaviour
{
    enum GinatPersonState
    {
        Sleep,
        Follow,
        Delay,
        Attack
    }

    GinatPersonState nowGiantState;

    NavMeshAgent agent;   //길찾기 ai
    Animator anim;        //애니메이션
    CharacterController cc; //캐릭터컨트롤러

    public Transform playerTransform;
    Vector3 peDir; //플레이어와 에너미 사이 방향
    float peDis;  // 플레이어와 에너미 사이 거리
    public float delayTime = 8;   //플레이어에게 공격받을시 딜레이 타임
    float tempDT;
    public float attackDis = 5;       //플레이어 어택할 기준 거리
    public float moveSpeed = 8;

    public bool duckTouch;  // 플레이어가 거위를 만졌을때 true가 되게할것
    public bool playerAttack; // 플레이어가 공격을 했을 때 true가 되게 할것



    void Start()
    {
        nowGiantState = GinatPersonState.Sleep;  //적의 상태 처음에 sleep
        agent = GetComponent<NavMeshAgent>();  //길찾기 ai
        agent.speed = moveSpeed;
        agent.enabled = false; // move상태에서만 켜주기 위해 기본으로 꺼준다
        anim = GetComponent<Animator>();  //애니메이션
        cc = GetComponent<CharacterController>(); // 캐릭터 컨트롤러
        tempDT = delayTime;

    }

    // Update is called once per frame
    void Update()
    {
        peDir = transform.position - playerTransform.position;  // 플레이어와 거인 사이 방향
        peDis = peDir.magnitude; //플레이어와 거인 사이 거리

        switch (nowGiantState)
        {
            case GinatPersonState.Sleep:
                Sleep();
                break;
            case GinatPersonState.Follow:
                Follow();
                break;
            case GinatPersonState.Delay:
                Delay();
                break;
            case GinatPersonState.Attack:
                Attack();
                break;

        }

        Debug.Log(nowGiantState);

    }


    private void Sleep()
    {
        //자는 애니메이션 추가할것

        if (peDis < 7 || duckTouch)  // 만약 플레이어가 거위에 닿거나 플레이어와의 거리가 1미터 이하면 follow로 상태전환
        {
            //일어나는 상태에서는 일어나는 애니메이션 재생할것 / has exit time으로 follow 로 넘어감
            nowGiantState = GinatPersonState.Follow;
        }

    }


    //follow 상태에서는 플레이어를 따라간다 이때, 플레이어에게 공격을 당하면 delay로 넘어감/ 플레이어가 attackDis 안에 있으면 Attack으로 넘어감
    private void Follow()
    {
        //걷는 애니메이션 추가할것       

        if (agent.enabled == false)
            agent.enabled = true;

        //플레이어 따라가며 길찾기 수행
        agent.destination = playerTransform.position;  //길찾기를 이용해 이동

        if (playerAttack)
            nowGiantState = GinatPersonState.Delay;
        if (peDis <= attackDis)   //attackDis보다 플레이어 사이 거리가 적어지면 어택으로 상태전환
            nowGiantState = GinatPersonState.Attack;


    }

    private void Delay()
    {

        //헤롱헤롱 애니메이션 추가할것, 애니메이션 재생시간이랑 delayTime같게 할것
        agent.destination = transform.position; // 딜레이에선 나자신이 목표

        delayTime -= Time.deltaTime;

        if (delayTime >= (tempDT - 0.2f))  //뒤로 살짝 밀려나기
        {
            cc.Move(-transform.forward * 50 * Time.deltaTime);
        }
        else
            cc.Move(transform.forward * 0);

        if (delayTime <= 0)      //딜레이타임이 0보다 작아지면 follow상태로 전환
        {
            nowGiantState = GinatPersonState.Follow;
            delayTime = tempDT; //딜레이타임 다시 초기화
            playerAttack = false;
        }

    }


    private void Attack()
    {
        //어택 애니메이션 실행


        if (peDis > attackDis)
            nowGiantState = GinatPersonState.Follow;

        if (playerAttack)
            nowGiantState = GinatPersonState.Delay;

    }

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        //벌레 공격 당함
        if (other.gameObject.name.Contains("axe"))// 플레이어가 어택하는것은 도끼가 몬스터에 닿는것과 같음
        {
            playerAttack = true;
           
        }
        if (other.gameObject.name.Contains("Player"))
        {

            //플레이어 라이프 감소
        }
    }
   
}

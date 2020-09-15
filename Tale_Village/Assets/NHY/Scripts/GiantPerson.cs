using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 거인이 움직이는 스크립트

//거인은 거위에 플레이어가 닿기 전까지 잠을 자고있다
//거인은 플레이어를 따라다니고 일정 거리에 다다르면 공격 시행
//만약 플레이어가 거인에게 공격을 시행하면 움직임에 일정시간 딜레이 발생
//거인의 집에서 빠져나와 올라왔던 콩나무에 다다르면 게임 클리어 씬으로 이동

public class GiantPerson : MonoBehaviour  //ItemManager상속
{


    enum GinatPersonState
    {
        Sleep,
        WakeUp,
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
    public float delayTime = 3;   //플레이어에게 공격받을시 딜레이 타임
    float tempDT;
    public float attackDis = 6;       //플레이어 어택할 기준 거리
    public float moveSpeed = 8;
    
    float awakeTime = 2;
   
    public float axeAttackkDis = 7; // 플레이어가 공격하는 기준거리
    public Transform axe;
    Vector3 aeDir; //도끼와 거인 사이 방향
    float aeDis;  // 도끼와 거인 사이 거리

    public Transform giantHand;  //거인 손위치
    public Transform giantFoot;  //거인 발위치
    float fpDis;  //거인 발이랑 플레이어 사이 거리

    void Start()
    {
        nowGiantState = GinatPersonState.Sleep;  //적의 상태 처음에 sleep
        agent = GetComponent<NavMeshAgent>();  //길찾기 ai
        agent.speed = moveSpeed;
        agent.enabled = false; // move상태에서만 켜주기 위해 기본으로 꺼준다
        anim = GetComponentInChildren<Animator>();  //애니메이션
        cc = GetComponent<CharacterController>(); // 캐릭터 컨트롤러

        tempDT = delayTime;

    }

    // Update is called once per frame
    void Update()
    {
        peDir = transform.position - playerTransform.position;  // 플레이어와 거인 사이 방향
        peDis = peDir.magnitude; //플레이어와 거인 사이 거리

        aeDir = transform.position - axe.position;//도끼와 거인 사이 방향
        aeDis = aeDir.magnitude;    // 도끼와 거인 사이 거리


        switch (nowGiantState)
        {
            case GinatPersonState.Sleep:
                Sleep();
                break;
            case GinatPersonState.WakeUp:
                Wakeup();
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


    }

    
    private void Sleep()
    {

        if (peDis < 7 || Duck.Instance.openCage == true)  // 만약 플레이어가 거위에 닿거나 플레이어와의 거리가 1미터 이하면 WakeUp 상태전환
        {
           anim.SetTrigger("WakeUp"); //깨는 애니메이션
            nowGiantState = GinatPersonState.WakeUp;
           
        }

    }
   
    void Wakeup()
    {
        awakeTime -= Time.deltaTime;
        if (awakeTime <= 0)
        {
            anim.SetBool("AwakeFollow", true);   //걷는 애니메이션
            nowGiantState = GinatPersonState.Follow; //follow 상태전환
        }
    }

    //follow 상태에서는 플레이어를 따라간다 이때, 플레이어에게 공격을 당하면 delay로 넘어감/ 플레이어가 attackDis 안에 있으면 Attack으로 넘어감
    private void Follow()
    {    

        if (agent.enabled == false)
            agent.enabled = true;

        //플레이어 따라가며 길찾기 수행
        agent.destination = playerTransform.position;  //길찾기를 이용해 이동

        if ((aeDis < axeAttackkDis)&&PickUp.Instance.isAxeUsed == true)//playerAttackDis보다 도끼 사이 거리가 적어지고 도끼가 인벤사용 되어있으면 딜레이로 상태전환
        {
            nowGiantState = GinatPersonState.Delay;  //헤롱헤롱 애니메이션
            anim.SetTrigger("Delay");
        }
        if (peDis <= attackDis)   //attackDis보다 플레이어 사이 거리가 적어지면 어택으로 상태전환
        {
            nowGiantState = GinatPersonState.Attack;
            anim.SetTrigger("Attack");//어택 애니메이션 실행
        }


    }

    private void Delay()
    {

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
            //따라가기 애니메이션
            anim.SetTrigger("Follow");
            delayTime = tempDT; //딜레이타임 다시 초기화
        }

    }
   
    private void Attack()
    {
        agent.destination = transform.position; // 어택위치에서 멈추기
        

        fpDis = (playerTransform.position - giantFoot.position).magnitude;
        if(fpDis<=5)
            LifeManager.Instance.LIFE -= 0.1f; //플레이어 라이프 감소


        //만약 라이프 다 닳으면 게임오버 애니메이션(들어올리기
        if (LifeManager.Instance.LIFE == 0)
        {
            anim.SetTrigger("GameOver");
            
            playerTransform.gameObject.GetComponent<Transform>().position = giantHand.position;
        }

        if (peDis > attackDis)   //플레이어가 어택 범위 벗어나면
        {
            nowGiantState = GinatPersonState.Follow;   //follow 상태전환
            //따라가기 애니메이션
            anim.SetTrigger("Follow");
        }
        if (aeDis < axeAttackkDis&& PickUp.Instance.isAxeUsed == true)   //도끼 사이 거리가 적어지고 인벤 사용 되어있으면
        {
            nowGiantState = GinatPersonState.Delay;   //딜레이로 상태전환
            //헤롱헤롱 애니메이션 추가할것, 애니메이션 재생시간이랑 delayTime같게 할것
            anim.SetTrigger("Delay");
        }

        
    }

}

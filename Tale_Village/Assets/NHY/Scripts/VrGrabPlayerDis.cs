using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//////////////////////
/*
[VR그랩 시스템]

(1) 그랩 포인트 감지하고 가장 가까운 그랩포인트 저장하기

1. 플레이어가 그랩 포인트에 가까이 다가간다
2. 그랩 포인트가 플레이어 손의 콜라이더에 닿으면 VrClimbingHand스크립트의 AddPoint에서 그랩포인트를 contactPoints로 추가한다
   이때, 그랩포인트 레이어가 아닌 다른 물체가 닿으면 아무일도 일어나지 않음
3. 2의 contactPoint가 추가된 상태에서 만약 그립버튼을 누르면, Grab함수 실행
4. VrClimbingHand스크립트의 Grab함수는 VrGrabPlayerDis스크립트의 손과 contactPoints사이의 거리를 측정해 가까운 오브젝트를  grabpoint에 저장
   contactPoints가 없으면 null, 있으면 값이 반환되어 grabpoint가 저장됨 <- grabpoint=가까운 contactPoints중 grabPoint레이어를 가진 오브젝트

(2) 그랩 포인트가 있을때 손 저장, 손 전/후 위치 바뀐 벡터로 플레이어 이동 제어

5. 만약 grabpoint가 있으면(null이 아니면), 플레이어의 오르는 기준 손을 셋팅 = climber.SetHand(this);
6. 5번을 통해 VRClimber스크립트의 currentHand가 5번에서 버튼을 누른 손으로 갱신됨=>(왼손 or 오른손 으로)
   VRClimber스크립트에서는 currentHand가 있을때, 
   currentHand(손)의 VrClimbingHand스크립트에서 fixedUpdate와 LateUpdate의 자신(손)위치값을 비교하여 
   currentHand(손)의 직전위치와 현재 위치가 얼마나 변했는지 벡터값으로 받아오고 <-beforeAfterDir
   플레이어는 손의 움직임과 반대방향(손을 밑으로하면 플레이어는 위로)으로 움직일것이므로 
   VRClimber스크립트에서 이동 스크립트를 끈 후 직전위치-현재 위치 한 값을 방향으로 하여 이동시킨다.

(3) 손 떼기
 7. 그립상태에서 플레이어가 그립버튼을 떼면 VRClimingHand스크립트의 NoGrab호출, NoGrab에서는 VRClimber의 ClearHand함수 호출
 8. currentHand를 null값으로 넣고 다시 이동할 수 있게 이동 스크립트 켜줌
    
     */
//////////////////////
public static class VrGrabPlayerDis
{
    //물체와 플레이어 손 사이의 거리를 계산하여 가까운 물체를 반환하는 함수
    public static GameObject GetNearest(Vector3 origin, List<GameObject> collection)
    {
        GameObject nearest = null;  //가까운 물체
        float minDistance = float.MaxValue;   // 기준값
        float distance = 0.0f;   // 거리

        foreach (GameObject entity in collection)
        {
            distance = (entity.gameObject.transform.position - origin).sqrMagnitude;  // 리스트에 들어있는 게임오브젝트와 origin사이의 거리
            if (distance < minDistance)    // 만약 거리가 최저거리보다 작으면
            {
                minDistance = distance;  // 최저거리 갱신하고
                nearest = entity;    // 리스트에서 가장 가까운거 nearest에 저장
            }
        }
        return nearest;
    }
}

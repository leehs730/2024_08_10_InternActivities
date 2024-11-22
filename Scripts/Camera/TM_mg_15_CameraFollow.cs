using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TM_mg_15_CameraFollow : MonoBehaviour
{
    public Transform player;    // 플레이어의 Transform을 참조합니다
    public float yOffset = 0f;  // 카메라의 y오프셋
    public float followSpeed = 4f;  // 카메라의 이동 속도
    //public float triggerZoneY = -2f; // 카메라가 플레이어를 따라가기 시작하는 y 위치 기준

    private bool shouldFollow = false;  // 카메라가 플레이어를 따라가야 하는지 여부

    void Update()
    {
        // 플레이어의 현재 y 위치가 triggerZoneY를 넘으면 카메라가 따라가도록 설정
        if(player.position.y <= transform.position.y)
        {
            shouldFollow = true;
        }
        
        if(player.position.y > transform.position.y)
        {
            shouldFollow = false;
        }

        if (shouldFollow)
        {
            // 카메라의 목표 위치를 설정
            Vector3 targetPosition = new Vector3(transform.position.x, player.position.y + yOffset,transform.position.z);

            // 카메라를 목표 위치로 부드럽게 이동시킨다.
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}

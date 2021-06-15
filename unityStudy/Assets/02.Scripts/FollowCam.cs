using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target; //카메라가 추적할 대상
    public float moveDamping = 15f; //이동속도 계수
    public float rotateDamping = 5f; //회전속도 계수
    //러프함수
    //부드럽게 움직이기 위함
    public float distance = 5f; //추적 대상과의 거리
    public float height = 4f; // 추적 대상과의 높이
    public float targetOffset = 2f; //추적 좌표의 오프셋
    //캐릭터의 바닥이 아닌 정수리부분을 바라보게

    Transform tr; //움직이고자 할때

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    //콜백함수 - 호출을 따로하지 않아도 알아서 작동하는 함수
    //이벤트 트리거 등 여러가지 사용법이 있음.
    private void LateUpdate() //카메라 작동할때 주로 사용
    {
        var camPos = target.position - (target.forward * distance) + (target.up * height);
        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);
        tr.LookAt(target.position + (target.up * targetOffset)); //발바닥보는상태 + 오프셋만큼(위쪽으로)
    }
    private void OnDrawGizmos() //디버그용 함수
    {
        Gizmos.color = Color.green;

        //DrawWireSphere 위치 , 지름
        //선으로 이루어진 구형의 모양을 그림(씬뷰에만 표시됨, 디버그용)
        Gizmos.DrawWireSphere(target.position + (target.up * targetOffset), 0.1f);

        //메인카메라와 추적지점 사이에 선을 그림
        //DrawLine(출발지점, 도착지점)
        //출발과 도착지점 사이에 선을 그림
        Gizmos.DrawLine(target.position + (target.up * targetOffset), transform.position);

    }
}

//레거시 , 메카님 애니메이션

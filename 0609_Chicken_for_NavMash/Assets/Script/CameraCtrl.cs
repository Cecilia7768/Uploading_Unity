using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform target;//카메라 추적대상
    public float moveDamping = 15f;//이동속도 계수
    public float rotateDamping = 500f;//회전속도 계수

    public float distance = 10f; //추적대상과의 거리
    public float height = 3f; //추적대상과의 높이
    public float tagetOffset = 2f;//추적좌표의 오프셋
    //바닥말고 정수리를 보게

    Transform tr;
    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        var campos = target.position - (target.forward * distance) + (target.up * height);
        tr.position = Vector3.Slerp(tr.position, campos, Time.deltaTime * moveDamping);
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);
        tr.LookAt(target.position + (target.up * tagetOffset));
        //오프셋만큼 위쪽으로 보기
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public Color _color = Color.yellow;
    public float _radius = 0.1f; //기즈모의 사이즈 설정

    private void OnDrawGizmos()
    {
        //콜백함수
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
        //해당 위치에 _radius 크기만큼 기즈모를 그려라
        //DrawSpere이므로 구체모양으로 그림
       
    }
}

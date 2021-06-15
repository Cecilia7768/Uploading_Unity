using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
//클래스의 경우 변수와 달리 직렬화 해줘야(직접 알려줘야) 인스펙터에 표시됨
public class PlayerAnim //내부클래스
{
    //직렬화    
    public AnimationClip idle;
    public AnimationClip runF;
    public AnimationClip runB;
    public AnimationClip runL;
    public AnimationClip runR;
}
public class PlayerCtrl : MonoBehaviour
{
    private float h = 0f;//private 접근 지시자
    private float v = 0f;
    private float r;

    Transform tr; //트랜스폼 컴포넌트 접근 변수

    public float moveSpeed = 10f;
    public float rotSpeed = 150f; //회전계수
                                  //Animator animator;

    public PlayerAnim playerAnim;
    public Animation anim;


    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();
        anim.clip = playerAnim.idle; //혹시 모르니 첫시작시 플레이할 클립 알랴줌
        anim.Play();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");
        //print("H값 -" + h + " V값 -" + v);
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        moveDir = moveDir.normalized;

        //벡터 정규화
        //대각선 이동이 일반적인 이동보다 크기가 커서 빨리 움직임
        //이를 해결하고자 벡터의 크기를 일정하네 1로 정규화 시킴

        tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
        //포지션값을 변경할때 Translate 사용!
        //forward -> 기준벡터!        
        //Space.Self : 오브젝트 기준으로 방향을 잡아줌(글로벌과 로컬의 차이!)

        //print(Vector3.Magnitude(Vector3.forward + Vector3.right));
        //Magnitude : 벡터의 크기를 구함
        //print(Vector3.Magnitude((Vector3.forward + Vector3.right).normalized));       
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

        //애니메이션 동작 구분
        if (v >= 0.1f) //전진        
            anim.CrossFade(playerAnim.runF.name, 0.3f);
        //CrossFade(애니메이션 이름,전환 시간(페이드인페이드아웃시간))
        //재생하고자하는 애니메이션의 이름으로 실행
        else if (v < -0.1f) //후진        
            anim.CrossFade(playerAnim.runB.name, 0.3f);
        else if (h >= 0.1f)//오른쪽
            anim.CrossFade(playerAnim.runR.name, 0.3f);
        else if (h < -0.1f)//왼쪽
            anim.CrossFade(playerAnim.runL.name, 0.3f);
        else //아무 입력도 없을때
            anim.CrossFade(playerAnim.idle.name, 0.3f);

    }
}


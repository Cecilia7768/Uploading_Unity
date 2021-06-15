using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    const string bulletTag = "BULLET";

    float hp = 100f;//체력
    GameObject bloodEffect; //혈흔효과 변수

    private void Start()
    {
        //Load 함수는 예약 폴더인 Resources 에서 데이터를 불러오는 함수임
        //Load<데이터유형>("파일의 경로"); 최상위 경로는 Resources경로임 ex)C드라이브
        bloodEffect = Resources.Load<GameObject>("");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == bulletTag)
        {
            //혈흔효과 함수 호출
            ShowBloodEffect(collision);
            //총알 삭제
            Destroy(collision.gameObject);

            hp -= collision.gameObject.GetComponent<BulletCtrl>().damage;

            //체력이 0이하게 되면 적이 죽었다고 판단
            if(hp <= 0)
            {
                //상태 변환 해줌
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            }
        }
    }

    void ShowBloodEffect(Collision coll)
    {
        //충돌위치값 가져오기
        Vector3 pos = coll.contacts[0].point;
        //충돌위치의 법선벡터(총알이 날아온 방향) 구하기
        Vector3 _nomal = coll.contacts[0].normal;
        //총알이 날아온 방향쪽으로 회전 시키기
        Quaternion rot = Quaternion.FromToRotation(Vector3.back, _nomal);
        
        GameObject blood = Instantiate(bloodEffect,pos,rot);
        //혈흔효과 동적 생성
        Destroy(blood,1f);

    }

}

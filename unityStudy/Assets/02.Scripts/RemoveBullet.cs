using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{

    public GameObject sparkEffect; //스파크 프리팹
    private void OnCollisionEnter(Collision collision)
    {        
        //콜백함수.
        //충돌이 발생한 것들 중에서 불렛 태그만 검출
        if (collision.collider.tag == "BULLET")
        {
            //스파크 이펙트 함수 호출
            ShowEffect(collision);            

            //충돌이 발생한 오브젝트 삭제
            Destroy(collision.gameObject); //즉시삭제
            //Destroy(collision.gameObject,5f); //5초뒤에 삭제
            //collision.gameObject.SetActive(false); 이문장과 차이 체크
        }
    }

    void ShowEffect(Collision coll)
    {
        ContactPoint contact = coll.contacts[0];
        //ContactPoint /contacts[0] 충돌이 발생한 제일 최초의 [0] 부분을 가져옴
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward,contact.normal);
        //FromToRotation (회전시키고자 하는 벡터, 타켓벡터) : 해당 벡터를 돌려버림
        //vector.back으로 써도된당(아마둥?)
        //법선 벡터라고 한단당!


        Vector3 point = contact.point + (-contact.normal * 0.05f); 
        //드럼통 레이어보다 살짝 위로 총알자국이 보이게

        GameObject spark = Instantiate(sparkEffect,point, rot);
        //충돌후 이펙트의 효과방향을 법선벡터로 생성


        //동적생성 된 이펙트의 부모로 드럼통을 설정
        spark.transform.SetParent(this.transform);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    const string bulletTag = "BUULET";
    float iniHP = 100f; //초기 체력
    public float currHP; //현재 체력

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDieEvent;
    private void Start()
    {
        currHP = iniHP;
    }

    //충돌이 아니라 관통일 경우에 사용하는 함수
    private void OnTriggerEnter(Collider other)
    {
        //충돌한 물체의 태그 비교
        if(other.tag == bulletTag)
        {
            Destroy(other.gameObject);
            currHP -= 5;//체력 5 감소
            print("현재 체력 = " + currHP);
            if(currHP <=0f)
            {
                //플레이어 사망 함수 호출
                PlayerDie();
            }
        }
    }
    void PlayerDie()
    {
        //print("플레이어 사망");

        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");

        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    //직접호출
        //    //enemies[i].GetComponent<EnemyAI>().OnPlayerDie();
        //    //메세지 호출
        //    enemies[i].SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        //    //델리게이트 호출

        //}

        OnPlayerDieEvent();
    }
}

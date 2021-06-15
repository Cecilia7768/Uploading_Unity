using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    const string bulletTag = "BUULET";
    float iniHP = 100f; //�ʱ� ü��
    public float currHP; //���� ü��

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDieEvent;
    private void Start()
    {
        currHP = iniHP;
    }

    //�浹�� �ƴ϶� ������ ��쿡 ����ϴ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        //�浹�� ��ü�� �±� ��
        if(other.tag == bulletTag)
        {
            Destroy(other.gameObject);
            currHP -= 5;//ü�� 5 ����
            print("���� ü�� = " + currHP);
            if(currHP <=0f)
            {
                //�÷��̾� ��� �Լ� ȣ��
                PlayerDie();
            }
        }
    }
    void PlayerDie()
    {
        //print("�÷��̾� ���");

        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");

        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    //����ȣ��
        //    //enemies[i].GetComponent<EnemyAI>().OnPlayerDie();
        //    //�޼��� ȣ��
        //    enemies[i].SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        //    //��������Ʈ ȣ��

        //}

        OnPlayerDieEvent();
    }
}

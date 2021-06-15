using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    const string bulletTag = "BULLET";

    float hp = 100f;//ü��
    GameObject bloodEffect; //����ȿ�� ����

    private void Start()
    {
        //Load �Լ��� ���� ������ Resources ���� �����͸� �ҷ����� �Լ���
        //Load<����������>("������ ���"); �ֻ��� ��δ� Resources����� ex)C����̺�
        bloodEffect = Resources.Load<GameObject>("");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == bulletTag)
        {
            //����ȿ�� �Լ� ȣ��
            ShowBloodEffect(collision);
            //�Ѿ� ����
            Destroy(collision.gameObject);

            hp -= collision.gameObject.GetComponent<BulletCtrl>().damage;

            //ü���� 0���ϰ� �Ǹ� ���� �׾��ٰ� �Ǵ�
            if(hp <= 0)
            {
                //���� ��ȯ ����
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            }
        }
    }

    void ShowBloodEffect(Collision coll)
    {
        //�浹��ġ�� ��������
        Vector3 pos = coll.contacts[0].point;
        //�浹��ġ�� ��������(�Ѿ��� ���ƿ� ����) ���ϱ�
        Vector3 _nomal = coll.contacts[0].normal;
        //�Ѿ��� ���ƿ� ���������� ȸ�� ��Ű��
        Quaternion rot = Quaternion.FromToRotation(Vector3.back, _nomal);
        
        GameObject blood = Instantiate(bloodEffect,pos,rot);
        //����ȿ�� ���� ����
        Destroy(blood,1f);

    }

}

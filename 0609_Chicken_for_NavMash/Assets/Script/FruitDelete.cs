using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDelete : MonoBehaviour
{
    static Rigidbody rb;
    GameObject banana;
    private void Start()
    {
    
        rb = GetComponent<Rigidbody>();
    }

    //GameObject[] tmp = GameObject.FindGameObjectsWithTag("ENEMY");
    //private void OnTriggerEnter(Collider other)
    //{
    //    //if (other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Floor"))
    //    //    this.gameObject.SetActive(false); //�ڽ��� �ٴڿ� �ε����� �����

    //    //if (other.gameObject.CompareTag("Player")) //�÷��̾� ������Ʈ�� �ε����� ������ �Ǿ����
    //    if (other.tag == "Player")
    //    {
    //        this.gameObject.SetActive(false);
    //        //Destroy(this.gameObject);
    //    }
    //    rb.velocity = new Vector3(0, -0.003f, 0); //���ϼӵ� üũ

    //}

    //private void Update()
    //{
    //    rb.velocity = new Vector3(0, -0.003f, 0); //���ϼӵ� üũ
    //}



}

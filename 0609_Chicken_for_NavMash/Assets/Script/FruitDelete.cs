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
    //    //    this.gameObject.SetActive(false); //박스나 바닥에 부딪히면 사라짐

    //    //if (other.gameObject.CompareTag("Player")) //플레이어 오브젝트와 부딪히면 삭제가 되어야함
    //    if (other.tag == "Player")
    //    {
    //        this.gameObject.SetActive(false);
    //        //Destroy(this.gameObject);
    //    }
    //    rb.velocity = new Vector3(0, -0.003f, 0); //낙하속도 체크

    //}

    //private void Update()
    //{
    //    rb.velocity = new Vector3(0, -0.003f, 0); //낙하속도 체크
    //}



}

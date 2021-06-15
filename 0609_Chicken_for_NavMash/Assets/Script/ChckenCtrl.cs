using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]

//public class PlayerAnim
//{
//    public AnimationClip idle;
//    public AnimationClip run;
//}

//[RequireComponent(typeof(Rigidbody))] �����÷ο��ڵ�...���ʿ�?
public class ChckenCtrl : MonoBehaviour
{
    private float h = 0f;
    private float v = 0f;
    private float r;
    public float moveSpeed = 10f;
    public float rotateSpeed = 180f;
    public float rotSpeed = 180f;   

    public Vector3 jump;
    public float jumpForce = 2.0f;
    Rigidbody rb;


    GameObject player;

    Transform tr;
    private void Start()
    {     
        tr = GetComponent<Transform>();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //�߻��� ���� �� ���� �ڿ������� ���ϰ� �����ߵ�
        //������ ���������̿� ������������!!!

        //(1Ʈ)
        //target.transform = new Vector3(Input.mousePosition.x,0,Input.mousePosition.y);
        //transform.LookAt(target.transform);
        //���̰͵� �ȵ�~ ^^

        //(2Ʈ)
        //this.rotation = new Vector3(0,h * rotateSpeed *Time.deltaTime,0);
        //Vector3 move = new Vector3(0, 0, v * Time.deltaTime);
        //move = this.transform.TransformDirection(move);
        //this.transform.Rotate(this.rotation);
        //�ȵ���?

        //(3Ʈ)
        //tr.Rotate(Vector3.up * moveSpeed * Time.deltaTime);
        //�̰��ϴϱ� ���ڸ����� ��۹�� ���� ��.��    

        //(4Ʈ)
        //var mousePos = Input.mousePosition;
        //var wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        //transform.LookAt(wantedPos);
        //�δ㽺���� �� �ٶ󺸸鼭 �յڷ� ������..^^

        //(5Ʈ)
        //�밡���� �� �ߵ����� ��ġ �� ����ó��
        //Vector3 mPosition = Input.mousePosition;
        //Vector3 oPosition = transform.position;
        //mPosition.z = oPosition.z - Camera.main.transform.position.z;
        //Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
        //float dy = target.y - oPosition.y;
        //float dx = target.x - oPosition.x;
        //float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, rotateDegree, 0f);

        //(6Ʈ) //�� ���� �ڵ�
        //�Ȱǰ�...�� �����ȵ�������� ī�޶� �����ϸ� ����������
        r = Input.GetAxis("Mouse X");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir = moveDir.normalized;
        tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

        //�׼� & ���⺰ �ִϸ��̼� ���� �ʿ�
        if (Input.GetKey(KeyCode.A))       
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        // anim.CrossFade(playerAnim.run.name, 0.3f);    
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        //������ �ϰ����!!!

        //����� 1) �����
        //if (Input.GetKeyDown(KeyCode.Space))
        //    rb.AddForce(jump * jumpForce, ForceMode.Force);

        //����� 2)
        //floor �հ� �������� ���� -> �ݶ��̴� ��ġ���� & Ű�׸�ƽ���� + x,z �����̼� ����
        if (Input.GetKeyDown("space"))
            transform.Translate(Vector3.up * 130 * Time.deltaTime, Space.World);


        Vector3.RotateTowards(transform.forward, moveDir, moveSpeed * Time.deltaTime, 0f);
    }
    //private void FixedUpdate()
    //{
    // if(Input.GetKeyDown(Space))
    //    {
    //        rb.AddForce(transform.up * 20f);
    //    }
    //}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]

//public class PlayerAnim
//{
//    public AnimationClip idle;
//    public AnimationClip run;
//}

//[RequireComponent(typeof(Rigidbody))] 오버플로우코드...왜필요?
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

        //닭새기 각기 턴 말고 자연스럽게 턴하게 만들어야됨
        //구글의 집단지성이여 나에게힘을죠!!!

        //(1트)
        //target.transform = new Vector3(Input.mousePosition.x,0,Input.mousePosition.y);
        //transform.LookAt(target.transform);
        //응이것도 안돼~ ^^

        //(2트)
        //this.rotation = new Vector3(0,h * rotateSpeed *Time.deltaTime,0);
        //Vector3 move = new Vector3(0, 0, v * Time.deltaTime);
        //move = this.transform.TransformDirection(move);
        //this.transform.Rotate(this.rotation);
        //안되쥬?

        //(3트)
        //tr.Rotate(Vector3.up * moveSpeed * Time.deltaTime);
        //이걸하니까 제자리에서 뱅글뱅글 돈당 ㅠ.ㅠ    

        //(4트)
        //var mousePos = Input.mousePosition;
        //var wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        //transform.LookAt(wantedPos);
        //부담스럽게 날 바라보면서 앞뒤로 움직임..^^

        //(5트)
        //대가리가 왜 삐딱하지 마치 내 마음처럼
        //Vector3 mPosition = Input.mousePosition;
        //Vector3 oPosition = transform.position;
        //mPosition.z = oPosition.z - Camera.main.transform.position.z;
        //Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
        //float dy = target.y - oPosition.y;
        //float dx = target.x - oPosition.x;
        //float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, rotateDegree, 0f);

        //(6트) //쌤 수업 코드
        //된건가...쫌 맘에안들긴하지만 카메라 설정하면 나아지려나
        r = Input.GetAxis("Mouse X");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir = moveDir.normalized;
        tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

        //액션 & 방향별 애니메이션 설정 필요
        if (Input.GetKey(KeyCode.A))       
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        // anim.CrossFade(playerAnim.run.name, 0.3f);    
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        //점프가 하고십펑!!!

        //츄라이 1) 노반응
        //if (Input.GetKeyDown(KeyCode.Space))
        //    rb.AddForce(jump * jumpForce, ForceMode.Force);

        //츄라이 2)
        //floor 뚫고 내려가는 문제 -> 콜라이더 위치조정 & 키네마틱해제 + x,z 로테이션 고정
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
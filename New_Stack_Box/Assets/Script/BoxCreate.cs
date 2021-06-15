using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreate : MonoBehaviour
{
    static float add_for_Height = 1f;

    public GameObject Box1;
    public GameObject Box2;
    public GameObject Box3;

    GameObject tmp;
    TimerUI about_UI;

    //public Mesh[] meshes;
    //MeshFilter meshFilter;

//    Renderer Box_color;

    float speed = 1.0f;
    float add_speed = 0.1f;

    static List<GameObject> BoxList = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(Coru_Box_Maker());
        //Box_color = gameObject.GetComponent<Renderer>();
        //meshFilter = GetComponent<MeshFilter>();
    }

    IEnumerator Coru_Box_Maker() //박스 열개미만이면 지속생성
    {
        while(true)
        {
            if(BoxList.Count<15 && TimerUI.timelimit > 0f)//박스가 열개 미만이라면
            {
                //생성함수
                Box_Creator();
            }
            yield return new WaitForSeconds(speed);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Box_Destroy(0);
            add_speed *= 2;
            speed -= add_speed;         
        }
        else  if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Box_Destroy(1);
            add_speed *= 2;
            speed -= add_speed;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Box_Destroy(2);
            add_speed *= 2;
            speed -= add_speed;     
        }            
    }
    void Box_Creator()
    {
        GameObject b_obj;
        GameObject tmp_obj;
        int i = Random.Range(0, 3);
        if (i == 0)
            tmp_obj = Box1;
        else if (i == 1)
            tmp_obj = Box2;
        else
            tmp_obj = Box3;

        b_obj = Instantiate(tmp_obj) as GameObject;
        b_obj.transform.position = new Vector3(tmp_obj.transform.position.x, (tmp_obj.transform.position.y) * add_for_Height,tmp_obj.transform.position.z);

        //Box_color.material.color = Color.red; 컬러 랜덤뽑기
        BoxList.Add(b_obj);

        b_obj.SetActive(true);
        add_for_Height += .5f;
        if (add_for_Height > 5) //너무 하늘뿌셔 지구뿌셔 높이에서 떨어지면
            add_for_Height = 2.5f; //알잘딱 높이조절
    }

    void Box_Destroy(int choice_case)
    {   
        //배열을 찾았을때 생성된 클론의 이름이 일치하다면 그걸 삭제해야함
        //ex) 0값이 들어오면 -> list중에 Box (clone) 인것중에 첫번째인것을 찾아야함
        for (int i = 0; i < BoxList.Count; i++) 
        {
            if (choice_case == 0)
            {
                if (BoxList[i].activeSelf && BoxList[i].name == "Tir_Box(Clone)") //마지막 리스트가 활성화되어있다면
                {
                    BoxList[i].SetActive(false); //비활성화 해주고
                    Destroy(BoxList[i]);
                    BoxList.RemoveAt(i);               
                    break; //빠져나오기
                }
            }
            else if (choice_case == 1)
            {
                if (BoxList[i].activeSelf && BoxList[i].name == "Box(Clone)") //마지막 리스트가 활성화되어있다면
                {
                    BoxList[i].SetActive(false); //비활성화 해주고
                    Destroy(BoxList[i]);
                    BoxList.RemoveAt(i);                 
                    break; //빠져나오기
                }
            }
            else
            {
                if (BoxList[i].activeSelf && BoxList[i].name == "Sec_Box(Clone)") //마지막 리스트가 활성화되어있다면
                {
                    BoxList[i].SetActive(false); //비활성화 해주고
                    Destroy(BoxList[i]);
                    BoxList.RemoveAt(i);                
                    break; //빠져나오기
                }
            } 
                 
        }
        TimerUI.score++;
    }

    //void Makeing_Color_Box() //랜덤메시
    //{
    //    int idx = Random.Range(0, meshes.Length);
    //    meshFilter.sharedMesh = meshes[idx];
    //}

    //bool Game_Clear() //프리팹의 첫번째 위치 (바닥)에 아무것도 없으면 게임 클리어
    //{
    //    if (transform.position == null) //바닥에 아무것도 없으면       
    //        return true;
    //    else
    //        return false;
    //}
}

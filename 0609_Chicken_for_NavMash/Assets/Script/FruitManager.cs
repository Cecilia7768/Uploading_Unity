using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public static GameObject banana;
    public static GameObject cheese;
    public static GameObject olive;
    public static GameObject hamburger;
    public GameObject Banana;
    public GameObject Cheese;
    public GameObject Olive;
    public GameObject Hamburger;

    static List<GameObject> Fruit_List = new List<GameObject>();

    float m_Speed;
    private void Start()
    {
        banana = Banana;
        cheese = Cheese;
        olive = Olive;
        hamburger = Hamburger;

         StartCoroutine(Co_Create_Fruits());
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        this.gameObject.SetActive(false);
    //        Destroy(this.gameObject);
    //    }
    //    //if(other.gameObject.CompareTag("Floor")) //���ڲ� ��Ű��??
    //    //{
    //    //    other.gameObject.SetActive(false);
    //    //    Destroy(other.gameObject);
    //    //}
    //}
    //private void Update()
    //{
    //    //Destroy_Fruits(); //�ڷ�ƾ�̶� ��Ŵ
    //}
    IEnumerator Co_Create_Fruits()
    {
        while(true)
        {
            if (Fruit_List.Count < 5)            
                Set_XY(Making_Fruit());
            yield return new WaitForSeconds(.5f);
        }      
    }     

    public static void Re_Using_Fruit() //������ ����������� ������ ����Ʈ���� �ٽ� ������
    {
        GameObject F_obj = null;
        for(int i =0;i<Fruit_List.Count;i++)
        {
            if(!Fruit_List[i].activeSelf) //��Ȱ��ȭ�� ������ ������
            {
                F_obj = Fruit_List[i];
                Set_XY(Making_Fruit());
                F_obj.SetActive(true);
            }
        }
    }

    static GameObject Making_Fruit() //���� ���� �̴� �Լ�
    {
        GameObject item;
        GameObject tmp_Fruit;
        int what_Fruit = Random.Range(0, 4);
        if (what_Fruit == 0)
            tmp_Fruit = banana;
        else if (what_Fruit == 1)
            tmp_Fruit = cheese;
        else if (what_Fruit == 2)
            tmp_Fruit = hamburger;
        else
            tmp_Fruit = olive;

        item = Instantiate(tmp_Fruit) as GameObject;

        return item;
    }  
    private static void Set_XY(GameObject obj) //���� ������ ������ǥ�̱�
    {
        
        float x = Random.Range(-25, 25);
        float z = Random.Range(-25, 25);
        obj.transform.position = new Vector3(x, 15, z);
        if(Fruit_List.Count <= 5)
            Fruit_List.Add(obj);    
        obj.SetActive(true);
    }
}

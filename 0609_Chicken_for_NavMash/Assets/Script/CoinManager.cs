using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{    
    public GameObject _coin;
    public static GameObject coin;

    static List<GameObject> Coin_List = new List<GameObject>();

    private void Start()
    {
        coin = _coin;
        StartCoroutine(Co_Create_Coin());
    }
    private void OnTriggerEnter(Collider other)
    {
        int randEffect;
        randEffect = Random.Range(0, 5);
        if(randEffect == 1)
        {
            //�ӵ� �ڷ�ƾ
        }
    }
    IEnumerator Co_Create_Coin()
    {
        while(true)
        {
            if (Coin_List.Count < 5)
                //��ǥ����
                Set_Coin_XY(Making_Coin());
            yield return new WaitForSeconds(2f);
                
        }
    }

    static GameObject Making_Coin()
    {
        GameObject item;

        item = Instantiate(coin) as GameObject;

        return item;
    }

    static void Set_Coin_XY(GameObject obj) //���� ���� ��ǥ����
    {
        float x = Random.Range(-30, 30);
        float y = Random.Range(-30, 30);
        obj.transform.position = new Vector3(x, 1.4f, y);
        if (Coin_List.Count <= 5) //�ټ��������� �����
            Coin_List.Add(obj);
        obj.SetActive(true);
    }

    static void Re_Making_Coin()
    {
        GameObject C_obj = null;
        for(int i = 0;i <Coin_List.Count; i++)
        {
            if(!Coin_List[i].activeSelf) //��Ȱ���� ������ �̝���
            {
                C_obj = Coin_List[i];
                Set_Coin_XY(Making_Coin());
                C_obj.SetActive(true);
            }               
        }
    }
    
}

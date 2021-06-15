using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //enum FOOD
    //{
    //    Hamburger,
    //    Cheese,
    //    Hotdog
    //}
    //FOOD food = FOOD.Hamburger;

    public GameObject hamburgerPrefab;
    public GameObject cheesePrefab;
    public GameObject hotdogPrefab;
    float span = 1.0f;//한개씩 프리팹 생성
    float delta = 0f;
    float speed = -0.03f;

    //foreach(int num in E...Enum E가 안뜬다..ㅠㅠ
    private void Start()
    {
        //hamburgerPrefab = Resources.Load<GameObject>("Hamburger");
        //food = (FOOD)Random.Range(0, Enum.GetValues)
        //tmp_Ham = 
        hamburgerPrefab = GameObject.Find("Hamburger");
        cheesePrefab = GameObject.Find("Cheese");
        hotdogPrefab = GameObject.Find("Hotdog");
    }
    public void SetParameter(float span, float speed, int ratio)
    {
        this.span = span;
        this.speed = speed;
    }
    private void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span) //3초마다 생성
        {
            this.delta = 0;
            GameObject item;
            int range = Random.Range(0, 3);    //어떤 음식이 떨어질지에 대한 확률
                                               //int random = UnityEngine.Random.Range(0,)
                                               //if (food == FOOD.Hamburger)
            if (range == 1)
            {
                item = Instantiate(hamburgerPrefab) as GameObject;
                item.transform.position = hamburgerPrefab.transform.position;
            }
            else if (range == 2)//치즈차례
            {
                item = Instantiate(cheesePrefab) as GameObject;
                item.transform.position = cheesePrefab.transform.position;
            }
            else
            {
                item = Instantiate(hotdogPrefab) as GameObject;
                item.transform.position = hotdogPrefab.transform.position;
            }

            //item.GetComponent<>


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    FoodManager Food;
    Rigidbody rb;
    public GameObject gb;
    float delta;

    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    void Start()
    {
        Food = GetComponent<FoodManager>();
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Spawn", spawnTime, spawnTime);        
    }

    void Spawn()
    {
        this.delta += Time.deltaTime;
        if(this.delta > this.spawnTime)
        {

            this.delta = 0;
            GameObject item;
            int range = Random.Range(0, 3);    //어떤 음식이 떨어질지에 대한 확률
            //int random = UnityEngine.Random.Range(0,)
            //if (food == FOOD.Hamburger)
            if (range == 1)
            {
                item = Instantiate(Food.hamburgerPrefab) as GameObject;
                item.transform.position = Food.hamburgerPrefab.transform.position;
            }
            else if (range == 2)//치즈차례
            {
                item = Instantiate(Food.cheesePrefab) as GameObject;
                item.transform.position = Food.cheesePrefab.transform.position;
            }
            else
            {
                item = Instantiate(Food.hotdogPrefab) as GameObject;
                item.transform.position = Food.hotdogPrefab.transform.position;
            }

        }
    }
}
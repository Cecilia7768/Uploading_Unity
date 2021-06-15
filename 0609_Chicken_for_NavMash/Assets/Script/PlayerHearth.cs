using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHearth : MonoBehaviour
{
    float startHeart;
    float currHeart;
    FruitManager f_m;

    private void Start()
    {
        f_m.b
        startHeart = 100f; //시작할때 체력
        currHeart = startHeart;
    }

}

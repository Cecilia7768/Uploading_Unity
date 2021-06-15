using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public float timelimit;

    public Text timer;
    private void Start()
    {
        timelimit = 50f;
    }

    private void Update()
    {
        if (timelimit > 0.1f) //계수체크
            timelimit -= Time.deltaTime;
        timer.text = string.Format("{0 :N2}", timelimit);
    }

}

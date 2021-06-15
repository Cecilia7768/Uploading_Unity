using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public static float timelimit;
    public static int score; //싱글턴에 포함
    
    public Text timer;
    public Text scoreText;
    public Text winText;
    private void Start()
    {
        //GameManager.Instance.score = 0;
        timelimit = 30f;
        StartCoroutine(Checking_score());
    }
    IEnumerator Checking_score()
    {
        while(true)
        {
            if (score > 10)
            {
                winText.text = "You Win !";
                Application.Quit();
                yield break;
            }
            else if (timelimit <= 0f)
            {
                winText.text = "You Lose !";
                Application.Quit();
                yield break;
            }
            else
                yield return new WaitForSeconds(1f);

        }
    }
    private void Update()
    {
        if (timelimit > 0f) //한번씩 -0.01f까지 프린팅 됨
            timelimit -= Time.deltaTime;    
        timer.text = string.Format("{0:N2}", timelimit);
        SetScoretext();
    } 
    void SetScoretext()
    {       
        winText.text = "";
        scoreText.text = "Score : " + score.ToString();   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int score;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                var obj = FindObjectOfType<GameManager>();
                if (!obj)
                    _instance = obj;
                else
                {
                    var newobj = new GameObject().AddComponent<GameManager>();
                    _instance = newobj;
                }
            }
            return _instance;
        }
     }
    private void Awake()
    {
        var objs = FindObjectOfType<GameManager>();
        if (objs)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

}
 

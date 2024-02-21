using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public int curScore { get; set; } = 0;
   
    void Awake()
    {
        if (null == instance){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public static GameManager Instance
    {
        get{
            if (null == instance)
                return null;
            return instance;
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    //Debug.Log(curScore);
    //}

    public void GameOver()
    {
        //Debug.Log("GameOver");
    }
}

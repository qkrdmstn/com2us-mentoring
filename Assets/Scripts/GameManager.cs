using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    //Scene info
    public string curSceneName;

    //Stage info
    public int curScore { get; set; } = 0;
    public float playTime = 0;
    public bool isStage = false;
    public bool isGameOver = false;

    void Awake()
    {
        curSceneName = SceneManager.GetActiveScene().name;
        SoundManager.Instance.OnSceneLoaded(curSceneName);

        if (null == instance){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    private void Update()
    {
        if(isStage)
        {
            playTime += Time.deltaTime;
        }
    }

    public static GameManager Instance
    {
        get{
            if (null == instance)
                return null;
            return instance;
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        curSceneName = sceneName;
        SoundManager.Instance.OnSceneLoaded(sceneName);
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}

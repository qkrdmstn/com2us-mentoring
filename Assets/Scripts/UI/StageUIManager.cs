using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject pauseMenuUI;
    public GameObject GameOverUI;


    private void Update()
    {

    }

    #region Scene
    public void LoadMainScene()
    {
        GameManager.Instance.ChangeScene("Main");
    }

    public void RestartStage()
    {
        GameManager.Instance.ChangeScene("Stage");
    }
    #endregion

    #region Pause
    public void PauseOn()
    {
        Time.timeScale = 0.0f;
    }

    public void PauseOff()
    {
        Time.timeScale = 1.0f;

    }
    #endregion

    #region GameOver
    public void SetActiveGameOverUI()
    { 
        GameOverUI.SetActive(true);
    }
    #endregion

    #region Sound
    public void PlayClickSound()
    {
        SoundManager.Instance.SetEffectSound("Click");
    }
    #endregion
}

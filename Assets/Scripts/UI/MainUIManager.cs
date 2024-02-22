using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public void LoadStageScene()
    {
        GameManager.Instance.ChangeScene("Stage");
    }

    public void PlayClickSound()
    {
        SoundManager.Instance.SetEffectSound("Click");
    }

}

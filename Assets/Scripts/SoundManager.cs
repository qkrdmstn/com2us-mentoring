using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public AudioSource[] audioSources; //0: BGM, 1: Effect
    public AudioClip[] BGMClips;
    public AudioClip[] effectClips;
    
    public float _bgmVolume = 0.6f;
    public float _effectVolume = 0.6f;
    //Singleton
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
                return null;
            return instance;
        }
    }

    public void OnSceneLoaded(string sceneName)
    {
        for(int i=0; i<BGMClips.Length; i++)
        {
            if(sceneName == BGMClips[i].name)
                PlayBGM(BGMClips[i]);
        }
    }

    public void PlayBGM(AudioClip _clip)
    {
        audioSources[0].clip = _clip;
        audioSources[0].loop = true;
        audioSources[0].volume = _bgmVolume;
        audioSources[0].Play();
    }

    public void SetEffectSound(string _clipName)
    {
        int index = 0;
        switch (_clipName)
        {
            case "Click":
                index = 0;
                break;
            case "Jump":
                index = 1;
                break;
            case "Hit":
                index = 2;
                break;
            case "ShootWire":
                index = 3;
                break;
            case "WireJump":
                index = 4;
                break;
            case "Lose":
                index = 5;
                break;
            default:
                Debug.LogWarning("OutOfRange in SoundClips");
                return;
        }
        PlayEffectSound(effectClips[index]);
    }

    public void PlayEffectSound(AudioClip _clip)
    {
        audioSources[1].clip = _clip;
        audioSources[1].loop = false;
        audioSources[1].volume = _effectVolume;
        audioSources[1].Play();
    }
}

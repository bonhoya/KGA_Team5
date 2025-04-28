using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
// 사운드를 사용할 네임스페이스 선언 구문
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;
    public AudioSource bgm;
    public AudioClip[] bgmlist;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //SceneManager.sceneLoaded += OnSceneLoaded;
            backGroundMusicPlay(bgmlist[0]);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // 기본적으로 출력하는 배경음악
    public void backGroundMusicPlay(AudioClip cilp)
    {
        bgm.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        bgm.clip = cilp;
        bgm.loop = true;
        bgm.volume = 0.5f;
        bgm.Play();
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(go, clip.length);

    }

    // 씬에 의해 변경되는 배경음악
    /*private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i < bgmlist.Length; i++)
        {
            if (arg0.name == bgmlist[i].name);
        }
    }*/

    // UI와 연결되는 배경음악 컨트롤러
    public void BGMController(float value)
    {
        audioMixer.SetFloat("BGMParam", Mathf.Log10(value) * 20);
    }

    // UI와 연결되는 효과음 컨트롤러
    public void SFXController(float value)
    {
        audioMixer.SetFloat("SFXParam", Mathf.Log10(value) * 20);
    }
}

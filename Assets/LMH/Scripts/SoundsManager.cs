using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
// ���带 ����� ���ӽ����̽� ���� ����
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;
    public AudioSource bgm;
    public AudioClip[] bgmlist;
    public AudioMixer audioMixer;
    public float bgmVolume = 1.0f;
    public float sfxVolume = 1.0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            backGroundMusicPlay(bgmlist[0]);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �⺻������ ����ϴ� �������
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

    // ���� ���� ����Ǵ� �������
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i < bgmlist.Length; i++)
        {
            if (arg0.name == bgmlist[i].name)
            {
                backGroundMusicPlay(bgmlist[i]);
            }
        }
    }

    // ����ϱ� ���ϱ� ���Ͽ� ���ܳ���.
    // UI�� ����Ǵ� ������� ��Ʈ�ѷ�
    /*public void BGMController(float value)
    {
        audioMixer.SetFloat("BGMParam", Mathf.Log10(value) * 20);
    }*/

    // UI�� ����Ǵ� ȿ���� ��Ʈ�ѷ�
    /*public void SFXController(float value)
    {
        audioMixer.SetFloat("SFXParam", Mathf.Log10(value) * 20);
    }*/
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenUI : MonoBehaviour
{
    [Header("SFX Setting")]
    [SerializeField] private AudioClip SelectMenuButtonClip;
    public void GameStart()
    {
        SoundsManager.Instance.SFXPlay("Select", SelectMenuButtonClip);
        SceneChangeManager.Instance.ChangeScene("TestingStageSelectScreen");
    }

    public void HowToPlay()
    {

    }

    public void GameQuit()
    {
        SoundsManager.Instance.SFXPlay("Select", SelectMenuButtonClip);
        Application.Quit();
        Debug.Log("����� ������ ����Ǿ����� ������ �󿡼��� ������� ���� ������ ó���˴ϴ�.");
    }
}

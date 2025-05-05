using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenUI : MonoBehaviour
{
    public void GameStart()
    {
        SceneChangeManager.Instance.ChangeScene("TestingStageSelectScreen");
    }

    public void HowToPlay()
    {

    }

    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("빌드된 게임이 종료되었지만 에디터 상에서는 종료되지 않은 것으로 처리됩니다.");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject stageSelect;

    private void Awake()
    {
        stageSelect.SetActive(false);
    }
    public void StartClick()
    {
        menu.SetActive(false);
        stageSelect.SetActive(true);
    }

    public void QuitClick()
    {
        //게임 꺼지는 코드
    }

    public void UndoClick()
    {
        menu.SetActive(true);
        stageSelect.SetActive(false);
    }

    public void StageClick(int n)
    {
        switch (n)
        {
            case 1:
                SceneManager.LoadScene("Map2");
                break;
            case 2:
                break;
            case 3:
                break;
        default:
                break;
        }
    }
}

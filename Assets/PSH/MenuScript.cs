using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject stageSelect;
    public GameObject option;

    private void Awake()
    {
        stageSelect.SetActive(false);
        option.SetActive(false);
    }
    public void StartClick()
    {
        menu.SetActive(false);
        stageSelect.SetActive(true);
    }

    public void QuitClick()
    {
        Application.Quit();
    }
    public void OptionClick()
    {
        menu.SetActive(false);
        option.SetActive(true);
    }
    public void UndoClick()
    {
        menu.SetActive(true);
        stageSelect.SetActive(false);
        option.SetActive(false);
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

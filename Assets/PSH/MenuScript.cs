using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //n에 따라 스테이지 씬 전환 다르게 코드
    }
}

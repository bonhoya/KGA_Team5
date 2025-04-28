using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    private bool isCleared;
    
    // 플레이어의 스테이지당 체력은 20
    
    private int playerHP;
    [SerializeField] private int gold;
    private Time stageTime;


    private void OnEnable()
    {
        isCleared = false;
        playerHP = 20;
    }
}

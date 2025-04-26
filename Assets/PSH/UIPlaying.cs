using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlaying : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;

    public Button DoubleSpeedBtn;
    public TextMeshProUGUI gameSpeedText;
    public Button PauseBtn;
    public TextMeshProUGUI gamePauseText;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timerText;


    private bool isPaused = false;
    private bool isDoubled = false;
    private bool isOver = false;

    private int health = 10;
    private int gold = 100;

    private float timer = 0f;
    private int gameSpeed = 1;
    private int[] waveTime;
    private int currentWave = 1;
    private void Start()
    {
        UpdateUI();
        waveTime = new int[] { 1,1,1,1,1};
    }

    private void Update()
    {
        if (isOver)
        {
            return;
        }

        timer += Time.deltaTime * gameSpeed;

        UpdateTimer();
        GameCleared();
        //여기에 체력과 골드 변화 조건 넣으면 됨
        //몬스터가 죽으면 골드를준다
        //몬스터가 성문에 도달하면 체력이 까인다

    }

    private void UpdateUI()
    {
        healthText.text = "체력: " + health.ToString();
        goldText.text = "골드: " + gold.ToString();
    }

    private void UpdateTimer()
    {
        float remainTime = waveTime[currentWave - 1] - timer;
        timerText.text = "Time : " + remainTime.ToString("F1") + "sec";
        if (remainTime <= 0)
        {
            currentWave++;
            waveText.text = "WAVE : " + currentWave + " / " + waveTime.Length;
            timer = 0;
        }      
    }

    void GameCleared()
    {
        if (currentWave > 5)//총 웨이브 수를 넘고 몬스터를 다 잡았다면
        {
            //게임멈추고 게임클리어 이미지를 열어
            PauseBtn.onClick.Invoke();
            Debug.Log("game clear!");
            isOver = true;
        }

        if (health <= 0)
        {
            //게임멈추고 게임오버 이미지를 열어
            isOver = true;
        }

    }

    public void PauseButtonClick()
    {
        if (isPaused)
        {
            gameSpeed = isDoubled? 2 : 1;
            isPaused = false;
            gamePauseText.text = "▷";
        }
        else
        {
            gameSpeed = 0;
            isPaused = true;
            gamePauseText.text = "l l";
        }
    }

    public void DoubleSpeedClick()
    {
        if (isDoubled)
        {
            gameSpeed = 1;
            isDoubled = false;
            gameSpeedText.text = "X1";
        }
        else
        {
            gameSpeed = 2;
            isDoubled = true;
            gameSpeedText.text = "X2";
        }
    }
}

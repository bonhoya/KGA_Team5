using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlaying : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;

    public Button DoubleSpeedBtn;
    public Button NormalSpeedBtn;
    public Button PauseBtn;
    public Button WaveBtn;
    public GameObject PopupPause;
    public GameObject PopupLose;
    public GameObject PopupWin;
    public GameObject PopupStageInfo;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timerText;

    public WaveLine waveLine;

    private bool isPaused = false;
    private bool isDoubled = false;
    private bool isOver = false;
    private bool isGameStarted = false;
    private bool isWave = false;

    private int health = 10;
    private int gold = 100;

    private float timer = 0f;
    private int gamePause = 1;
    private int currentWave = 0;

    private void OnEnable()
    {
        CameraController.OnCameraMoveDone += StartGame;
    }
    private void StartGame()
    {
        isGameStarted = true;
        PopupStageInfo.SetActive(false);
    }
    private void Start()
    {
        PopupPause.SetActive(false);
        PopupLose.SetActive(false);
        PopupWin.SetActive(false);

        UpdateUI();

        //웨이브 관련 정보 가져오기

    }

    private void Update()
    {
        if (isOver || !isGameStarted)
        {
            return;
        }

        timer += Time.deltaTime * gamePause;
        timerText.text = timer.ToString("F1") + Time.time.ToString("F1");//시간흐르는거확인용

        if (!isWave)
            WaveInfo();//웨이브 시작 전 

        GameCleared();
        //여기에 체력과 골드 변화 조건 넣으면 됨
        //몬스터가 죽으면 골드를준다
        //몬스터가 성문에 도달하면 체력이 까인다

        //적이 다 죽어서 웨이브 끝나면 isWave = false;
    }

    private void UpdateUI()
    {
        healthText.text = health.ToString();
        goldText.text = gold.ToString();
    }

    private void WaveInfo()//웨이브관련
    {
        WaveBtn.gameObject.SetActive(true);
        //웨이브가 이방향으로 옵니다 표시
        waveLine.DrawPath(1);
        //표시할것들은 currentWave와 관련되게 배열쓰면 될듯
        
    }

    public void WaveStartClick()
    {
        WaveBtn.gameObject.SetActive(false);
        waveLine.HidePath();
        isWave = true;
        currentWave = currentWave + 1;
        waveText.text = "WAVE : " + currentWave.ToString() + "/ 5";//5는 총 웨이브 변수로 바꿀것
    }
    void GameCleared()
    {
        if (currentWave > 5)// 게임 클리어했으면
        {
            //게임멈추고 게임클리어 이미지를 열어
            gamePause = 0;
            PopupWin.SetActive(true);
            isOver = true;
        }

        if (health <= 0)
        {
            //게임멈추고 게임오버 이미지를 열어
            gamePause = 0;
            PopupLose.SetActive(true);
            isOver = true;
        }

    }

    public void PauseButtonClick()
    {
        if (isPaused)
        {
            gamePause = 1;
            Time.timeScale = 1;
            isPaused = false;
            PopupPause.SetActive(false);
        }
        else
        {
            gamePause = 0;
            Time.timeScale = 0;
            isPaused = true;
            PopupPause.SetActive(true);
        }
    }

    public void DoubleSpeedClick()
    {
        if (isPaused)
            return;
        if (isDoubled)
        {
            Time.timeScale = 1;
            isDoubled = false;
            DoubleSpeedBtn.gameObject.SetActive(false);
            NormalSpeedBtn.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 2;
            isDoubled = true;
            DoubleSpeedBtn.gameObject.SetActive(true);
            NormalSpeedBtn.gameObject.SetActive(false);
        }
    }

    public void RestartClick()
    {
        //재시작버튼누르면어쩌고
    }

    public void QuitClick()
    {
        //끝버튼누르면어쩌고
    }

    public void NextClick()
    {
        //다음스테이지 어쩌고
    }


}

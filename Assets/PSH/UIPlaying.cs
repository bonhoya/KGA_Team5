using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool isWave = false;

    private int currentWave = 0;

    private void OnEnable()
    {
        CameraController.OnCameraMoveDone += StartGame;
        StartCoroutine(DelayStartCamera());
    }
    private void StartGame()
    {
        GameManager.Instance.isStageStarted = true;
        if (PopupStageInfo != null)
            PopupStageInfo.SetActive(false);
    }
    private IEnumerator DelayStartCamera()
    {
        yield return new WaitForSeconds(.1f);
        FindObjectOfType<CameraController>().StartCameraMove();
    }

    private void Start()
    {
        PopupPause.SetActive(false);
        PopupLose.SetActive(false);
        PopupWin.SetActive(false);

        Time.timeScale = 1;

        UpdateUI();


        //웨이브 관련 정보 가져오기

    }

    private void Update()//사실 업데이트가 여기서 할일은 아닌거같은 싱글톤에서 해야지
    {
        if (isOver || !GameManager.Instance.isStageStarted)
        {
            return;
        }

        // GameManager.Instance.timer += Time.deltaTime * GameManager.Instance.isGamePause;
        //timerText.text = timer.ToString("F1") + Time.time.ToString("F1");//시간흐르는거확인용

        if (!isWave)
            WaveInfo();//웨이브 시작 전 

        GameCleared();
        //여기에 체력과 골드 변화 조건 넣으면 됨
        //몬스터가 죽으면 골드를준다
        //몬스터가 성문에 도달하면 체력이 까인다

        //적이 다 죽어서 웨이브 끝나면 isWave = false;
    }




    public void UpdateUI()//골드와 체력이 변할때마다 적용해야할 함수
    {
        healthText.text = GameManager.Instance.playerLife.ToString();
        goldText.text = GameManager.Instance.gold.ToString();
    }

    private void WaveInfo()//웨이브관련. 웨이브 시작 전에 표시할 함수
    {
        WaveBtn.gameObject.SetActive(true);
        //웨이브가 이방향으로 옵니다 표시
        //waveLine.DrawPath(1, 1);//몇스테이지 몇번째웨이브라는뜻
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
            GameManager.Instance.isGamePause = 0;
            Time.timeScale = 0;
            PopupWin.SetActive(true);
            GameManager.Instance.playerLife = 20;
            GameManager.Instance.gold = 100;
        }

        if (GameManager.Instance.playerLife <= 0)
        {
            //게임멈추고 게임오버 이미지를 열어
            GameManager.Instance.isGamePause = 0;
            Time.timeScale = 0;
            PopupLose.SetActive(true);
            GameManager.Instance.isGameOver = true;
            GameManager.Instance.playerLife = 20;
            GameManager.Instance.gold = 100;
        }

    }

    public void PauseButtonClick()
    {
        if (isPaused)
        {
            GameManager.Instance.isGamePause = 1;
            Time.timeScale = 1;
            isPaused = false;
            PopupPause.SetActive(false);
        }
        else
        {
            GameManager.Instance.isGamePause = 0;
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
        GameManager.Instance.isStageStarted = false;
        CameraController.OnCameraMoveDone -= StartGame;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextClick()
    {
        //다음스테이지 어쩌고
    }


}

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildPoint : MonoBehaviour
{
    public GameObject buildUI;
    public GameObject manageUI;
    private Transform spawnPoint;

    private GameObject currentTower;
    private int a;//현재 선택중인 ui가 빈공간인지 buildUI인지 manageUI인지 구분하기위함 0, 1, 2

    //타워별배열
    public GameObject[] tower1;
    public GameObject[] tower2;
    public GameObject[] tower3;

    private int towerType = 0;
    private int towerLevel = 0;

    private void Awake()
    {
        spawnPoint = transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"현재a는{a}입니다");
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (!Physics.Raycast(ray, out RaycastHit hit) && a != 0)
            {
                CloseAllUI();
                a = 0;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 0f)//일시정지때는 작동 안하게
            return;

        if (currentTower == null)
        {
            if (a == 1 || a == 0) manageUI.SetActive(false);
            CloseAllUI();
            buildUI.SetActive(true);
            buildUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            StartCoroutine(ScaleUpCoroutine(buildUI));
            BuildManager.Instance.SetCurrentSpot(this);
            a = 1;
        }
        else
        {
            if (a == 2 || a == 0) buildUI.SetActive(false);
            CloseAllUI();
            manageUI.SetActive(true);
            manageUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            StartCoroutine(ScaleUpCoroutine(manageUI));
            BuildManager.Instance.SetCurrentSpot(this);
            a = 2;
        }
        //포지션 설정 생각해야함 클릭 주위로 원형으로 나오는건 괜찮은거같은데 위치에 따라 ui가 잘려보이거나 할 수 있음
        //맵을 넓히거나 ui 위치를 고정하고 클릭 위치를 하이라이트하거나
    }

    IEnumerator ScaleUpCoroutine(GameObject UI)
    {
        float duration = 0.2f;
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            UI.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }

        UI.transform.localScale = Vector3.one;
    }

    IEnumerator ScaleDownCoroutine(GameObject UI)
    {
        float duration = 0.2f;
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            UI.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            yield return null;
        }

        UI.transform.localScale = Vector3.zero;
    }

    public void CloseAllUI()
    {
        StartCoroutine(ScaleDownCoroutine(buildUI));
        StartCoroutine(ScaleDownCoroutine(manageUI));
    }
    public void CloseAllUI(int n)
    {
        switch (n)
        {
            case 1:
                StartCoroutine(ScaleDownCoroutine(buildUI));
                break;
            case 2:
                StartCoroutine(ScaleDownCoroutine(manageUI));
                break;
            default:
                break;
        }
    }
    public void BuildTower(int n)//n에 따라 타워 건설하기
    {
        towerType = n;
        towerLevel = 0;

        switch (n)
        {
            case 1:
                currentTower = Instantiate(tower1[0], spawnPoint.position + Vector3.up, Quaternion.identity);
                break;
            case 2:
                currentTower = Instantiate(tower2[0], spawnPoint.position + Vector3.up, Quaternion.identity);
                break;
            case 3:
                currentTower = Instantiate(tower3[0], spawnPoint.position + Vector3.up, Quaternion.identity);
                break;
            default:
                break;
        }
        //일단 스폰포지션에 1 더하는걸로 했는데 첨부터 스폰포인트를 조정하는게 나을거같은데
        buildUI.SetActive(false);
    }

    public void UpgradeTower()
    {
        towerLevel++;

        if (towerType == 1 && towerLevel < tower1.Length)
        {
            Destroy(currentTower);
            currentTower = Instantiate(tower1[towerLevel], spawnPoint.position + Vector3.up, Quaternion.identity);
        }
        else if (towerType == 2 && towerLevel < tower2.Length)
        {
            Destroy(currentTower);
            currentTower = Instantiate(tower2[towerLevel], spawnPoint.position + Vector3.up, Quaternion.identity);
        }
        else if (towerType == 3 && towerLevel < tower3.Length)
        {
            Destroy(currentTower);
            currentTower = Instantiate(tower3[towerLevel], spawnPoint.position + Vector3.up, Quaternion.identity);
        }
        else
        {
            towerLevel--; // 되돌리기
            Debug.Log("최대 업그레이드 단계입니다");
        }

        manageUI.SetActive(false);
    }

    public void RemoveTower()
    {
        Destroy(currentTower);
        currentTower = null;
        manageUI.SetActive(false);
    }
}

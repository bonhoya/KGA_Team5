using System.Collections;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public GameObject buildUI;
    public GameObject manageUI;
    public Transform spawnPoint; // 타워 위치 이거 왜있어야하는거임

    private GameObject currentTower;

    public GameObject testprefab;

    private void OnMouseDown()
    {
        if (currentTower == null)
        {
            buildUI.SetActive(true);
            buildUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            StartCoroutine(ScaleUpCoroutine(buildUI));
            BuildManager.Instance.SetCurrentSpot(this);
        }
        else
        {
            manageUI.SetActive(true);
            manageUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            StartCoroutine(ScaleUpCoroutine(manageUI));
            BuildManager.Instance.SetCurrentSpot(this);
        }
        //포지션 설정 생각해야함 클릭 주위로 원형으로 나오는건 괜찮은거같은데 위치에 따라 ui가 잘려보이거나 할 수 있음
        //맵을 넓히거나 ui 위치를 고정하고 클릭 위치를 하이라이트하거나
        //ui 2개 동시에 안나오게 하는 조건 필요
        //ui 끄는 방법 구현 필요 
    }

    IEnumerator ScaleUpCoroutine(GameObject UI)
    {
        float duration = 0.2f;
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer/ duration);
            UI.transform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one, t);
            yield return null;
        }

        UI.transform.localScale = Vector3.one;
    }
    public void BuildTower(GameObject prefab)
    {
        currentTower = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        buildUI.SetActive(false);
    }

    public void UpgradeTower()
    {
        // 업그레이드 로직
        // 업글전 타워파괴 업글후 타워생성
        Destroy(currentTower);
        currentTower = Instantiate(testprefab, spawnPoint.position, Quaternion.identity);
        //currentTower = Instantiate();
        //다음레벨 타워를 어떻게 가져오지
        Debug.Log("업그레이드됨");
        manageUI.SetActive(false);
    }

    public void RemoveTower()
    {        
        Destroy(currentTower);
        currentTower = null;
        manageUI.SetActive(false);
    }
}

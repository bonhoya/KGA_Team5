using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public GameObject buildUI;
    public GameObject manageUI;
    public Transform spawnPoint; // 타워 위치

    private GameObject currentTower;

    private void OnMouseDown()
    {
        if (currentTower == null)
        {
            buildUI.SetActive(true);
            buildUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            BuildManager.Instance.SetCurrentSpot(this);
        }
        else
        {
            manageUI.SetActive(true);
            manageUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            BuildManager.Instance.SetCurrentSpot(this);
        }
    }

    public void BuildTower(GameObject prefab)
    {
        currentTower = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        buildUI.SetActive(false);
    }

    public void UpgradeTower()
    {
        // 업그레이드 로직
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

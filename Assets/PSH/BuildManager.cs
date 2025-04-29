using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    private BuildPoint currentSpot;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCurrentSpot(BuildPoint spot)
    {
        currentSpot = spot;
    }

    public void BuildSelectedTower(GameObject towerPrefab)
    {
        currentSpot?.BuildTower(towerPrefab);
    }

    public void UpgradeTower()
    {
        currentSpot?.UpgradeTower();
    }

    public void RemoveTower()
    {
        currentSpot?.RemoveTower();
    }
}

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

    public void BuildSelectedTower(int n)
    {
        currentSpot?.BuildTower(n);
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

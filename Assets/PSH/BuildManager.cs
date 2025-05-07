using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    private BuildPoint currentSpot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
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
    public void CloseUI()
    {
        currentSpot?.CloseAllUI();
    }
}

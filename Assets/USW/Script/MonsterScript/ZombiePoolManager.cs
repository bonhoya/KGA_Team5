using UnityEngine;
using System.Collections.Generic;

public class ZombiePoolManager : MonoBehaviour
{
    
    public static ZombiePoolManager Instance { get; private set; }

    public GameObject zombiePrefab;
    public int maxPoolSize = 35;

    private List<GameObject> zombiePool = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        for (int i = 0; i < maxPoolSize; i++)
        {
            CreateAndAddZombie();
        }
    }

    // 풀에서 좀비 꺼내오기 (없으면 새로 생성)
    public GameObject GetZombie()
    {
        foreach (var zombie in zombiePool)
        {
            if (!zombie.activeInHierarchy)
            {
                zombie.SetActive(true);
                return zombie;
            }
        }

        // 풀에 남은 게 없으면 새로 생성해서 추가
        return CreateAndAddZombie(true);
    }

    // 좀비를 풀로 반환 (비활성화)
    public void ReturnZombie(GameObject zombie)
    {
        zombie.SetActive(false);
    }

    // 좀비 생성 + onDeath 이벤트 연결 + 풀에 추가
    private GameObject CreateAndAddZombie(bool activate = false)
    {
        GameObject zombie = Instantiate(zombiePrefab, transform);
        Zombie zombieScript = zombie.GetComponent<Zombie>();
        if (zombieScript != null)
        {
            zombieScript.onDeath -= ReturnZombie; // 중복 연결 방지
            zombieScript.onDeath += ReturnZombie;
        }
        zombie.SetActive(activate);
        zombiePool.Add(zombie);
        return zombie;
    }
}
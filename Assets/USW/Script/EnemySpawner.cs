using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 웨이브 단위로 적을 스폰하고, 오브젝트 풀링을 관리하는 클래스
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// 웨이브별 설정값을 담는 클래스 
    /// </summary>
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;    // 소환할 적 프리팹
        public int enemyCount;            // 웨이브당 적 개수
        public float spawnInterval = 1f;  // 적 소환 인터벌
        public float waveDelay = 5f;      // 다음 웨이브 시작 전 대기 시간
    }

    [Header("웨이브 설정")]
    public List<Wave> waves;              // 웨이브 설정 리스트

    [Header("위치 설정")]
    public Transform spawnPoint;          // 적 소환될 위치
    public Transform endPoint;            // 적 목표 위치

    [Header("풀링 설정")]
    public int poolSize = 50;             // 최대 풀 크기

    [Header("타이밍 설정")]
    public float startDelay = 2f;         // 게임 시작 후 첫 웨이브까지 대기 시간

    [Header("웨이포인트 경로")]
    public List<Transform> wayPoints;     // 웨이포인트 설정.
    
    
    private Dictionary<GameObject, List<GameObject>> enemyPools; // 프리팹별 오브젝트 풀
    private int currentWaveIndex = 0;     // 현재 웨이브 인덱스
    private bool isSpawning = false;      // 웨이브 진행 중 여부

    private UIPlaying _uiPlaying; // uiplaying 참조 변수 

    /// <summary>
    /// 게임 오브젝트가 생성될 때 풀 딕셔너리 초기화
    /// </summary>
    void Awake()
    {
        InitializePools();
        
        //ui playing 인스턴스 일어나자마자 찾아버리기 
        _uiPlaying = FindObjectOfType<UIPlaying>();
    }

    /// <summary>
    /// 게임 시작 시 풀을 미리 채우고, 웨이브 스폰 코루틴 시작
    /// </summary>
    void Start()
    {
        // 각 적 타입별로 poolsize만큼 미리 생성해서 풀에 저장
        foreach (var pool in enemyPools)
        {
            GameObject prefab = pool.Key;
            List<GameObject> enemies = pool.Value;

            for (int i = 0; i < poolSize; i++)
            {
                GameObject enemy = CreateEnemy(prefab);
                enemies.Add(enemy);
            }
        }

        // 웨이브 스폰 코루틴 시작
        StartCoroutine(StartWaves());
    }

    /// <summary>
    /// 웨이브에 등장하는 모든 적 프리팹에 대해 풀 딕셔너리 생성
    /// </summary>
    void InitializePools()
    {
        enemyPools = new Dictionary<GameObject, List<GameObject>>();
        foreach (Wave wave in waves)
        {
            // 프리팹이 중복되지 않게 한 번만 풀 생성
            if (wave.enemyPrefab != null && !enemyPools.ContainsKey(wave.enemyPrefab))
            {
                enemyPools.Add(wave.enemyPrefab, new List<GameObject>());
            }
        }
    }

    /// <summary>
    /// 적 프리팹을 인스턴스화해서 풀에 추가하는거
    /// </summary>
    GameObject CreateEnemy(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab, transform); // EnemySpawner 오브젝트의 자식으로 생성
        enemy.SetActive(false); // 풀에 넣을 때는 비활성화

        // Enemy 스크립트가 붙어있으면, 죽을 때 ReturnToPool이 호출되도록 이벤트 연결
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.onDeath += (enemyObj) => ReturnToPool(enemyObj, prefab);
        }
        return enemy;
    }

    /// <summary>
    /// 적이 죽으면 풀로 반환(비활성화)
    /// </summary>
    void ReturnToPool(GameObject enemy, GameObject prefab)
    {
        if (enemyPools.ContainsKey(prefab))
        {
            enemy.SetActive(false); // 비활성화해서 풀로 반환
        }
    }

    /// <summary>
    /// 풀에서 비활성화된 적을 꺼내와서 초기화 후 반환
    /// </summary>
    GameObject GetEnemyFromPool(GameObject prefab)
    {
        if (!enemyPools.ContainsKey(prefab)) return null;

        List<GameObject> enemies = enemyPools[prefab];
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true); // 먼저 활성화
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.Initialize(spawnPoint, endPoint,wayPoints); // 위치/목표 초기화
                }
                return enemy;
            }
        }

        // 풀 가득 찼으면 새로 만들지 않음
        return null;
    }

    /// <summary>
    /// 웨이브 전체를 관리하는 코루틴 
    /// </summary>
    IEnumerator StartWaves()
    {
        yield return new WaitForSeconds(startDelay); // 게임 시작 후 대기

        while (currentWaveIndex < waves.Count)
        {
            while (isSpawning) yield return null; // 이미 스폰 중이면 대기

            Wave currentWave = waves[currentWaveIndex];
            
       
            yield return StartCoroutine(SpawnWave(currentWave)); // 웨이브 스폰
            currentWaveIndex++;

            if (currentWaveIndex >= waves.Count)
            {
                Debug.Log("적 소환 끝"); // 모든 웨이브 종료
                yield break;
            }

            yield return new WaitForSeconds(currentWave.waveDelay); // 다음 웨이브까지 대기
        }
    }

    /// <summary>
    /// 웨이브의 적들을 순차적으로 소환하는 코루틴
    /// </summary>
    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;
        for (int i = 0; i < wave.enemyCount; i++)
        {
            GameObject enemy = GetEnemyFromPool(wave.enemyPrefab); // 풀에서 적 꺼내오기
            if (enemy == null)
            {
                // 풀에 남은 적이 없으면 아무것도 안함
            }
            yield return new WaitForSeconds(wave.spawnInterval); // 적 간 소환 간격
        }
        isSpawning = false;
    }
}

using UnityEngine;
using UnityEngine.AI;
using System;

/// <summary>
/// Enemy 클래스: 적 유닛의 행동과 풀링, 네비메시 이동을 담당
/// </summary>
public class Enemy : MonoBehaviour
{
    // ---------------[필드 선언부]----------------

    /// <summary>
    /// 적이 도달해야 할 목표 지점
    /// </summary>
    public Transform endPoint;

    /// <summary>
    /// 적이 생성될 위치 
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// 적의 체력
    /// </summary>
    public float health = 100f;

    /// <summary>
    /// 네비
    /// </summary>
    private NavMeshAgent agent;

    /// <summary>
    /// 적이 죽을 때 풀로 반환하는 이벤트 (풀링 시스템에서 사용)
    /// </summary>
    public event Action<GameObject> onDeath;

    // ---------------[초기화]----------------

    /// <summary>
    /// 오브젝트가 생성될 때(풀에서 꺼낼 때) 한 번 실행
    /// </summary>
    void Awake()
    {
        // 일단 일어나봐 메쉬야
        agent = GetComponent<NavMeshAgent>(); 
    }


    /// <summary>
    /// 적을 스폰할 때마다 호출되는 초기화 함수
    /// </summary>
    /// <param name="spawn">스폰 위치</param>
    /// <param name="end">목표 위치</param>
    public void Initialize(Transform spawn, Transform end)
    {
        spawnPoint = spawn;   // 스폰 위치 저장
        endPoint = end;       // 목표 위치 저장
        health = 100f;        // 체력 리셋

        // NavMeshAgent 재설정 (풀링 때문에 필수)
        agent.enabled = false;                        // NavMeshAgent 비활성화
        transform.position = spawnPoint.position;     // 위치 이동
        agent.enabled = true;                         // NavMeshAgent 재활성화
        
        // NavMesh 위에 제대로 올라갔는지 체크
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(endPoint.position);  // 목적지 설정 (이동 시작)
        }
        else
        {
            gameObject.SetActive(false);              // 적 오브젝트 비활성화(풀로 반환)
        }
    }

    // ---------------[게임 루프]----------------

    /// <summary>
    /// 매 프레임마다 호출 (적의 상태 체크)
    /// </summary>
    void Update()
    {
        // NavMeshAgent가 활성화되어 있고, 경로 계산 중이 아니며, 목표 지점에 거의 도달했다면
        if (agent.enabled && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            TakeDamage(1f); 
        }
    }

    // ---------------[전투/풀링 시스템]----------------

    /// <summary>
    /// 데미지를 입는 함수
    /// </summary>
    /// <param name="damage">입는 데미지 양</param>
    void TakeDamage(float damage)
    {
        health -= damage;         // 체력 감소
        if (health <= 0)
        {
            Die();                // 체력이 0 이하가 되면 사망 처리
        }
    }

    /// <summary>
    /// 적이 죽었을 때 호출 (풀로 반환)
    /// </summary>
    void Die()
    {
        // NavMeshAgent 비활성화
        agent.enabled = false;
        
        // 풀링 시스템에 반환 요청 (이벤트 호출)
        onDeath?.Invoke(gameObject); 
    }
}

using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;

/// <summary>
/// Enemy 클래스: 적 유닛의 행동과 풀링, 네비메시 이동, 적 상태 담당;
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
    /// 적 상태창
    /// </summary>
    [Header("적 상태창")]
    public float maxHealth = 0f;
    public float currentHealth;
    public float attackPower = 0f;
    public float defense = 0f;
    public float magicPower = 0f;
    public float magicResistance = 0f;
    public float moveSpeed = 0f;
    public Vector3 offset;
    /// <summary>
    /// 네비
    /// </summary>
    [Header("적 네비창")]
    [SerializeField] protected NavMeshAgent agent;

    /// <summary>
    /// 적이 죽을 때 풀로 반환하는 이벤트 (풀링 시스템에서 사용)
    /// </summary>
    public event Action<GameObject> onDeath;

    /// <summary>
    /// 웨이포인트 경로
    /// </summary>
    protected List<Transform> wayPoints; // 웨이포인트 경로
    protected int currentWayPointIndex; // 현재 목표 웨이포인트 인덱스

    // ---------------[초기화]----------------

    /// <summary>
    /// 오브젝트가 생성될 때(풀에서 꺼낼 때) 한 번 실행
    /// </summary>
    void Awake()
    {
        // 일단 일어나봐 메쉬야
        agent = GetComponent<NavMeshAgent>(); 
    }
    
    #region EnemySpawner 용 초기화 함수
    
    /// <summary>
    /// 적을 스폰할 때마다 호출되는 초기화 함수
    /// </summary>
    /// <param name="spawn">스폰 위치</param>
    /// <param name="end">목표 위치</param>
    public virtual void Initialize(Transform spawn, Transform end,List<Transform> wayPointList)
    {
        spawnPoint = spawn;   // 스폰 위치 저장
        endPoint = end;       // 목표 위치 저장

        //웨이포인트 경로 저장
        wayPoints = wayPointList;
        currentWayPointIndex = 0;
        
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
        
        // 첫번째 웨이포인트로 이동 시작 

        if (wayPoints != null && wayPoints.Count > 0)
        {
            agent.SetDestination(wayPoints[0].position);
        }
        else
        {
            agent.SetDestination(endPoint.position);
        }
        // 초기 애니메이션 초기화
        
        InitializeStats();
    }
#endregion


#region 네크로멘서용 Initialize

public virtual void Initialize(Vector3 spawnPos, Transform end, List<Transform> wayPointList, Vector3 zombieOffset,int startWayPointIndex)
{
    endPoint = end;
    wayPoints = wayPointList;
    currentWayPointIndex = startWayPointIndex;
    offset = zombieOffset; 

    agent.enabled = false;
    transform.position = spawnPos;
    agent.enabled = true;
    
    
    
    // NavMesh 위에 제대로 올라갔는지 체크
    if (agent.isOnNavMesh)
    {
        agent.SetDestination(endPoint.position);  // 목적지 설정 (이동 시작)
    }
    else
    {
        gameObject.SetActive(false);              // 적 오브젝트 비활성화(풀로 반환)
    }
        
   

    if (wayPoints != null && currentWayPointIndex <wayPoints.Count)
    {
        agent.SetDestination(wayPoints[currentWayPointIndex].position + offset);
    }
    else
    {
        agent.SetDestination(endPoint.position);
    }
    InitializeStats();
}

#endregion
    // ---------------[게임 루프]----------------

    /// <summary>
    /// 매 프레임마다 호출 (적의 상태 체크)
    /// </summary>
    protected virtual void Update()
    {
        if (agent.enabled && !agent.pathPending)
        {
            if (wayPoints != null && currentWayPointIndex < wayPoints.Count)
            {
                // 현재 웨이포인트에 거의 도달하면 다음 웨이포인트로
                if (agent.remainingDistance < 0.5f)
                {
                    currentWayPointIndex++;
                    if (currentWayPointIndex < wayPoints.Count)
                    {
                        agent.SetDestination(wayPoints[currentWayPointIndex].position+offset);
                    }
                    else
                    {
                        // 웨이포인트 끝나면 최종 목적지로 이동
                        agent.SetDestination(endPoint.position);
                    }
                }
            }
            else
            {
                // 최종 목적지 도달 체크
                if (agent.remainingDistance < 0.5f)
                {
                    GameManager.Instance.PlayerTakeDamage(1);
                    Die();
                }
            }
        }
        
    }


    // ---------------[전투/풀링 시스템]----------------

    /// <summary>
    /// 데미지를 입는 함수
    /// </summary>

    public virtual void InitializeStats()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakePhysicalDamage(float attackerAttack)
    {
        float damage = GameManager.Instance.CalculatePhysicalDamage(attackerAttack, this.defense);
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    public virtual void TakeMagicDamage(float attackerMagic)
    {
        float damage = GameManager.Instance.CalculateMagicDamage(attackerMagic, this.magicResistance);
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }


    /// <summary>
    /// 적이 죽었을 때 호출 (풀로 반환)
    /// </summary>
    protected virtual void Die()
    {
        // NavMeshAgent 비활성화
        agent.enabled = false;
        
        OnDeath();
    }

    // 다른쪽 스크립트에서 사용하기를 위함.
    protected virtual void OnDeath()
    {
        onDeath?.Invoke(gameObject);
    }
}

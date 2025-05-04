using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PortalTrigger : MonoBehaviour
{
    [Header("포탈 설정")]
    public Transform portalDestination; // 텔레포트 목적지
    public float triggerRadius = 1f; // 포탈 트리거 반경
    public int waypointIndex;

    private HashSet<Enemy> teleportedEnemies = new HashSet<Enemy>(); // 이미 텔레포트한 적 추적

    void Update()
    {
        // 근처에 있는 모든 적 체크
        Collider[] colliders = Physics.OverlapSphere(transform.position, triggerRadius);
        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && !teleportedEnemies.Contains(enemy))
            {
                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
                if (agent != null && agent.enabled)
                {
                    // 적을 포탈 목적지로 텔레포트
                    agent.Warp(portalDestination.position);
                    teleportedEnemies.Add(enemy);
                    Debug.Log($"{enemy.gameObject.name}이(가) 포탈을 통해 순간이동했습니다!");
                }
            }
        }
    }

    // 디버깅용: 트리거 반경 시각화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}
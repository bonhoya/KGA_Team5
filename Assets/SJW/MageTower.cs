using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [Header("마법 타워 전용 설정")]
    [SerializeField] private GameObject magicPrefab;
    [SerializeField] private Transform firePoint;

    private float nextAttackTime = 0f; // 다음 공격 가능한 시간

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Update()
    {
        base.Update(); // 부모 클래스에서 FindEnemy() 호출됨
    }

    protected override void Attack()
    {
        if (Time.time < nextAttackTime) return;

        if (currentTarget == null) return;

        GameObject energyBall = EnergyBallPool.Instance.GetFromPool();
        if (energyBall == null) return;

        energyBall.transform.position = firePoint.position;
        energyBall.transform.rotation = Quaternion.identity;

        // 방향 계산
        Vector3 direction = (currentTarget.position - firePoint.position).normalized;

        Rigidbody rb = energyBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float projectileSpeed = EnergyBallPool.Instance.ProjectileSpeed;
            rb.velocity = direction * projectileSpeed;
        }

        nextAttackTime = Time.time + (1f / attackSpeed);
    }


    protected override void Upgrade()
    {
        attackPower += 5f;
        range += 1f;
        attackSpeed += 0.2f;
        Debug.Log("업글");
    }
}


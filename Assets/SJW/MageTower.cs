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

        GameObject energyBall = EnergyBallPool.Instance.GetFromPool();
        if (energyBall == null) return;

        energyBall.transform.position = firePoint.position;
        energyBall.transform.rotation = Quaternion.identity;

        // 다음 공격 가능 시간 계산 (1초 / 초당 공격 횟수)
        if (attackSpeed > 0f)
            nextAttackTime = Time.time + (1f / attackSpeed);
        else
            nextAttackTime = Time.time + 1f; // 혹시 0일 경우 대비
    }

    protected override void Upgrade()
    {
        attackPower += 5f;
        range += 1f;
        attackSpeed += 0.2f;
        Debug.Log("업글");
    }
}


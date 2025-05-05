using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [Header("마법 타워 전용 설정")]
    [SerializeField] private GameObject magicPrefab;  // 마법 투사체 프리팹
    [SerializeField] private Transform firePoint;     // 발사 위치
    
    private Coroutine fireCoroutine;
    private YieldInstruction fireDelay;

    private float attackCooldown = 0f;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    private IEnumerator FireCoroutine()
    {
        yield return fireDelay;
    }



    void Update()
    {
        base.Update();

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    protected override void Attack()
    {
        GameObject energyBall = Instantiate(magicPrefab, firePoint.position, Quaternion.identity);
    }

    protected override void Upgrade()
    {
        attackPower += 5f;
        range += 1f;
        attackSpeed += 0.2f;
        Debug.Log("업글");
    }

}

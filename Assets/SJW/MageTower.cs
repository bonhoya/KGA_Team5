using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [Header("마법 타워 전용 설정")]
    [SerializeField] private GameObject magicPrefab;  // 마법 투사체 프리팹
    [SerializeField] private Transform firePoint;     // 발사 위치
    [SerializeField] private SphereCollider rangeCollider;

    private float attackCooldown = 0f;

    void Start()
    {
        if (rangeCollider != null)
        {
            rangeCollider.radius = range;
            rangeCollider.isTrigger = true;
            rangeCollider.center = Vector3.zero; // 타워 중심에 맞춤
        }
    }

    protected override void Update()
    {
        base.Update();

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    protected override void Attack()
    {
        if (attackCooldown > 0f || currentTarget == null) return;
        Debug.Log("마법발사!");
        attackCooldown = 1f / attackSpeed;
    }

    protected override void Upgrade()
    {
        attackPower += 5f;
        range += 1f;
        attackSpeed += 0.2f;
        Debug.Log("업글");
    }

    void OnValidate()
    {
        if (rangeCollider != null)
            rangeCollider.radius = range;
    }
}

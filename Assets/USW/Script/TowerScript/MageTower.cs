using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MageTower : Tower
{
    [Header("메이지 타워 설정")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    private float attackTimer = 0f;

    private void Start()
    {
        attackPower = 20f;
        range = 10f;
        attackSpeed = 5f; 
    }

// 왜 공격이 안되는거야 진짜
// position 은 그대로인데 순간이동은 왜하는건데 
// 디버그찍어볼려니깐 그떄는 또 가만히있고 이게 뭐여

    
    protected override void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 1f / attackSpeed)
        {
            if (currentTarget != null)
            {
                float damage = GameManager.Instance.CalculateMagicDamage(attackPower, currentTarget.GetComponent<Enemy>().magicResistance);
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(currentTarget.position - firePoint.position));
                MagicBolt magicBolt = projectile.GetComponent<MagicBolt>();
                if (magicBolt != null)
                {
                    magicBolt.Init(currentTarget, 5f, damage);
                }
                attackTimer = 0f;
            }
        }
    }

    protected override void Upgrade()
    {
        // 업그레이드는 못할것같음.
    }
}
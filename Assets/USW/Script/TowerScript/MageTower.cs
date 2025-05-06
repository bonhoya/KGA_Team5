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
        attackPower = 10f;
        range = 10f;
        attackSpeed = 3f; 
    }
    

    void Update()
    {
        Debug.Log("World Position: " + transform.position);
        Debug.Log("Local Position: " + transform.localPosition);
    }


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
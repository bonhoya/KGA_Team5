using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    [Header("아처 타워 설정")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackTimer = 0f;



    private void Start()
    {
        attackPower = 5f;
        range = 10f;
        attackSpeed = 10f;
    }
    // 어택타이머는 그치 타워에 두는게 맞지 
    // 일단 레이어로 퉁쳤으니깐 화살에도 그러면 ? 태그가 아닌 레이어에 두는게 맞지 신원아.
    
    // 잠시만 어쩌피 impulse 하는거는 화살이 책임지는게 맞고
    // 생성만 얘가 해주는건데 왜 안나가지 ? 
    
    // 생성자체가 안되는데 ? gizmo  ? 
    // 아니지 바로 앞에 있었는데 식별자체가 안되었짢아 . 
    
    protected override void Attack()
    {
        attackTimer += Time.deltaTime;
       
        Debug.Log("attackTimer" +attackTimer);
        Debug.Log("attackSpeed" +attackSpeed);

        if (attackTimer >= 1f / attackSpeed)
        {
            if (currentTarget != null)
            {
                Debug.Log("일단 적이 감지되었는지 확인"+currentTarget);
                float damage =
                    GameManager.Instance.CalculatePhysicalDamage(attackPower,
                        currentTarget.GetComponent<Enemy>().defense);

                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                
                ArcherTowerArrow arrow = projectile.GetComponent<ArcherTowerArrow>();
                if (arrow != null)
                {
                    arrow.Init(currentTarget,attackSpeed,attackPower);
                }

                attackTimer = 0f;
            }
        }

        
    }

    protected override void Upgrade()
    {
        //구현 못할듯 
    }
}

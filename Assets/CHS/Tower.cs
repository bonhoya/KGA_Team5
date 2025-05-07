using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tower은 그저 부모님일뿐 일은 자식클래스들이 할꺼임
public abstract class Tower : MonoBehaviour
{
    [Header("타워 스탯")] 
    [SerializeField] protected float attackPower;

    [SerializeField] protected float range;

    [SerializeField] protected float attackSpeed;

    [Header(" 적 탐지 설정")] 
    
    // 적 레이어 구별 
    [SerializeField] private LayerMask enemyLayer;

    
    // 현재 타겟 : 타워가 현재 공격하고 있는 타겟
    [SerializeField] protected Transform currentTarget;
    
    public float Damage => attackPower;
    public float Range => range;
    public float AttackSpeed => attackSpeed;

    protected virtual void Update()
    {
        FindEnemy();
    }

   protected virtual void FindEnemy()
    {
        // 현재 타겟이 있는지 확인
        if (currentTarget != null)
        {
            Enemy enemy = currentTarget.GetComponent<Enemy>();
            if (enemy != null && enemy.currentHealth > 0)
            {
                float distance = Vector3.Distance(transform.position, currentTarget.position);
                if (distance <= range)
                {
                    Vector3 lookPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
                    transform.LookAt(lookPos);
                    Attack();
                    return;
                }
                else
                {
               
                    currentTarget = null; 
                }
            }
            else
            {
                currentTarget = null;
            }
        }

        // currentTarget이 null이면 새로운 적을 찾기
        if (currentTarget == null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyLayer);
            float minDistance = float.MaxValue;
            Transform closestEnemy = null;

            foreach (var hit in hits)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null && enemy.currentHealth > 0)
                {
                    float distance = Vector3.Distance(transform.position, hit.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestEnemy = hit.transform;
                    }
                }
            }

            if (closestEnemy != null)
            {
                currentTarget = closestEnemy;
                Vector3 lookPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
                transform.LookAt(lookPos);
                Attack();
            }
        }
    }

    // 공격로직인데 자식클래스에서 구현할껍니다.
    protected abstract void Attack();

    protected abstract void Upgrade();
    


}

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
        if (currentTarget != null && currentTarget.gameObject.activeInHierarchy)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.position);

            if (distance <= range)
            {   // 적을 바라보도록 타워 방향을 조정하고 y축은 건들지말것 , 건들면 얘 심하게 흔들어 재낌 
                Vector3 lookPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
                transform.LookAt(lookPos);
                Attack();
                
                // 이미 타겟있으니깐 더이상 새로운 적 안찾음.
                return;
            }
            else
            {
                // 타겟이 범위를 벗어났으면 타겟을 초기화 
                currentTarget = null;
            }
        }
        
        // 새로운 타겟 찾고 적 레이어 해당되고 활성화가 된 타겟으로 설정 
        Collider[] hits = Physics.OverlapSphere(transform.position, range,enemyLayer);
        foreach (var hit in hits)
        {
            if (hit.gameObject.layer == 6 && hit.gameObject.activeInHierarchy)
            {
                currentTarget = hit.transform;
                break;
            }
        }

        if (currentTarget != null)
        {
            Vector3 lookPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
            transform.LookAt(lookPos);
            Attack();
        }
        
        
    }
    // 공격로직인데 자식클래스에서 구현할껍니다.
    protected abstract void Attack();

    protected abstract void Upgrade();
    


}

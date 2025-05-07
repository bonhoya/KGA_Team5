using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerArrow : MonoBehaviour
{

    [SerializeField] private float arrowSpeed = 10f;

    [SerializeField] private Transform target;

    [SerializeField] private float damage;
    
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody arrowRb;
    
    // 그럼 포물선, 적레이어 , 데미지 , 속도 , 목표 , 또 필요한게 있나 ?  


    private void Awake()
    {
        arrowRb = GetComponent<Rigidbody>();
      
    }

    public void Init(Transform target, float arrowSpeed, float damage)
    {
        this.target = target;
        this.damage = damage;
        this.arrowSpeed = arrowSpeed;
        
       /* if (target != null && arrowRb != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            arrowRb.velocity = direction * arrowSpeed;
            
            
            // 임펄스를 여기에다가 안줘도 되나 ? 아니지 어쩌피 초기설정이니깐 ? 
            // 
            //방향을 설정 해야하는데
            // 타겟하고 화살속도는 정해놨고 
            // 어쩌피 레이어 가져와서 적구분  할꺼니깐 
            
        }
        */
    }

    private void FixedUpdate()
    {
        if (target != null && arrowRb != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            arrowRb.velocity = direction * arrowSpeed;
            transform.LookAt(target);
            
            
            // 임펄스를 여기에다가 안줘도 되나 ? 아니지 어쩌피 초기설정이니깐 ? 
            // 
            //방향을 설정 해야하는데
            // 타겟하고 화살속도는 정해놨고 
            // 어쩌피 레이어 가져와서 적구분  할꺼니깐 
            
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
        // 일단 여기에서 타겟이 사라지면 화살은 어떻게할꺼임 ? 
        
        // 그대로 둬 ? 만약 그렇게 하면 전에 그 유튜브에서 봤던것 처럼 논타겟형 타워로 만드는것도 한방법이긴하지만 그건 
        
        // 업그레이드 티어에다가 두는게 맞을뿐더러 시간이 없다 신원아
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 6)
        {
            Enemy newEnemy = other.gameObject.GetComponent<Enemy>();
            if (newEnemy != null)
            {
                newEnemy.TakePhysicalDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}

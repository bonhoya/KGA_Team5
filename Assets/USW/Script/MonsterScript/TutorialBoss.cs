using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TutorialBoss : Enemy
{
    private Animator animator;
    [SerializeField] public ParticleSystem deathEffect;

    private void Awake()
    {
        agent= GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    public override void Initialize(Transform spawn, Transform end, List<Transform> wayPointList)
    {
        maxHealth = 100f; 
        currentHealth = maxHealth; 
        defense = 0.1f; 
        magicResistance = 0.1f; 
        moveSpeed = 4f; 
        
        base.Initialize(spawn, end, wayPointList);
        animator.SetBool("isMoving", false);
        animator.SetBool("isSkill", false);
        animator.SetBool("isDead", false);
        
    }
    
    protected override void Update()
    {
        base.Update();
        if (agent.enabled && !agent.pathPending)
        {
            bool isMoving = agent.velocity.magnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);
        }
    }

    protected override void Die()
    {
        agent.enabled = false;
        animator.SetBool("isDead", true);
        Invoke("TriggerOnDeath", 2.0f);
        
    }

    private void TriggerOnDeath()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        base.OnDeath();
    }
    
    public virtual void UseSkill()
    {
        animator.SetBool("isSkill", true);
        Invoke("EndSkill", 1.0f);
    }

    private void EndSkill()
    {
        animator.SetBool("isSkill", false);
    }

    // 만약에 int 에다가 isMoving 하잖아요 ? 그러면 Ismoving 에 ㅎ시값 (  int ) 를 변환해서 변수에 저장하는 시스템이래요
    // setbool 함수는 string 하고 int를 둘다 받을수있데요 
    // 미리 만들어둔 해시값을 넘겨줄수있다는 내요 ㅇ이 
    // move , Move , MOve 다 다른 해시값이잖아요
    // 

}

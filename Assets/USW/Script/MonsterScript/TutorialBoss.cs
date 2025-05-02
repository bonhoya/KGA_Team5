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
}

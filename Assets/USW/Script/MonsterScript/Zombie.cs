using UnityEngine;

public class Zombie : Enemy
{
    public override void InitializeStats()
    {
        maxHealth = 100f;
        moveSpeed = 2f;
        base.InitializeStats();
        if (agent != null)
            agent.speed = moveSpeed;
    }

    
    
    
    protected override void Die()
    {
       
        base.Die();
    }
}
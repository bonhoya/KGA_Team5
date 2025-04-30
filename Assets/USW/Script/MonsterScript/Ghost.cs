using UnityEngine;

public class Ghost : Enemy
{
    
    public override void InitializeStats()
    {
        maxHealth = 50f; 
        currentHealth = maxHealth; 
        defense = 1f; // 물리 방어력 100% 
        magicResistance = 0.1f; // 마법 저항력 10%
        moveSpeed = 6f; 
        
        // NavMeshAgent 속도 업데이트
        if (agent != null)
        {
            agent.speed = moveSpeed;
        }
    }

    // 피지컬 대미지를 받았을 때 피해를 입지 않도록 재정의
    public override void TakePhysicalDamage(float attackerAttack)
    {
      
        float damage = GameManager.Instance.CalculatePhysicalDamage(attackerAttack, this.defense);
        
        currentHealth -= damage; 
        if (currentHealth <= 0) Die(); // 만약의 경우를 대비해 체크
    }

    // 매직 대미지는 피해를 받도록 재정의
    public override void TakeMagicDamage(float attackerMagic)
    {
        float damage = GameManager.Instance.CalculateMagicDamage(attackerMagic, this.magicResistance);
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }
}
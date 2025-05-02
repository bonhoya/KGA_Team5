using UnityEngine;

public class Orc : Enemy
{
    
    
    private bool hadEnraged = false;
    
    // 분노 상태 관련 변수
    private bool isEnraged = false;
    
    // 속도 증가 중인지 여부
    private bool isEnraging = false; 
    
    // 속도 감소 중인지 여부
    private bool isCalming = false; 
    
    // 체력이 30% 이하일 때 분노 상태 돌입
    private float enrageThreshold = 0.3f; 
    
    // 분노 시 속도 2배 증가
    private float enrageSpeedMultiplier = 2f; 
    
    // 속도 증가 지속 시간 
    private float enrageDuration = 2f; 
    
    // 속도 감소 지속 시간 
    private float calmDuration = 2f; 
    
    // 속도 증가 타이머
    private float enrageTimer = 0f; 
    
    // 속도 감소 타이머
    private float calmTimer = 0f; 
    
    // 원래 속도 저장
    private float baseSpeed = 0f; 
    
    // 분노 상태 목표 속도 저장
    private float enragedSpeed = 0f; 

    
    public override void InitializeStats()
    {
        maxHealth = 100f; 
        currentHealth = maxHealth; 
        defense = 0.5f; 
        magicResistance = 0.1f; 
        moveSpeed = 2.5f; 
   
        
        // 분노 상태 초기화
        isEnraged = false;
        isEnraging = false;
        isCalming = false;
        hadEnraged = false;
        
        // 원래 속도 저장
        baseSpeed = moveSpeed; 

        
        if (agent != null)
        {
            agent.speed = moveSpeed;
        }
    }



    protected override void Update()
    {
        base.Update();

        // 분노 상태 체크 체력이 일정 수준 이하로 떨어지면 분노 상태 돌입
        if (!isEnraged && !hadEnraged && currentHealth <= maxHealth * enrageThreshold)
        {
            StartEnrage(); 
        }


        // 타이머로 스타트해주고 
        if (isEnraging)
        {
            enrageTimer += Time.deltaTime;
            moveSpeed = Mathf.Lerp(moveSpeed, enragedSpeed, enrageTimer / enrageDuration);

            if (agent != null)
            {
                agent.speed = moveSpeed;
            }


            if (enrageTimer >= enrageDuration)
            {
                isEnraging = false;
                isCalming = true;
                calmTimer = 0f;
            }
        }




        if (isCalming)
        {
            calmTimer += Time.deltaTime;
            moveSpeed = Mathf.Lerp(enragedSpeed, baseSpeed, calmTimer / calmDuration);

            if (agent != null)
            {
                agent.speed = moveSpeed;
            }

            if (calmTimer >= calmDuration)
            {
                isCalming = false;
                isEnraged = false;
            }
        }

    }

    // 기본 물리계산
    public override void TakePhysicalDamage(float attackerAttack)
    {
        float damage = GameManager.Instance.CalculatePhysicalDamage(attackerAttack, this.defense);
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    // 기본 마법계산
    public override void TakeMagicDamage(float attackerMagic)
    {
        float damage = GameManager.Instance.CalculateMagicDamage(attackerMagic, this.magicResistance);
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    // 분노 상태 시작 메서드
    private void StartEnrage()
    {
        hadEnraged = true;
        isEnraged = true;
        isEnraging = true;
        enrageTimer = 0f;
        baseSpeed = moveSpeed; 
        enragedSpeed = baseSpeed * enrageSpeedMultiplier; 
    }
}

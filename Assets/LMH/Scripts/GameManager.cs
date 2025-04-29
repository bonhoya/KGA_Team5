using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int playerLife = 10;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    
    /// <summary>
    /// 플레이어가 데미지를 입었을 때 라이프를 감소시키는 함수
    /// </summary>
    public void PlayerTakeDamage(int damage)
    {
        playerLife -= damage;
        if (playerLife <= 0)
        {
            playerLife = 0;
            
            Debug.Log("Player life has been destroyed");
        }
    }

    
    /// <summary>
    /// 물리, 마법 데미지 계산 공식
    /// 퍼센테이지 경감방식을 채택하였습니다.
    ///
    /// 
    /// Enemy 스크립트에서 magicResist 0.2f  혹 defence 0.2f 를 준다면 타워측에서 주는 데미지의 20퍼 경감 처리 됩니다.
    ///
    /// 
    /// 타워하시는분은 public float attackPower / public float magicPower로 같이 통일 해주시고
    /// 
    /// public void AttackEnemy(Enemy enemy)
    /// {   
    ///     float damage = GameManger.Instance.CalculatePhysicalDamage(attackPower,enemy.defense);
    ///   
    ///     enemy.TakePhysicalDamage(damage);
    /// }
    ///
    ///
    /// 
    /// 와 같이 적용시켜주시면 됩니다.
    /// 저 또한 defense , magicResist 로 통일할꺼라서 그렇게 짜주시면 될것같아요. 
    /// </summary>
    
   
    
    public float CalculatePhysicalDamage(float attackerAttack, float defenderDefence)
    {
        float reduced = attackerAttack * (1f- defenderDefence);
        return Mathf.Max(0, reduced);
    }

    public float CalculateMagicDamage(float attackerMagic, float defenderMagicRresist)
    {
        float reduced = attackerMagic * (1f - defenderMagicRresist);
        return Mathf.Max(0, reduced);
    }
    
   //-------------------------------------------
   //[수치 통일 규칙]
   //-------------------------------------------
   //- 타워는 attackPower, magicPower로 공격력을 통일
   //- Enemy는 defense, magicResist로 방어력을 통일
   //- 퍼센트 경감 방식이므로 defense/magicResist 값은 0~1 사이로 설정
   //-------------------------------------------
}


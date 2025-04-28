using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    public float Damage { get; }
    public float Range { get; }
    public float AttackSpeed { get; }
    void Attack(); // target: enemy
    void Upgrade();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private float energySpeed;

    private void OnEnable()
    {
        rigid.AddForce(transform.forward * energySpeed, ForceMode.Impulse);
    }
}

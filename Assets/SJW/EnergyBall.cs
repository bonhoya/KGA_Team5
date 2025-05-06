using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    private float lifeTime = 2f;

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    private void ReturnToPool()
    {
        EnergyBallPool.Instance.ReturnToPool(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke(); // 혹시 중복 방지
    }
}


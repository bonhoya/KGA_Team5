using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField] private Rigidbody rigid;

    [SerializeField] private float arrowSpeed;

    public Stack<GameObject> returnPool;

    private void Update()
    {
        if (rigid.velocity.magnitude > 1)
        {
            transform.forward= rigid.velocity;
        }
    }
    private void OnEnable()
    {
        rigid.AddForce(transform.forward * arrowSpeed, ForceMode.Impulse);
        StartCoroutine(ReturnPool(2f));
    }

    private IEnumerator ReturnPool(float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        rigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
        returnPool.Push(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            
            StartCoroutine(ReturnPool());
        }
    }
}

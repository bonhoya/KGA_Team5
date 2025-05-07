using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigid;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask target;
    [SerializeField] private AudioClip clip;
    private Coroutine coroutineBattle;
    private YieldInstruction delayTime;

    private Vector3 playerVec3;

    private void Start()
    {
        delayTime = new WaitForSeconds(1f);
    }

    private void Update()
    {
        Move();
        OnDetected();
    }

    private void FixedUpdate()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        playerVec3 = new Vector3(x, 0, z).normalized;
    }

    private void Move()
    {
        playerRigid.velocity = playerVec3 * playerSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    private void OnDetected()
    {
        if (Physics.OverlapSphere(transform.position, detectRadius, target).Length > 0)
        {
            if (coroutineBattle == null)
            {
                coroutineBattle = StartCoroutine(Battle());
            }
        }
        else
        {
            if(coroutineBattle != null)
            {
                StopCoroutine(coroutineBattle);
                coroutineBattle = null;
            }
        }
    }

    private IEnumerator Battle()
    {
        SoundsManager.Instance.SFXPlay("battle", clip);
        yield return delayTime;
    }
}

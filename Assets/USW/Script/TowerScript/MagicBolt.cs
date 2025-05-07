using UnityEngine;

public class MagicBolt : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private Transform target;
    [SerializeField] private float damage;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(Transform target, float speed, float damage)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
            transform.LookAt(target.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeMagicDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
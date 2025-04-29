using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public enum WeaponState { SearchTarget, AttackToTarget }        // 적을 발견하고 쏜다
public class ArcherTower : MonoBehaviour, ITower
{

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform muzzlePos;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float attackRate;

    [SerializeField] private Animator animator;

    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private int poolSize;
    public Stack<GameObject> arrowPool;

    private Coroutine arrowCoroutine;
    private YieldInstruction arrowDelay;

    [SerializeField] private Transform targetPos;

    private string arrowType;       // 나중에 추가할 속성? 능력?

    [SerializeField] private Transform currentTarget;     // 현재 타겟 저장

    [SerializeField] private float attackPower;
    [SerializeField] private float range;       // 사거리
    [SerializeField] private float attackSpeed;

    
    public float Damage => attackPower;
    public float Range => range;
    public float AttackSpeed => attackSpeed;

    private void Awake()
    {
        arrowDelay = new WaitForSeconds(attackRate);
    }


    private void Start()
    {
        arrowPool = new Stack<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(arrowPrefab);
            obj.GetComponent<Arrow>().returnPool = arrowPool;
            obj.SetActive(false);
            arrowPool.Push(obj);
        }
    }


    private void Update()
    {
        findEnemy();
    }

    private void findEnemy()
    {
        if (currentTarget != null && currentTarget.gameObject.activeInHierarchy)
        {
            // 현재 타겟이 여전히 사거리 안에 있는지 확인
            float distance = Vector3.Distance(transform.position, currentTarget.position);
            if (distance <= range)
            {
                Vector3 lookPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
                transform.LookAt(lookPos);

                if (arrowCoroutine == null)
                {
                    Attack();
                }

                return; // 현재 타겟 유지 중이므로 새 타겟 찾지 않음
            }
            else
            {
                // 타겟 사거리 벗어남
                currentTarget = null;
            }
        }

        // 새 타겟 찾기 (레이어 6번만)
        Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyLayer);

        foreach (var hit in hits)
        {
            if (hit.gameObject.layer == 6 && hit.gameObject.activeInHierarchy)      // 태그 사용 시 other.CompareTag("Enemy")
            {
                currentTarget = hit.transform;
                break;
            }
        }

        if (currentTarget != null)
        {
            Vector3 lookPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
            transform.LookAt(lookPos);

            if (arrowCoroutine == null)
            {
                Attack();
            }
        }
        else
        {
            // 타겟 없음
            if (arrowCoroutine != null)
            {
                StopCoroutine(arrowCoroutine);
                arrowCoroutine = null;
            }
        }
    }
    public void Attack()
    {
        if (arrowCoroutine == null)
        {
            arrowCoroutine = StartCoroutine(AttackCoroutine());
        }
    }
    public void AttackEnemy(Enemy enemy)
    {
        float damage = GameManager.Instance.CalculatePhysicalDamage(attackPower, enemy.defense);

        enemy.TakePhysicalDamage(damage);
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {

            GameObject arrow = arrowPool.Pop();

            arrow.transform.position = muzzlePos.position;
            arrow.transform.forward = transform.forward;

            arrow.SetActive(true);

            yield return arrowDelay;
        }
    }

    public void CriticalShot()
    {

    }
    
    public void Upgrade()
    {
        attackPower += 5f;
        attackSpeed *= 0.9f;
        arrowDelay = new WaitForSeconds(attackRate / attackSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

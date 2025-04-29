using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public enum WeaponState { SearchTarget, AttackToTarget }
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

    private string arrowType;

    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float attackSpeed;
    

    public float Damage => damage;
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
        if (Physics.OverlapSphere(transform.position, range, enemyLayer).Length > 0)
        {
            Vector3 lookPos = new Vector3(targetPos.position.x, transform.position.y, targetPos.position.z);
            transform.LookAt(lookPos);
            if (arrowCoroutine == null)
            {
                Attack();
            }
        }
        else
        {
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

    public void Upgrade()
    {
        damage += 5f;
        attackSpeed *= 0.9f;
        arrowDelay = new WaitForSeconds(attackRate / attackSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

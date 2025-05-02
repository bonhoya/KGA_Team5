using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AcherTower : Tower
{
    [SerializeField] private GameObject acherTower;

    [SerializeField] private GameObject arrowPrefab;

    [SerializeField] private Transform muzzlePos;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float attackRate;

    [SerializeField] private int poolSize;
    public Stack<GameObject> arrowPool;

    private Coroutine arrowCoroutine;
    private YieldInstruction arrowDelay;

    [SerializeField] private Transform targetPos;

    [SerializeField, Range(0f, 1f)] private float criticalChance = 0.2f;   // 20% 확률로 크리티컬
    [SerializeField] private float criticalMultiplier = 1.5f;             // 크리티컬 시 1.5배 데미지

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
    

    protected override void Attack()
    {
        if (arrowCoroutine == null)
        {
            arrowCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    public void AttackEnemy(Enemy enemy)
    {
        float finalDamage = attackPower;

        if (Random.value < criticalChance)
        {
            finalDamage *= criticalMultiplier;
            CriticalShot();
        }

        float damage = GameManager.Instance.CalculatePhysicalDamage(attackPower, enemy.defense);

        enemy.TakePhysicalDamage(damage);
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {

            GameObject arrow = arrowPool.Pop();

            arrow.transform.position = muzzlePos.position;
            arrow.transform.forward = muzzlePos.forward;

            arrow.SetActive(true);

            yield return arrowDelay;
        }
    }

    public void CriticalShot()
    {
        Debug.Log("Critical");
    }

    protected override void Upgrade()
    {
        attackPower += 9;
        attackSpeed -= 0.4f;
        range += 4;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

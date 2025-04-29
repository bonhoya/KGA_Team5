using UnityEngine;
using System.Collections;

public class Necromancer : Enemy
{
    public int zombiesToSpawn = 7;
    public float summonInterval = 7f; // 7초마다 소환
    protected bool isAlive = true;

    private Coroutine summonRoutine;
    

    public override void InitializeStats()
    {
        maxHealth = 80f;
        moveSpeed = 3.5f;
        isAlive = true;
        base.InitializeStats();
        if (agent != null)
            agent.speed = moveSpeed;
    }

    protected override void Update()
    {
        base.Update();

        // 한 번만 코루틴 시작
        if (summonRoutine == null && agent.enabled)
        {
            summonRoutine = StartCoroutine(SummonLoop());
        }
    }

    private IEnumerator SummonLoop()
    {
        // 네크로멘서가 살아있는 동안 반복
        while (isAlive)
        {
            yield return new WaitForSeconds(summonInterval);

            // 소환 전 캐스팅 연출(1초)
            if (agent.enabled)
                agent.isStopped = true;

            yield return new WaitForSeconds(1f);

            // 네크로멘서 근처에 좀비 소환
            for (int i = 0; i < zombiesToSpawn; i++)
            {
                float offsetX = Random.Range(-3f, 3f); // 로컬 x축
                float offsetZ = Random.Range(-3f, 3f); // 로컬 z축
                
                // 네크로멘서 기준으로 좀비 소환
                Vector3 spawnPos = transform.position 
                                   + transform.right * offsetX
                                   + transform.forward * offsetZ;
                
                
                // y축은 네크로멘서랑 맞추고
                spawnPos.y = transform.position.y;

                GameObject zombieObj = ZombiePoolManager.Instance.GetZombie();
                zombieObj.transform.position = spawnPos;
                zombieObj.transform.rotation = Quaternion.identity;

                Zombie zombie = zombieObj.GetComponent<Zombie>();
                if (zombie != null)
                    zombie.Initialize(spawnPos, endPoint, null);
            }

            if (agent.enabled)
                agent.isStopped = false;
        }
        yield break;
    }

    // 네크로멘서가 죽으면 코루틴 정지
    protected override void Die()
    {
        isAlive = false;
        if (summonRoutine != null)
        {
            StopCoroutine(summonRoutine);
            summonRoutine = null;
        }
        base.Die();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    enum PHASE
    {
        Ready,          // 다음 웨이브 기다리는 상태.
        StartWave,      // 웨이브 시작.
        Spawn,          // 스폰 중인 상태.
        End,            // 스폰이 다 끝난 상태.
    }

    [SerializeField] Enemy enemyPrefab;             // 적 프리팹.
    [SerializeField] Transform vectorParent;        // 방향.
    [SerializeField] int spawnEnemyCount;           // 생성되는 적 숫자.
    [SerializeField] float spawnTime;               // 스폰 간격 시간.
    [SerializeField] float waveTime;                // 웨이브 간격 시간.

    Transform[] vectors;
    int enemyCount;                                 // 현재 나와있는 적의 수.

    float nextWaveTime;
    float nextSpawnTime;

    PHASE phase;                                    // 현재 스폰 매니저의 상태.

    int wave = 0;


    void Start()
    {
        vectors = new Transform[vectorParent.childCount];       // 배열을 자식의 개수만큼 할당.
        for(int i = 0; i<vectorParent.childCount; i++)
        {
            vectors[i] = vectorParent.GetChild(i);
        }

        // 상태 초기화.
        TopBarUI.Instance.SetWaveText(wave);
        nextWaveTime = Time.time + waveTime;
        phase = PHASE.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

        switch(phase)
        {
            case PHASE.Ready:
                if (nextWaveTime <= Time.time)
                {
                    wave += 1;
                    TopBarUI.Instance.SetWaveText(wave);

                    phase = PHASE.Spawn;
                }
                else
                {
                    float remainingTime = nextWaveTime - Time.time;  // 남은 시간.
                    TopBarUI.Instance.SetNextWaveTime((int)remainingTime);
                }
                break;
            case PHASE.Spawn:
                if(Spawn() == false)
                    phase = PHASE.End;

                break;
            case PHASE.End:
                if (enemyCount <= 0)
                {
                    phase = PHASE.Ready;
                    nextWaveTime = Time.time + waveTime;
                }

                break;
        }

        // 1. 만약 다음 웨이브 타임이 되었다면
        // 2. 웨이브 발생.
        // 3. 적이 다 사라지고 웨이브가 끝났다면.
        // 4. 1번으로 이동.
    }

    bool Spawn()
    {
        if (enemyCount >= spawnEnemyCount)
            return false;

        if (nextSpawnTime <= Time.time)
        {
            nextSpawnTime = Time.time + spawnTime;
            enemyCount++;

            Enemy enemy = Instantiate(enemyPrefab, vectors[0].position, Quaternion.identity);
            enemy.transform.SetParent(transform);
            enemy.Setup(vectors, OnDeadEnemy);
        }

        return true;
    }
    void OnDeadEnemy()
    {
        enemyCount -= 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    enum PHASE
    {
        Ready,          // ���� ���̺� ��ٸ��� ����.
        StartWave,      // ���̺� ����.
        Spawn,          // ���� ���� ����.
        End,            // ������ �� ���� ����.
    }

    [SerializeField] Enemy enemyPrefab;             // �� ������.
    [SerializeField] Transform vectorParent;        // ����.
    [SerializeField] int spawnEnemyCount;           // �����Ǵ� �� ����.
    [SerializeField] float spawnTime;               // ���� ���� �ð�.
    [SerializeField] float waveTime;                // ���̺� ���� �ð�.

    Transform[] vectors;
    int enemyCount;                                 // ���� �����ִ� ���� ��.

    float nextWaveTime;
    float nextSpawnTime;

    PHASE phase;                                    // ���� ���� �Ŵ����� ����.

    int wave = 0;


    void Start()
    {
        vectors = new Transform[vectorParent.childCount];       // �迭�� �ڽ��� ������ŭ �Ҵ�.
        for(int i = 0; i<vectorParent.childCount; i++)
        {
            vectors[i] = vectorParent.GetChild(i);
        }

        // ���� �ʱ�ȭ.
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
                    float remainingTime = nextWaveTime - Time.time;  // ���� �ð�.
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

        // 1. ���� ���� ���̺� Ÿ���� �Ǿ��ٸ�
        // 2. ���̺� �߻�.
        // 3. ���� �� ������� ���̺갡 �����ٸ�.
        // 4. 1������ �̵�.
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

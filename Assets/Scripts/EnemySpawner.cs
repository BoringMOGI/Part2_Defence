using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Transform vectorParent;
    [SerializeField] int enemyCount;
    [SerializeField] float spawnTime;

    Transform[] vectors;
    int createCount;
    float nextSpawnTime;

    void Start()
    {
        vectors = new Transform[vectorParent.childCount];       // 배열을 자식의 개수만큼 할당.
        for(int i = 0; i<vectorParent.childCount; i++)
        {
            vectors[i] = vectorParent.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(createCount < enemyCount && nextSpawnTime <= Time.time)
        {
            nextSpawnTime = Time.time + spawnTime;
            createCount++;
            Spawn();
        }
    }

    void Spawn()
    {
        Enemy enemy = Instantiate(enemyPrefab, vectors[0].position, Quaternion.identity);
        enemy.transform.SetParent(transform);
        enemy.Setup(vectors);
    }
}

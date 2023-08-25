using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] float spawnDelay = 2;

    float elaspeDelayTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        elaspeDelayTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (elaspeDelayTime >= spawnDelay)
        {
            GameObject enemy = EnemyPool.Instance.SpawnEnemy(-1, transform.position);

           // GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], transform.position, Quaternion.identity);

            elaspeDelayTime = 0;
        }   

        elaspeDelayTime += Time.deltaTime;
    }
}

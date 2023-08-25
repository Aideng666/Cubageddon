using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;

    Queue<GameObject>[] availableEnemies;

    int enemiesPerPool = 5;

    public static EnemyPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);

            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        availableEnemies = new Queue<GameObject>[enemyPrefabs.Length];

        //for (int i = 0; i < availableEnemies.Length; i++)
        //{
        //    CreatePools((EnemyTypes)i);
        //}

        for (int i = 0; i < availableEnemies.Length; i++)
        {
            availableEnemies[i] = new Queue<GameObject>();
        }
    }

    void CreatePools(EnemyTypes type)
    {
        for (int i = 0; i < enemiesPerPool; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[(int)type], Vector3.zero, Quaternion.identity, transform);

            availableEnemies[(int)type].Enqueue(enemy);

            enemy.SetActive(false);
        }
    }

    public GameObject SpawnEnemy(int type, Vector3 pos)
    {
        //Select Random
        if (type == -1)
        {
            type = Random.Range(0, enemyPrefabs.Length);
        }

        if (availableEnemies[type].Count <= 0)
        {
            CreatePools((EnemyTypes)type);
        }

        GameObject enemy = availableEnemies[(int)type].Dequeue();
        enemy.SetActive(true);
        enemy.transform.position = pos;

        return enemy;
    }

    public void AddEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);

        availableEnemies[(int)enemy.GetComponent<Enemy_Base>().EnemyType].Enqueue(enemy);
    }
}

public enum EnemyTypes
{
    Brute,
    PistolGunner,
    MachineGunner,
    Shotgunner,
    Sniper,
    Stomper,
    Splitter,
    Spinner
}


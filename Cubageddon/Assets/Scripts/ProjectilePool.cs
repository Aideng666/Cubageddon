using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{

    [SerializeField] GameObject playerProjectile;
    [SerializeField] GameObject enemyProjectile;

    Queue<GameObject> availablePlayerProjectiles;
    Queue<GameObject> availableEnemyProjectiles;

    int projectilesPerPool = 20;

    public static ProjectilePool Instance { get; private set; }

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
        availableEnemyProjectiles = new Queue<GameObject>();
        availablePlayerProjectiles = new Queue<GameObject>();

        CreatePools();
    }

    void CreatePools()
    {
        for (int i = 0; i < projectilesPerPool; i++)
        {
            GameObject bullet = Instantiate(enemyProjectile, Vector3.zero, Quaternion.identity, transform);

            availableEnemyProjectiles.Enqueue(bullet);
            bullet.SetActive(false);

            bullet = Instantiate(playerProjectile, Vector3.zero, Quaternion.identity, transform);

            availablePlayerProjectiles.Enqueue(bullet);
            bullet.SetActive(false);
        }
    }

    public GameObject SpawnProjectile(int type, Vector3 pos) // 0 = player | 1 = Enemy for the type
    {
        GameObject bullet = null;

        if (type == 0)
        {
            if (availablePlayerProjectiles.Count <= 0)
            {
                CreatePools();
            }

            bullet = availablePlayerProjectiles.Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = pos;
        }
        else if (type == 1)
        {
            if (availableEnemyProjectiles.Count <= 0)
            {
                CreatePools();
            }

            bullet = availableEnemyProjectiles.Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = pos;
        }

        return bullet;
    }

    public void AddProjectileToPool(GameObject bullet, int type)
    {
        bullet.SetActive(false);

        if (type == 0)
        {
            availablePlayerProjectiles.Enqueue(bullet);
        }
        else if (type == 1)
        {
            availableEnemyProjectiles.Enqueue(bullet);
        }
    }
}

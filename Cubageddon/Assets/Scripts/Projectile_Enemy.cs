using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Enemy : MonoBehaviour
{
    Player_Base player;
    Enemy_Base enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Base>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(enemy.AttackDamage);
        }

        //Change to pool
        Destroy(gameObject);
    }

    public void SetEnemy(Enemy_Base enemy)
    {
        this.enemy = enemy;
    }
}

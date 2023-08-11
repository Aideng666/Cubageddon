using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected int collisionDamage = 5;

    protected Player_Base player;
    protected Rigidbody2D body;
    protected int currentHealth;
    protected float elaspedAttackDelay = 0;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player_Base>();

        currentHealth = maxHealth;
        elaspedAttackDelay = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();

        if (CanAttack())
        {
            Attack();
        }

       elaspedAttackDelay += Time.deltaTime;
    }

    protected virtual void Move()
    {

    }

    protected virtual void Attack()
    {
        
    }

    bool CanAttack()
    {
        if (elaspedAttackDelay >= attackDelay)
        {
            elaspedAttackDelay = 0;

            return true;
        }

        return false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        //Change to object pooling

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(collisionDamage);
        }
    }
}

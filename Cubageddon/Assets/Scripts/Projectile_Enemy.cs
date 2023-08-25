using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Projectile_Enemy : MonoBehaviour
{
    Player_Base player;
    Enemy_Base enemy;
    SpriteRenderer sprite;

    ParticleSystem hitParticle;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Base>();
        sprite = GetComponent<SpriteRenderer>();
        hitParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(enemy.AttackDamage);
        }

        StartCoroutine(BulletHit());
    }

    public void SetEnemy(Enemy_Base enemy)
    {
        this.enemy = enemy;
    }

    IEnumerator BulletHit()
    {
        hitParticle.Play();

        sprite.enabled = false;

        yield return new WaitForSeconds(0.2f);

        sprite.enabled = true;

        ProjectilePool.Instance.AddProjectileToPool(gameObject, 0);
    }
}

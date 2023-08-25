using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Player : MonoBehaviour
{
    Player_Base player;
    ParticleSystem hitParticle;
    SpriteRenderer sprite;

    int weaponUsed = 0; // 0 = Primary, 1 = Secondary

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Base>();
        sprite = GetComponent<SpriteRenderer>();
        hitParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (weaponUsed == 0)
            {
                collision.gameObject.GetComponent<Enemy_Base>().TakeDamage(player.PrimaryDamage);
            }
            else if (weaponUsed == 1)
            {
                collision.gameObject.GetComponent<Enemy_Base>().TakeDamage(player.SecondaryDamage);
            }
        }

        StartCoroutine(BulletHit());
    }

    public void SetWeaponUsed(int weapon)
    {
        weaponUsed = weapon;
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

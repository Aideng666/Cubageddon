using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PistolGunner : Enemy_Base
{
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float desiredShootDistance = 6f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        if (body.velocity.magnitude < moveSpeed)
        {
            float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
            float walkOffset = 1.5f;

            Vector3 moveDir = Vector3.zero;

            //move Away from player
            if (distanceFromPlayer < desiredShootDistance - walkOffset)
            {
                moveDir = (transform.position - player.transform.position).normalized;
                moveDir.z = 0;

                moveDir = Quaternion.Euler(0, 0, Random.Range(-45f, 45f)) * moveDir;
            }
            //move towards player
            else if (distanceFromPlayer > desiredShootDistance + walkOffset)
            {
                moveDir = (player.transform.position - transform.position).normalized;
                moveDir.z = 0;

                moveDir = Quaternion.Euler(0, 0, Random.Range(-45f, 45f)) * moveDir;
            }
            //Move perpendicular to player
            else if (distanceFromPlayer < desiredShootDistance + walkOffset && distanceFromPlayer > desiredShootDistance - walkOffset)
            {
                moveDir = (transform.position - player.transform.position).normalized;
                moveDir.z = 0;

                int direction = Random.Range(0, 2);

                if (direction == 0)
                {
                    moveDir = Quaternion.Euler(0, 0, 90) * moveDir;
                }
                else if (direction == 1)
                {
                    moveDir = Quaternion.Euler(0, 0, -90) * moveDir;
                }
            }

            body.AddForce(moveDir * moveSpeed, ForceMode2D.Force);
        }

        transform.up = player.transform.position - transform.position;
    }

    protected override void Attack()
    {
        //check if enemy has line of sight on player
        RaycastHit2D hit = Physics2D.Raycast(transform.position + ((player.transform.position - transform.position).normalized), (player.transform.position - transform.position).normalized);

        print(hit.collider.gameObject.tag);

        if (hit.collider.gameObject.CompareTag("Wall"))
        {
            return;
        }

        //Creates a bullet and shoots it towards the player
        Vector3 bulletDirection = (player.transform.position - transform.position).normalized;
        bulletDirection.z = 0;
        bulletDirection = bulletDirection.normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.transform.up = bulletDirection;

        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
        bullet.GetComponent<Projectile_Enemy>().SetEnemy(this);
    }
}

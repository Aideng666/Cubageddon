using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Player : MonoBehaviour
{
    Player_Base player;

    int weaponUsed = 0; // 0 = Primary, 1 = Secondary

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

        //Change to pool
        Destroy(gameObject);
    }

    public void SetWeaponUsed(int weapon)
    {
        weaponUsed = weapon;
    }
}

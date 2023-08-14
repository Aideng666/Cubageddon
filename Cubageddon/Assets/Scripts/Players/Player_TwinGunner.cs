using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_TwinGunner : Player_Base
{
    [SerializeField] Animator leftGunAnim;
    [SerializeField] Animator rightGunAnim;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;

    float enableDelay = 0.1f;
    float elaspedDelayTime = 0;
    bool delayActive;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.primaryFireAction.canceled += CancelLeftShoot;
            InputManager.Instance.secondaryFireAction.canceled += CancelRightShoot;
        }
        else
        {
            delayActive = true;
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (InputManager.Instance.primaryFireAction.IsPressed())
        {
            PrimaryFire();
        }

        if (InputManager.Instance.secondaryFireAction.IsPressed())
        {
            SecondaryFire();
        }

        if (delayActive) 
        {
            if (elaspedDelayTime >= enableDelay)
            {
                InputManager.Instance.primaryFireAction.canceled += CancelLeftShoot;
                InputManager.Instance.secondaryFireAction.canceled += CancelRightShoot;

                delayActive = false;
            }

            elaspedDelayTime += Time.deltaTime;
        }
    }

    protected override void PrimaryFire()
    {
        leftGunAnim.SetBool("Shooting", true);

        if (CanPrimaryFire())
        {
            GameObject bullet = Instantiate(bulletPrefab, leftGunAnim.transform.position, Quaternion.identity);

            bullet.transform.up = transform.up;

            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
            bullet.GetComponent<Projectile_Player>().SetWeaponUsed(0);
            bullet.GetComponent<SpriteRenderer>().color = leftGunAnim.gameObject.GetComponent<SpriteRenderer>().color;
        }
    }

    protected override void SecondaryFire()
    {
        rightGunAnim.SetBool("Shooting", true);

        if (CanSecondaryFire())
        {
            GameObject bullet = Instantiate(bulletPrefab, rightGunAnim.transform.position, Quaternion.identity);

            bullet.transform.up = transform.up;

            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
            bullet.GetComponent<Projectile_Player>().SetWeaponUsed(1);
            bullet.GetComponent<SpriteRenderer>().color = rightGunAnim.gameObject.GetComponent<SpriteRenderer>().color;
        }
    }

    protected override void MovementSkill()
    {

    }

    private void CancelLeftShoot(InputAction.CallbackContext context)
    {
        leftGunAnim.SetBool("Shooting", false);
    }

    private void CancelRightShoot(InputAction.CallbackContext context)
    {
        rightGunAnim.SetBool("Shooting", false);
    }
}

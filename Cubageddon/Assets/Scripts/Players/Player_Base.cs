using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Player_Base : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float primaryFireDelay;
    [SerializeField] protected float secondaryFireDelay;

    protected Rigidbody2D body;
    protected Vector2 moveDirection;
    protected Vector3 aimPosition;

    float elaspedPrimaryFireDelay = 0;
    float elaspedSecondaryFireDelay = 0;

    //private void OnEnable()
    //{
    //    InputManager.Instance.primaryFireAction.performed += PrimaryFire;
    //    InputManager.Instance.secondaryFireAction.performed += SecondaryFire;
    //    InputManager.Instance.movementSkillAction.performed += MovementSkill;
    //}

    //private void OnDisable()
    //{
    //    InputManager.Instance.primaryFireAction.performed -= PrimaryFire;
    //    InputManager.Instance.secondaryFireAction.performed -= SecondaryFire;
    //    InputManager.Instance.movementSkillAction.performed -= MovementSkill;
    //}

    // Start is called before the first frame update
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        moveDirection = Vector3.zero;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Aim();

        elaspedPrimaryFireDelay += Time.deltaTime;
        elaspedSecondaryFireDelay += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        InputAction input = InputManager.Instance.moveAction;

        if (input.IsPressed())
        {
            moveDirection = input.ReadValue<Vector2>();
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        if (body.velocity.magnitude < maxSpeed)
        {
            body.AddForce(moveDirection * moveSpeed, ForceMode2D.Force);
        }
    }
    protected void Aim()
    {
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimPos.z = 0;

        aimPosition = aimPos;

        transform.up = aimPos - transform.position;
    }

    protected virtual void PrimaryFire()
    {
        
    }

    protected virtual void SecondaryFire()
    {

    }

    protected virtual void MovementSkill()
    {

    }

    protected bool CanPrimaryFire()
    {
        if (elaspedPrimaryFireDelay > primaryFireDelay)
        {
            elaspedPrimaryFireDelay = 0;

            return true;
        }

        return false;
    }

    protected bool CanSecondaryFire()
    {
        if (elaspedSecondaryFireDelay > secondaryFireDelay)
        {
            elaspedSecondaryFireDelay = 0;

            return true;
        }

        return false;
    }
}

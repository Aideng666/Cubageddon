using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;

    Rigidbody2D body;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        moveDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Aim();
    }

    void Move()
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
    void Aim()
    {
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimPos.z = 0;

        transform.up = aimPos - transform.position;
    }
}

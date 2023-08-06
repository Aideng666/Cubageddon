using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float followOffsetX = 3;
    [SerializeField] float followOffsetY = 3;
    [SerializeField] Player_Base player;

    Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (transform.position.x < player.transform.position.x - followOffsetX)
        {
            moveVelocity.x = 1;
            //body.AddForce(Vector3.right * player.MaxSpeed, ForceMode2D.Force);
        }
        else if (transform.position.x > player.transform.position.x + followOffsetX)
        {
            moveVelocity.x = -1;
            //body.AddForce(Vector3.left * player.MaxSpeed, ForceMode2D.Force);
        }

        if (transform.position.y < player.transform.position.y - followOffsetY)
        {
            moveVelocity.y = 1;
            //body.AddForce(Vector3.up * player.MaxSpeed, ForceMode2D.Force);
        }
        else if (transform.position.y > player.transform.position.y + followOffsetY)
        {
            moveVelocity.y = -1;
            //body.AddForce(Vector3.down * player.MaxSpeed, ForceMode2D.Force);
        }

        if (moveVelocity.magnitude != 0)
        {
            body.velocity = moveVelocity * player.MaxSpeed / 5;
        }
    }
}

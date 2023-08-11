using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Brute : Enemy_Base
{
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
            Vector3 moveDir = (player.transform.position - transform.position).normalized;

            body.AddForce(moveDir * moveSpeed, ForceMode2D.Force);
        }

        transform.up = player.transform.position - transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI_Follow : MonoBehaviour
{
    public Transform target;

    public float moveSpeed = 100f;
    public float newWaypointDist = 0.5f;
    public float desiredDistanceFromTarget = 0;

    Path path;
    int currentWaypoint = 0;
    bool endOfPathReached = false;

    Seeker seeker;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        body = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        target = FindObjectOfType<Player_Base>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            endOfPathReached = true;

            return;
        }

        endOfPathReached = false;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - body.position).normalized;
        Vector2 force = direction * moveSpeed * Time.deltaTime;

        body.AddForce(force);

        float distance = Vector2.Distance(body.position, path.vectorPath[currentWaypoint]);

        if (distance < newWaypointDist)
        {
            currentWaypoint++;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            float distanceToTarget = Vector2.Distance(body.position, target.position);

            if (distanceToTarget > desiredDistanceFromTarget)
            {
                seeker.StartPath(body.position, target.position, OnPathComplete);
            }
            else if (distanceToTarget <= desiredDistanceFromTarget)
            {
                Vector2 directionToMove = body.position - (Vector2)target.position;

                seeker.StartPath(body.position, body.position + directionToMove, OnPathComplete);
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}

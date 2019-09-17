using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector]
    public Transform player;

    public float speed = 100f;

    public float nextWayPointDistance = 2f;

    private Path path;

    private int curWayPoint;

    private bool reachedEnd = false;

    private Seeker seeker;

    private Rigidbody2D rb;

    void Start()
    {
        player = PlayerHealth.instance.transform;

        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (player == null)
            return;

        if(seeker.IsDone())
            seeker.StartPath(rb.position, player.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
        }
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        if (path == null)
            return;

        if (curWayPoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[curWayPoint] - rb.position).normalized;

        Vector2 force = dir * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[curWayPoint]);
        if (distance < nextWayPointDistance)
            curWayPoint++;
    }

}

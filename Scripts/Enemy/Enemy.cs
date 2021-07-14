using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    protected Transform m_target;

    protected float m_speed;
    protected float m_detectRate;
    protected float m_nextWaypointDistance;

    protected float m_wanderRange;
    protected float m_detectRange;
    protected float m_attackRange;

    private Seeker m_seeker;
    private Rigidbody2D m_rigidbody;

    private Path m_path;
    private int m_currentWaypoint = 0;
    private float m_lastRepath = float.NegativeInfinity;

    protected virtual void Start()
    {
        m_seeker = GetComponent<Seeker>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        float targetDistance = Vector2.Distance(m_rigidbody.position, m_target.position);

        if (targetDistance > m_detectRange)
        {
            Wander();
        }
        else if (targetDistance > m_attackRange)
        {
            Chase();
        }
        else if (targetDistance < m_attackRange)
        {
            Attack();
        }
    }

    protected virtual void Wander() { }
    protected virtual void Chase() { }
    protected virtual void Attack() { }
    protected void UpdatePath()
    {
        if (Time.time > m_lastRepath + m_detectRate && m_seeker.IsDone())
        {
            m_lastRepath = Time.time;
            m_seeker.StartPath(m_rigidbody.position, m_target.position, OnPathComplete);
        }

        if (m_path == null) return;

        float distance;
        while (true)
        {
            distance = Vector2.Distance(m_rigidbody.position, m_path.vectorPath[m_currentWaypoint]);
            if (distance < m_nextWaypointDistance)
            {
                if (m_currentWaypoint + 1 < m_path.vectorPath.Count) m_currentWaypoint++;
                else break;
            }
            else break;
        }

        Vector2 direction = ((Vector2)m_path.vectorPath[m_currentWaypoint] - m_rigidbody.position).normalized;
        m_rigidbody.AddForce(direction * m_speed * Time.fixedDeltaTime);
    }

    private void OnPathComplete(Path path)
    {
        path.Claim(this);
        if (!path.error)
        {
            if (m_path != null) m_path.Release(this);
            m_path = path;
            m_currentWaypoint = 0;
        }
        else path.Release(this);
    }
}

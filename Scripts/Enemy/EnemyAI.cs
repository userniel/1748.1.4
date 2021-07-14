using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform m_target;
    public float m_speed = 200f;
    public float m_nextWaypointDistance = 3f;
    public float m_repathRate = 0.5f;

    private Path m_path;
    private int m_currentWaypoint = 0;
    private float m_lastRepath = float.NegativeInfinity;

    private Seeker m_seeker;
    private Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_target = FindObjectOfType<PlayerZombie>().transform;

        m_seeker = GetComponent<Seeker>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (Time.time > m_lastRepath + m_repathRate && m_seeker.IsDone())
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
        //Debug.Log("A path was calculated. Did it fail with an error? " + path.error);

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

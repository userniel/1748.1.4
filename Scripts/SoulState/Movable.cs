using UnityEngine;

public class Movable : State
{
    private readonly Rigidbody2D rigidbody;
    private readonly float modifier;
    
    public Movable(PlayerSoul playerSoul) : base(playerSoul)
    {
        rigidbody = m_playerSoul.m_target.m_entity;
        modifier = m_playerSoul.m_target.m_modifier;

        m_playerSoul.m_target.m_core = rigidbody.position;
    }

    public override void Control()
    {
        Vector2 direction;
        direction.x = Input.GetAxisRaw("HorizontalSoul");
        direction.y = Input.GetAxisRaw("VerticalSoul");
        m_playerSoul.m_isMoving = direction != Vector2.zero;

        if (!m_playerSoul.m_target.m_isKinematic)
        {
            rigidbody.AddForce(direction * m_playerSoul.m_strength, ForceMode2D.Impulse);
            if (m_playerSoul.m_target.m_useLimits && m_playerSoul.m_isMoving)
            {
                if (rigidbody.velocity.magnitude > m_playerSoul.m_target.m_limitMax) rigidbody.velocity = rigidbody.velocity.normalized * m_playerSoul.m_target.m_limitMax;
            }
            //Debug.Log(rigidbody.velocity.magnitude);
        }
        else
        {
            rigidbody.MovePosition(rigidbody.position + direction * m_playerSoul.m_strength * modifier * Time.fixedDeltaTime);
        }

        m_playerSoul.m_target.m_core = rigidbody.position;
    }
    public override void Possess()
    {
        base.Possess();
        if (!m_playerSoul.m_isControlling) m_playerSoul.SetState(new Soul(m_playerSoul));
    }
}

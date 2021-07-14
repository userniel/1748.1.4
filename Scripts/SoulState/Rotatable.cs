using UnityEngine;

public class Rotatable : State
{
    private readonly Rigidbody2D rigidbody;
    private readonly float modifier;

    public Rotatable(PlayerSoul playerSoul) : base(playerSoul)
    {
        rigidbody = m_playerSoul.m_target.m_entity;
        modifier = m_playerSoul.m_target.m_modifier;

        m_playerSoul.m_target.m_core = rigidbody.position;
    }

    public override void Control()
    {
        float direction = -Input.GetAxisRaw("HorizontalSoul");
        m_playerSoul.m_isMoving = direction != 0f;

        if (!m_playerSoul.m_target.m_isKinematic)
        {
            rigidbody.AddTorque(direction * m_playerSoul.m_strength, ForceMode2D.Impulse);
            if (m_playerSoul.m_target.m_useLimits && m_playerSoul.m_isMoving)
            {
                if (Mathf.Abs(rigidbody.angularVelocity) > m_playerSoul.m_target.m_limitMax) rigidbody.angularVelocity = direction * m_playerSoul.m_target.m_limitMax;
            }
            //Debug.Log(rigidbody.angularVelocity);
        }
        else
        {
            rigidbody.MoveRotation(rigidbody.rotation + direction * m_playerSoul.m_strength * modifier * Time.fixedDeltaTime);
        }

        m_playerSoul.m_target.m_core = rigidbody.position;
    }
    public override void Possess()
    {
        base.Possess();
        if (!m_playerSoul.m_isControlling) m_playerSoul.SetState(new Soul(m_playerSoul));
    }
}

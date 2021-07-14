using UnityEngine;

public class Soul : State
{
    public Soul(PlayerSoul playerSoul) : base(playerSoul) { }

    public override void Control()
    {
        Vector2 velocity;
        velocity.x = Input.GetAxis("HorizontalSoul");
        velocity.y = Input.GetAxis("VerticalSoul");

        m_playerSoul.m_isMoving = velocity != Vector2.zero;

        m_playerSoul.transform.Translate(velocity * m_playerSoul.m_moveSpeed * Time.fixedDeltaTime);
    }
    public override void Possess()
    {
        base.Possess();
        if (m_playerSoul.m_isControlling)
        {
            switch (m_playerSoul.m_target.m_type)
            {
                case Type.Diasble:
                    break;
                case Type.Movable:
                    m_playerSoul.SetState(new Movable(m_playerSoul));
                    break;
                case Type.Rotatable:
                    m_playerSoul.SetState(new Rotatable(m_playerSoul));
                    break;
            }
        }
    }
}

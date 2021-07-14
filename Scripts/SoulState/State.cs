using UnityEngine;

public abstract class State
{
    public enum Type { Diasble, Movable, Rotatable };
    // Clickable?

    protected PlayerSoul m_playerSoul = null;

    public State(PlayerSoul playerSoul) { m_playerSoul = playerSoul; }

    public virtual void Control() { }
    public virtual void Possess()
    {
        if (Input.GetKeyDown(KeyCode.E)) m_playerSoul.m_isControlling ^= true;
    }
}

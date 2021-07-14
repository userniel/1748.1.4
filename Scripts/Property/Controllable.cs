using UnityEngine;

public class Controllable : MonoBehaviour
{
    public State.Type m_type = State.Type.Diasble;
    public Rigidbody2D m_entity = null;

    public bool m_isKinematic;
    public bool m_useLimits;
    public float m_limitMax;
    public float m_limitMin;
    public float m_modifier;

    public bool m_isTargeted;
    public Vector2 m_core;

    private Color m_default;
    private Color m_targeted;
    private Color m_controlled;

    private void Start()
    {
        m_default = GetComponent<SpriteRenderer>().color;
        m_targeted = Color.yellow;
        m_controlled = Color.cyan;

        m_entity = GetComponent<Rigidbody2D>();
        m_entity.isKinematic = m_isKinematic;
        m_entity.mass = m_entity.transform.localScale.x * m_entity.transform.localScale.y * 10f;
        m_entity.tag = "Controllable";

        m_isTargeted = false;
        m_core = m_entity.position;

        switch (m_type)
        {
            case State.Type.Diasble:
                m_entity.tag = "Untagged";
                enabled = false;
                break;
            case State.Type.Movable:
                if (m_limitMax < 0f) m_limitMax *= -1f;
                break;
            case State.Type.Rotatable:
                if (m_limitMax < 0f) m_limitMax *= -1f;
                break;
        }
    }
    private void Update()
    {
        SetSprite();
    }

    private void SetSprite()
    {
        SpriteRenderer sprite = m_entity.GetComponent<SpriteRenderer>();
        PlayerSoul playerSoul = FindObjectOfType<PlayerSoul>();

        if (m_isTargeted)
        {
            sprite.sortingLayerName = "Target";
            if (playerSoul != null) sprite.color = playerSoul.m_isControlling ? m_controlled : m_targeted;
        }
        else
        {
            sprite.sortingLayerName = "Default";
            sprite.color = m_default;
        }
    }
}

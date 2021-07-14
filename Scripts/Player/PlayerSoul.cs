using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerSoul : MonoBehaviour
{
    public float m_moveSpeed;
    public float m_strength;
    public Image m_valueBar;

    [HideInInspector] public bool m_isControlling;
    [HideInInspector] public bool m_isMoving;
    [HideInInspector] public bool m_isTired;
    [HideInInspector] public Controllable m_target;

    private Controllable[] m_others;
    private State m_state;
    private float m_energy;

    private void Start()
    {
        if (m_moveSpeed == 0.0f) m_moveSpeed = 5.0f;
		if (m_strength == 0.0f) m_strength = 1.7f;

        m_isControlling = false;
        m_isMoving = false;
        m_target = null;

        m_others = null;
        m_state = new Soul(this);
        m_energy = 100f;
    }

    public void CustomUpdate()
    {
        if (!m_isMoving && m_target != null) m_state.Possess();
        SetSprite();
    }
    public void CustomFixedUpdate()
    {
        SetPosition();
        SetEnergy();
        Request();
    }
    public void SetState(State state)
    {
        m_state = state;
    }
    public void ForceEject()
    {
        if (!m_isControlling) return;

        m_isControlling = false;
        SetState(new Soul(this));
        m_target.m_isTargeted = false;
        m_target = null;
    }

    private void SetPosition()
    {
        float xMin = Camera.main.GetBorders(gameObject).left;
        float xMax = Camera.main.GetBorders(gameObject).right;
        float yMin = Camera.main.GetBorders(gameObject).down;
        float yMax = Camera.main.GetBorders(gameObject).up;

        Vector2 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, xMin, xMax);
        viewPos.y = Mathf.Clamp(viewPos.y, yMin, yMax);
        transform.SetPosition(viewPos);

        if (m_isControlling)
        {
            transform.SetPosition(m_target.m_core);
            if (transform.position.x < xMin || transform.position.x > xMax || transform.position.y < yMin || transform.position.y > yMax)
            {
                ForceEject();
            }
        }
    }
    private void SetEnergy()
    {
        if (m_valueBar == null)
        {
            Debug.LogWarning("ValueBar for Soul hasn't been assigned!");
            return;
        }

        float maximum = 100f;
        float minimum = 20f;
        float consume = 3f;
        float regain = m_energy * 0.3f + 0.5f;
        // Consume
        if (m_isControlling && m_isMoving)
        {
            m_energy = m_energy > 0f ? m_energy - consume * Time.deltaTime : 0f;
        }
        // Regain
        if (!m_isControlling)
        {
            m_energy = m_energy < maximum ? m_energy + regain * Time.deltaTime : maximum;
        }
        // Is Tired?
        if (m_isControlling && m_energy < 0f) m_isTired = true;
        if (!m_isControlling && m_energy > minimum) m_isTired = false;
        // UI Visualize
        m_valueBar.fillAmount = m_energy * 0.01f;
        m_valueBar.color = m_energy <= minimum ? Color.red : Color.cyan;
    }
    private void SetSprite()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Light2D light = GetComponent<Light2D>();

        light.color = m_target != null ? m_target.GetComponent<SpriteRenderer>().color : sprite.color;
        sprite.enabled = !m_isControlling;
    }
    private void Request()
    {
        if (m_target == null || m_isTired) ForceEject();
        if (!m_isTired) m_state.Control();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Controllable"))
        {
            if (m_isControlling) return;

            m_target = collision.GetComponent<Controllable>();
            if (m_target != null)
            {
                m_target.m_isTargeted = true;
                m_others = FindObjectsOfType<Controllable>().Except(new Controllable[] { m_target }).ToArray();
                foreach (Controllable item in m_others) item.m_isTargeted = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Controllable"))
        {
            if (m_isControlling) return;

            if (m_target != null)
            {
                m_target.m_isTargeted = false;
                m_target = null;
            }
        }
    }
}

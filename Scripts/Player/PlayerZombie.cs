using UnityEngine;
using UnityEngine.UI;

public class PlayerZombie : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_jumpSpeed;
    [SerializeField] private Image m_valueBar;

    private Rigidbody2D m_rigidbody;
    private SpriteRenderer m_sprite;
    private Animator m_animator;

    private float m_moveDirection;
    private float m_stamina;
    private bool m_isTurn;

    private bool m_isGrounded;
    private bool m_isMoving;
    private bool m_isJumping;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();

        m_moveDirection = 0f;
        m_stamina = 100f;
        m_isTurn = false;

        if (m_moveSpeed == 0f) m_moveSpeed = 4.5f;
        if (m_jumpSpeed == 0f) m_jumpSpeed = 7.5f;
        m_isGrounded = m_isMoving = m_isJumping = false;
    }

    public void CustomUpdate()
    {
        GroundCheck();
        SetStamina();
        Jump();
    }
    public void CustomFixedUpdate()
    {
        Move();
    }

    private void GroundCheck()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, m_sprite.bounds.size.y * 0.5f + 0.2f, LayerMask.GetMask("Ground"));
        RaycastHit2D hitProperty = Physics2D.Raycast(transform.position, Vector2.down, m_sprite.bounds.size.y * 0.5f + 0.2f, LayerMask.GetMask("Property"));

        if (!m_isGrounded && (hitGround || hitProperty))
        {
            m_isGrounded = true;
            m_animator.SetBool("IsGrounded", m_isGrounded);
        }
        if (m_isGrounded && !(hitGround || hitProperty))
        {
            m_isGrounded = false;
            m_animator.SetBool("IsGrounded", m_isGrounded);
        }
    }
    private void SetStamina()
    {
        if (m_valueBar == null)
        {
            Debug.LogWarning("ValueBar for Zombie hasn't been assigned!");
            return;
        }

        float maximum = 100f;
        float minimum = 10f;
        float regain = 5f;
        // Set Stamina
        if (m_stamina < maximum) m_stamina += regain * Time.deltaTime;
        if (m_stamina > maximum) m_stamina = maximum;
        if (m_stamina < minimum) m_stamina = minimum;
        // UI Visualize
        m_valueBar.fillAmount = m_stamina * 0.01f;
        if (m_stamina <= 20f) m_valueBar.color = new Color(0.5f, 0.1f, 0.1f);
        else if (m_stamina <= 40f) m_valueBar.color = new Color(0.9f, 0.1f, 0.1f);
        else if (m_stamina <= 60f) m_valueBar.color = new Color(0.9f, 0.5f, 0.1f);
        else if (m_stamina <= 80f) m_valueBar.color = new Color(0.9f, 0.9f, 0.1f);
        else if (m_stamina <= 100f) m_valueBar.color = new Color(0.5f, 0.9f, 0.1f);
    }
    private void Move()
    {
        float input = Input.GetAxis("HorizontalZombie");
        float speed = m_moveSpeed * m_stamina * 0.01f;
        float airDrag = 0.7f;

        if (m_isGrounded)
        {
            m_moveDirection = input;
            m_isTurn = false;
        }
        else
        {
            if (input * m_moveDirection < 0f) m_isTurn = true;
            if (m_isTurn) speed = m_moveSpeed * m_stamina * 0.01f * airDrag;
        }
        transform.Translate(new Vector2(speed * m_moveDirection, 0f) * Time.fixedDeltaTime);

        m_isMoving = input != 0f;
        m_animator.SetBool("IsMoving", m_isMoving);

        if (input > 0) m_sprite.flipX = false;
        if (input < 0) m_sprite.flipX = true;
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            m_animator.SetTrigger("Jump");
            m_isGrounded = false;
            m_animator.SetBool("IsGrounded", m_isGrounded);
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, m_jumpSpeed);
        }

        m_isJumping = m_rigidbody.velocity.y > 0f;
        m_animator.SetFloat("FallSpeed", m_rigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float endurance = 10f;
        float consume;

        if (collision.relativeVelocity.magnitude > endurance)
        {
            consume = Mathf.FloorToInt(collision.relativeVelocity.magnitude) * 2f;

            m_animator.SetTrigger("Hurt");
            m_stamina -= consume;
        }
    }
}

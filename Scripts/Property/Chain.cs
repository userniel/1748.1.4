using UnityEngine;

public class Chain : MonoBehaviour
{
    public Rigidbody2D m_anchor;
    public Rigidbody2D m_weight;

    public bool m_isAnchorStatic;
    public int m_length;

    public bool m_isBreakable;
    public float m_distance;
    public int m_certain;

    private HingeJoint2D m_linkPrefab;
    private HingeJoint2D[] m_links;

    private void Start()
    {
        if (m_anchor == null) Debug.LogError("Anchor hasn't been assigned!");
        else if (m_isAnchorStatic) m_anchor.bodyType = RigidbodyType2D.Static;
        if (m_weight == null) Debug.LogError("Weight hasn't been assigned!");

        if (m_length < 2) m_length = 2;
        if (m_distance < 0) m_distance *= -1f;
        if (m_certain < 0) m_certain *= -1;

        m_linkPrefab = Resources.Load<HingeJoint2D>("Prefabs/Prop_Link");

        Generate();
    }
    private void Update()
    {
        if (m_isBreakable) Break();
    }

    private void Generate()
    {
        float linkHalfHeight = m_linkPrefab.GetComponent<SpriteRenderer>().size.y * 0.5f * m_linkPrefab.transform.lossyScale.y;

        m_links = new HingeJoint2D[m_length];
        for (int i = 0; i < m_links.Length; i++)
        {
            m_links[i] = Instantiate(m_linkPrefab, transform);
            m_links[i].transform.SetPositionY(m_anchor.position.y - linkHalfHeight * (i * 2f + 1f));
            m_links[i].useLimits = i != 0;
            m_links[i].connectedBody = i != 0 ? m_links[i - 1].GetComponent<Rigidbody2D>() : m_anchor;
        }

        FixedJoint2D joint = m_weight.gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = m_links[m_length - 1].GetComponent<Rigidbody2D>();
        m_weight.transform.SetPositionY(m_links[m_length - 1].transform.position.y - m_weight.GetComponent<SpriteRenderer>().bounds.size.y * 0.5f);
    }
    private void Break()
    {
        PlayerZombie playerZombie = FindObjectOfType<PlayerZombie>();

        if (playerZombie != null)
        {
            float distance = Vector2.Distance(playerZombie.transform.position * Vector2.right, transform.position * Vector2.right);
            if (distance <= m_distance) m_links[m_certain].enabled = false;
        }
    }
}

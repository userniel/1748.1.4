using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Stage : MonoBehaviour
{
    public float m_borderUp;
    public float m_borderDown;
    public float m_borderRight;
    public float m_borderLeft;

    [SerializeField] private Light2D m_globalLight;
}

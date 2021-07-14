using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public struct Dialogue
    {
        public string name;
        [TextArea(3, 10)]
        public string sentence;
    }

    public DialogueManager m_manager;
    public Dialogue[] m_dialogues;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            m_manager.Begin(this);
        }
    }
}

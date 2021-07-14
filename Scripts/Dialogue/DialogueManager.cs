using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject m_dialogueBox;
    public Text m_name;
    public Text m_dialogue;

    private bool m_isTalking;
    private Queue<string> m_names;
    private Queue<string> m_dialogues;

    private void Start()
    {
        m_isTalking = false;
        m_names = new Queue<string>();
        m_dialogues = new Queue<string>();
    }
    private void Update()
    {
        m_dialogueBox.SetActive(m_isTalking);
        if (m_isTalking && Input.GetKeyDown(KeyCode.G)) Next();
    }

    public void Begin(DialogueTrigger trigger)
    {
        m_isTalking = true;
        m_names.Clear();
        m_dialogues.Clear();
        foreach (var dialogue in trigger.m_dialogues)
        {
            m_names.Enqueue(dialogue.name);
            m_dialogues.Enqueue(dialogue.sentence);
        }
        Next();
    }
    public void Next()
    {
        if (m_dialogues.Count == 0)
        {
            End();
            return;
        }
        m_name.text = m_names.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Type(m_dialogues.Dequeue()));
    }
    public void End()
    {
        m_isTalking = false;
        Debug.Log("End of conversation.");
    }

    private IEnumerator Type(string sentence)
    {
        m_dialogue.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            m_dialogue.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
}

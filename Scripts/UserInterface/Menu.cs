using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [HideInInspector] public bool m_isChanged = false;

    [SerializeField] private Menu m_root;
    [SerializeField] private Selectable[] m_selectables;
    [SerializeField] private GameObject m_firstSelected;

    private int m_index = 0;
    private bool m_isKeyDown = false;

    private void Update()
    {
        SetFirstSelected(m_firstSelected);
        SetSelected();

        Cancel();
    }

    private void Cancel()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gameObject.name == "MenuPause") FindObjectOfType<Game>().isPaused = false;
            else if (m_root != null) Switch(m_root);
        }
    }
    private void Switch(Menu next)
    {
        // Enable next menu and disable current menu
        next.gameObject.SetActive(true);
        gameObject.SetActive(false);
        // Set "isChange" of next menu to true
        next.m_isChanged = true;
    }
    private void SetSelected()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (!m_isKeyDown)
            {
                if (Input.GetAxisRaw("Vertical") > 0) m_index = m_index > 0 ? m_index - 1 : m_selectables.Length - 1;
                if (Input.GetAxisRaw("Vertical") < 0) m_index = m_index < m_selectables.Length - 1 ? m_index + 1 : 0;
                EventSystem.current.SetSelectedGameObject(m_selectables[m_index].gameObject);
            }
            m_isKeyDown = true;
        }
        else m_isKeyDown = false;
    }
    private void SetFirstSelected(GameObject selected)
    {
        // Init EventSystem if menu is changed
        if (m_isChanged)
        {
            EventSystem.current.SetSelectedGameObject(null);
            m_index = 0;
            m_isChanged = false;
        }
        // Set first selected selectable item and current selected selectable item
        EventSystem.current.firstSelectedGameObject = selected;
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        }
        // If menu isn't changed then return
        if (!m_isChanged) return;
    }
}

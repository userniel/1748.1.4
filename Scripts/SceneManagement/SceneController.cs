using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject m_progress;
    [SerializeField] private Image m_bar;

    private SceneState m_state;
    private bool m_isDone;

    private void Awake()
    {
        SetState(new SceneMain(this));
    }
    private void Update()
    {
        // Show progress during loading
        m_progress.SetActive(!m_isDone);
        // Check if scene is still loading
        if (!m_isDone) return;
        // Notify new scene to start
        if (m_state != null) m_state.SceneUpdate();
    }

    public void SetState(SceneState state)
    {
        // Init isDone
        m_isDone = false;
        // Load the scene
        StartCoroutine(LoadSceneAsynchronous(state.Name));
        // Notify previous scene to end
        if (m_state != null) m_state.SceneEnd();
        // Set state
        m_state = state;
    }

    private IEnumerator LoadSceneAsynchronous(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            m_isDone = true;
            m_progress.SetActive(true);
            m_bar.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
    }
}

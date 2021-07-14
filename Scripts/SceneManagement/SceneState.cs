public abstract class SceneState
{
    protected SceneController m_sceneController;
    protected string m_sceneName;

    public SceneState(SceneController sceneController)
    {
        m_sceneController = sceneController;
        m_sceneName = "";
    }
    public virtual string Name => m_sceneName;

    public virtual void SceneUpdate() { }
    public virtual void SceneEnd() { }
}

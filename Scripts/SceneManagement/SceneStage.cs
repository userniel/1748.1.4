using UnityEngine;

public class SceneStage : SceneState
{
    private Game game = null;

    public SceneStage(SceneController sceneController, string sceneName) : base(sceneController)
    {
        m_sceneName = sceneName;
    }

    public override void SceneUpdate()
    {
        if (game != null) game.CustomUpdate();
        else game = Object.FindObjectOfType<Game>();
    }
    public override void SceneEnd()
    {
        if (game != null) game.CustomEnd();
        else game = Object.FindObjectOfType<Game>();
    }
}

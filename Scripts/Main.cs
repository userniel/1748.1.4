using UnityEngine;

public class Main : MonoBehaviour
{
    // Runs before a scene gets loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMain()
    {
        GameObject main = Instantiate(Resources.Load<GameObject>("Prefabs/_Main"));
        DontDestroyOnLoad(main);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

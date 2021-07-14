using UnityEngine;

public class Game : MonoBehaviour
{
    [HideInInspector] public bool isPaused = false;

    [SerializeField] private Transform m_startPointSoul;
    [SerializeField] private Transform m_startPointZombie;
    [SerializeField] private Transform m_finishPoint;
    [SerializeField] private GameObject m_pause;

    private PlayerSoul playerSoul;
    private PlayerZombie playerZombie;

    private void Awake()
    {
        playerSoul = Instantiate(Resources.Load<PlayerSoul>("Prefabs/Player_Soul"), m_startPointSoul.position, Quaternion.identity);
        playerZombie = Instantiate(Resources.Load<PlayerZombie>("Prefabs/Player_Zombie"), m_startPointZombie.position, Quaternion.identity);
    }
    private void FixedUpdate()
    {
        if (isPaused) return;

        if (playerSoul != null) playerSoul.CustomFixedUpdate();
        if (playerZombie != null) playerZombie.CustomFixedUpdate();
        Camera.main.Follow(playerZombie.gameObject, 3f);
    }

    public void CustomUpdate()
    {
        if (Input.GetButtonDown("Cancel")) isPaused = true;
        Time.timeScale = isPaused ? 0.0f : 1.0f;
        if (m_pause != null) m_pause.SetActive(isPaused);

        if (isPaused) return;

        if (playerSoul != null) playerSoul.CustomUpdate();
        if (playerZombie != null) playerZombie.CustomUpdate();
    }
    public void CustomEnd()
    {
        Destroy(playerSoul);
        Destroy(playerZombie);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int lifePoints = 1;
    [SerializeField] private float disablePlayerControlTime = 5.3f;

    [Header("Timeline Settings")]
    [SerializeField] private PlayableDirector mainTimeline;

    static private GameManager _instance;
    static public GameManager instance { get { return _instance; } }

    private bool m_isPlayerMoving = false;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {
        m_isPlayerMoving = false;

        Invoke("EnableControls", disablePlayerControlTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerHit()
    {
        lifePoints--;
        if (lifePoints <= 0)
        {
            PlayerDied();
        }
    }

    public void ObstacleDestroyed(int points)
    {
        Scoreboard.instance.IncreaseScore(points);
    }

    public bool isPlayerMoving()
    {
        return m_isPlayerMoving;
    }

    private void EnableControls()
    {
        m_isPlayerMoving = true;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (!player) { return; }

        var controls = player.GetComponent<PlayerInput>();
        if (!controls) { return; }

        controls.ActivateInput();
    }

    private void PlayerDied()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);

        m_isPlayerMoving = false;

        mainTimeline.time = 0;
        mainTimeline.Stop();

        Invoke("RestartLevel", 1);
    }


    private void RestartLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

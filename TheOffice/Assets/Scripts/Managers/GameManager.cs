using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus
{
    Menu,
    Pause,
    Playing,
    Won,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    public GameStatus CurrentStatus { get { return currentGameStatus; } }

    [SerializeField] private GameStatus currentGameStatus;

    [Header("References")]
    [SerializeField] private PlayerController _player;

    [Header("Level Properties")]
    [SerializeField] private int scoreToPassLevel = 500;
    [SerializeField] private int requiredItemsToDestroy = 10;
    private int _currentPlayerScore = 0;
    private int _destroyedItems = 0;

    private bool _isPaused = false;
    private static GameManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        Time.timeScale = 1;
        currentGameStatus = GameStatus.Playing;
        UserInterfaceManager.Instance.UpdateDestroyCount(0, requiredItemsToDestroy);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                Unpause();
            }
            else
            {
                _isPaused = true;
                UserInterfaceManager.Instance.ShowPauseCanvas();
                PauseGame();
            }
        }
    }
    
    public void UpdateScore(int score)
    {
        _currentPlayerScore += score;
        _destroyedItems++;
        UserInterfaceManager.Instance.UpdateDestroyCount(_destroyedItems, requiredItemsToDestroy);
        if (_destroyedItems >= requiredItemsToDestroy)
            UpdateStatus(GameStatus.Won);
    }

    public void PlayerHasBeenCaught()
    {
        UpdateStatus(GameStatus.GameOver);
    }

    private void UpdateStatus(GameStatus status)
    {
        currentGameStatus = status;
        // Handle different status 
        switch (currentGameStatus)
        {
            case GameStatus.Menu:
                break;
            case GameStatus.Pause:
                break;
            case GameStatus.Playing:
                break;
            case GameStatus.Won:
                {
                    PauseGame();
                    UserInterfaceManager.Instance.ShowWonCanvas();
                }
                break;
            case GameStatus.GameOver:
                {
                    PauseGame();
                    UserInterfaceManager.Instance.ShowLostCanvas();
                }
                break;
        }
    }

    public void RestartLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UpdateStatus(GameStatus.Playing);
    }

    private void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        _player.DisableAudio();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UserInterfaceManager.Instance.ShowPlayerCanvas();
        _player.EnableAudio();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Unpause()
    {
        ResumeGame();
        _isPaused = false;
    }

    public void LoadNextLevel()
    {
        int nextSceneCount = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneCount < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextSceneCount);
        else
            SceneManager.LoadScene(0);
    }

    public bool IsPlaying ()
    {
        return GameManager.Instance.CurrentStatus == GameStatus.Playing;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

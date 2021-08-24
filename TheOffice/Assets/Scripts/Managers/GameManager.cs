using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private GameStatus currentGameStatus;   

    [Header("References")]
    [SerializeField] private PlayerController _player;

    [Header("Level Properties")]
    [SerializeField] private int scoreToPassLevel = 500;
    private int _currentPlayerScore;

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
    // 4. Talk to update UI 
    // 5. Make 

    // 1. Make progression manager
    // 2. Some way to keep track of score


    public void UpdateScore(int score)
    {
        Debug.Log("Increased score by " + score);
        _currentPlayerScore += score;
        if (_currentPlayerScore >= scoreToPassLevel)
            UpdateStatus(GameStatus.Won);
    }

    // 3. Level and game status
    private void UpdateStatus(GameStatus status)
    {
        currentGameStatus = status;
        // Handle different status 
        switch(currentGameStatus)
        {
            case GameStatus.Menu:
                break;
            case GameStatus.Pause:
                break;
            case GameStatus.Playing:
                break;
            case GameStatus.Won:
                LoadNextLevel();
                break;
            case GameStatus.GameOver:
                break;
        }
    }

    private void LoadNextLevel()
    {
        Debug.Log("Won level, loading next!");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    public int currentLevelIndex = 0;
    public int currentScore = 0;
    public int highScore = 0;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        Scene current = SceneManager.GetActiveScene();
        if (current.buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadHighScore();
    }

    // SCORE MANAGEMENT
    public void AddScore(int points)
    {
        currentScore += points;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
        }

        Debug.Log($"Score: {currentScore} | High Score: {highScore}");
    }

    //SAVE / LOAD
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // LEVEL CONTROL
    public void LoadNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;

        Debug.Log($"Trying to load next level: {nextLevelIndex}");

        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            currentLevelIndex = nextLevelIndex;
            SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            Debug.Log("All levels complete!");
        }
    }

    public void RestartLevel()
    {
        Debug.Log($"Restarting Level: {currentLevelIndex}");
        SceneManager.LoadScene(currentLevelIndex);
    }
    
    
    public void LoadMainMenu()
    {
        currentLevelIndex = 0;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}

    

    

using UnityEditor.AssetImporters;
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
        currentLevelIndex = nextLevelIndex;

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

    public void CheckLevelComplete()
    {
        // Assuming you have an EnemyManager or a way to get all enemies
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        if (enemies.Length == 0)
        {
            // Delay a little if you want (optional)
            Debug.Log("Level Complete! Loading next level...");
            LoadNextLevel();
        }
    }

    public void CheckGameOver(int remainingBirds)
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        if (remainingBirds <= 0 && enemies.Length > 0 )
        {
            Debug.Log("Game Over!");

            new WaitForSeconds(7);

            SceneManager.LoadScene("GameOverMenu");
            currentScore = 0;
            
        }
    }

}





using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartLevel()
    {
        if (GameManager.Instance.lastPlayedLevel != 0)
        {
            Debug.Log($"Restarting level: {GameManager.Instance.lastPlayedLevel}");
            SceneManager.LoadScene(GameManager.Instance.lastPlayedLevel);
        }
        else
        {
            Debug.LogWarning("No last level saved! Returning to main menu instead.");
            SceneManager.LoadScene(0);
        }
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to main menu...");
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

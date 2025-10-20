using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private GameObject menuPanel;

    public void StartGame()
    {
        Debug.Log("Starting Game - Loading Level 1");
        SceneManager.LoadScene(1); // Level1 is usually index 1
    }

    public void OpenLevelSelect()
    {
        Debug.Log("Opening Level Select");
        levelSelectPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void CloseLevelSelect()
    {
        menuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();

        // Note: Quit() won't close the game in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void LoadLevel(int index)
    {
        Debug.Log($"Loading Level {index}");
        SceneManager.LoadScene(index);
    }
}

using UnityEngine;

public class DebugControls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("DEBUG: Loading next level manually!");
            GameManager.Instance.LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("DEBUG: Restarting current level!");
            GameManager.Instance.RestartLevel();
        }
    }
}

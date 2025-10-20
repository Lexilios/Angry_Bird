using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int totalEnemies;

    void Start()
    {
        totalEnemies = FindObjectsOfType<Enemy>().Length;
    }

    public void OnEnemyDestroyed()
    {
        totalEnemies--;

        Debug.Log("enemies left: " + totalEnemies);

        if (totalEnemies <= 0)
        {
            Debug.Log("Level complete!");
            GameManager.Instance.LoadNextLevel();
        }
    }

    private void NextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }
}

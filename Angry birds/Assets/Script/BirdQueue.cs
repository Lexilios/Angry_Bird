using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdQueue : MonoBehaviour
{
    [Header("Bird Setup")]
    public List<GameObject> birdPrefabs; // List of bird prefabs (Red, Blue, Yellow, etc.)
    public Transform spawnPoint;          // Where the next bird sits in the slingshot
    public Slingshot slingshot;           // Reference to your Slingshot script

    private Queue<GameObject> birds = new Queue<GameObject>();

    //public UnityEvent<GameObject> LouisNextBird; 

    void Start()
    {
        // Load all prefabs into a queue
        foreach (var birdPrefab in birdPrefabs)
            birds.Enqueue(birdPrefab);

        LoadNextBird();

    }

    public void LoadNextBird()
    {

        if (birds.Count == 0)
        {
            Debug.Log("No more birds left!");
            slingshot.currentBird = null;
            return;
        }

        GameObject birdObj = Instantiate(birds.Dequeue(), spawnPoint.position, Quaternion.identity);
        Bird bird = birdObj.GetComponent<Bird>();
        slingshot.currentBird = bird;
        //LouisNextBird.Invoke(birdObj);
        Debug.Log($"Loaded new bird: {bird.name}");
    }

    public void OnBirdLaunched()
    {
        // Wait a short moment before loading the next bird
        Invoke(nameof(LoadNextBird), 2f);
    }
}

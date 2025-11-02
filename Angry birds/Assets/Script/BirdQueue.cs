using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BirdQueue : MonoBehaviour
{
    [System.Serializable]
    public class BirdEntry
    {
        public Bird birdPrefab;
        public int count = 1;
        public Sprite icon;
    }

    public List<BirdEntry> birdEntries = new List<BirdEntry>();
    public Transform slingshotSpawn;
    public Slingshot slingshot;
    public GameObject buttonPrefab;
    public Transform buttonPanel;

    private Dictionary<BirdEntry, int> remainingBirds = new();
    private Dictionary<BirdEntry, TextMeshProUGUI> countLabels = new();
    private BirdEntry selectedBirdType;

    void Start()
    {
        foreach (var entry in birdEntries)
            remainingBirds[entry] = entry.count;

        StartCoroutine(InitUI());
    }


    IEnumerator InitUI()
    {
        yield return null; // wait one frame so UI system is ready
        foreach (var entry in birdEntries)
            remainingBirds[entry] = entry.count;

        CreateBirdButtons();
    }
    void CreateBirdButtons()
    {
        foreach (var entry in birdEntries)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, buttonPanel);
            Button button = buttonObj.GetComponent<Button>();
            Image icon = buttonObj.GetComponent<Image>();
            TextMeshProUGUI countText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

            icon.sprite = entry.icon;
            countText.text = $"x{remainingBirds[entry]}";

            // store reference to this label for later updates
            countLabels[entry] = countText;

            button.onClick.AddListener(() => OnBirdSelected(entry));
        }
    }

    void OnBirdSelected(BirdEntry entry)
    {
        // Don’t allow selecting empty types
        if (remainingBirds[entry] <= 0)
        {
            Debug.Log($"{entry.birdPrefab.name} is out of birds!");
            return;
        }

        // If there’s already a bird loaded, remove it (without consuming ammo)
        if (slingshot.currentBird != null)
        {
            Destroy(slingshot.currentBird.gameObject);
            slingshot.currentBird = null;
        }

        // Set new selection
        selectedBirdType = entry;

        // Spawn new bird
        LoadNextBird(entry);
    }

    void LoadNextBird(BirdEntry entry)
    {
        if (remainingBirds[entry] <= 0)
            return;

        Bird newBird = Instantiate(entry.birdPrefab, slingshotSpawn.position, Quaternion.identity);
        newBird.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        slingshot.currentBird = newBird;

        Debug.Log($"Loaded bird: {entry.birdPrefab.name}");
    }

    public void OnBirdLaunched()
    {
        if (selectedBirdType == null)
            return;

        // consume one bird
        remainingBirds[selectedBirdType]--;

        // update only that bird’s counter
        if (countLabels.TryGetValue(selectedBirdType, out TextMeshProUGUI label))
        {
            label.text = $"x{remainingBirds[selectedBirdType]}";
        }

        slingshot.currentBird = null;
    }
}

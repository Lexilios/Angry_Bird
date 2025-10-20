using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public float maxStretch = 3f;
    public float launchPower = 10f;
    public Bird currentBird;
    public BirdQueue birdQueue;

    private Vector2 startPos;
    private bool isDragging = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (currentBird == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

            if (hit != null && hit.gameObject == currentBird.gameObject)
            {
                isDragging = true;
                currentBird.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;

            Vector2 releaseDir = (Vector2)startPos - (Vector2)currentBird.transform.position;
            currentBird.Launch(releaseDir, launchPower);

            // Notify the queue to load the next bird
            birdQueue.OnBirdLaunched();
        }

        if (isDragging && currentBird != null)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = mouseWorldPos - startPos;

            if (dragVector.magnitude > maxStretch)
                dragVector = dragVector.normalized * maxStretch;

            currentBird.transform.position = startPos + dragVector;
        }
    }
}

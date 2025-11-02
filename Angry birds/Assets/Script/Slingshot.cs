using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Slingshot : MonoBehaviour
{
    public float maxStretch = 3f;
    public float launchPower = 10f;
    public Bird currentBird;
    public BirdQueue birdQueue;
    public int trajectoryLength = 10;  

    private Vector2 startPos;
    private bool isDragging = false;
    private LineRenderer lineRenderer;
    private Rigidbody2D birdRb;

    void Start()
    {
        startPos = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (currentBird == null)
            return;

        birdRb = currentBird.GetComponent<Rigidbody2D>();

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

            if (hit != null && hit.gameObject == currentBird.gameObject)
            {
                isDragging = true;
                birdRb.bodyType = RigidbodyType2D.Kinematic;
                lineRenderer.enabled = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            lineRenderer.enabled = false;

            Vector2 releaseDir = (Vector2)startPos - (Vector2)currentBird.transform.position;
            currentBird.Launch(releaseDir, launchPower);

            birdQueue.OnBirdLaunched();
        }

        if (isDragging && currentBird != null)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = mouseWorldPos - startPos;

            if (dragVector.magnitude > maxStretch)
                dragVector = dragVector.normalized * maxStretch;

            currentBird.transform.position = startPos + dragVector;

            DrawTrajectory((Vector2)startPos - (Vector2)currentBird.transform.position);
        }
    }

    void DrawTrajectory(Vector2 launchDirection)
    {
        // This velocity should match AddForce(direction * launchPower, ForceMode2D.Impulse)
        Vector2 launchVelocity = launchDirection * (launchPower / birdRb.mass);


        Vector2 startPoint = currentBird.transform.position;
        Vector3[] points = new Vector3[trajectoryLength];

        for (int i = 0; i < trajectoryLength; i++)
        {
            float t = i * 0.1f; // time step
            Vector2 position = startPoint + launchVelocity * t + 0.5f * Physics2D.gravity * (t * t);
            points[i] = position;
        }

        lineRenderer.positionCount = trajectoryLength;
        lineRenderer.SetPositions(points);
    }

}

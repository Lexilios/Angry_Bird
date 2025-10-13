using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public float maxStretch = 3f;
    public float launchPower = 10f;

    private Rigidbody2D rb;
    private Vector2 startPos;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;

        // Prevent falling at start
        rb.gravityScale = 0;
    }

    void Update()
    {
        // Detect mouse down
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

            if (hit != null && hit.gameObject == gameObject)
            {
                Debug.Log("Clicked!");
                isDragging = true;
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        // Detect mouse release
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Debug.Log("Released!");
            isDragging = false;
            rb.bodyType = RigidbodyType2D.Dynamic;

            // Turn gravity back on when launched
            rb.gravityScale = 3;

            Vector2 releaseDir = startPos - rb.position;
            rb.linearVelocity = releaseDir * launchPower;
        }

        // Handle dragging
        if (isDragging)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = mouseWorldPos - startPos;

            if (dragVector.magnitude > maxStretch)
                dragVector = dragVector.normalized * maxStretch;

            rb.position = startPos + dragVector;
        }
    }
}

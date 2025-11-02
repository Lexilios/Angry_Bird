using UnityEngine;
public class BlueBird : Bird
{
    public GameObject miniBirdPrefab;   // Assign in Inspector
    public float splitForce = 5f;
    private bool hasSplit = false;

    protected override void ActivateAbility()
    {
        if (hasSplit || miniBirdPrefab == null)
            return;

        hasSplit = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 currentVelocity = rb.linearVelocity; // use velocity instead of linearVelocity for clarity
        Vector3 pos = transform.position;

        // Spawn mini-birds
        GameObject bird1 = Instantiate(miniBirdPrefab, pos, Quaternion.identity);
        GameObject bird2 = Instantiate(miniBirdPrefab, pos, Quaternion.identity);

        Rigidbody2D rb1 = bird1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = bird2.GetComponent<Rigidbody2D>();

        // Calculate split angles
        float angleOffset = 30f; // degrees to left/right from main velocity
        Vector2 dir1 = RotateVector(currentVelocity.normalized, angleOffset) * currentVelocity.magnitude;
        Vector2 dir2 = RotateVector(currentVelocity.normalized, -angleOffset) * currentVelocity.magnitude;

        rb1.linearVelocity = dir1;
        rb2.linearVelocity = dir2;

        // Copy damping
        rb1.linearDamping = rb.linearDamping;
        rb2.linearDamping = rb.linearDamping;

        Debug.Log("Blue bird split into three!");
    }

    // Helper function to rotate a 2D vector by degrees
    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }

}


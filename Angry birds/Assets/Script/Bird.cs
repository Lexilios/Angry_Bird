using UnityEngine;

public class Bird : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected bool isLaunched = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    public virtual void Launch(Vector2 direction, float force)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
        rb.linearVelocity = direction * force;
        isLaunched = true;
    }

    protected virtual void ActivateAbility()
    {
        // Empty: subclasses override this
    }

    protected virtual void Update()
    {
        if (isLaunched && Input.GetMouseButtonDown(0))
        {
            ActivateAbility();
        }
    }
}

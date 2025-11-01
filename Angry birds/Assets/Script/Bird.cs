using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected bool isLaunched = false;
    protected bool abilityUsed = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero; 
        rb.angularVelocity = 0;
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
        if (isLaunched && !abilityUsed && Input.GetMouseButtonDown(0))
        {
            ActivateAbility();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BreakableBloc>())
        {
            var bloc = collision.gameObject.GetComponent<BreakableBloc>();
            bloc.TakeDamage(rb.linearVelocity.magnitude);
        }
    }
}

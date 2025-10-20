using JetBrains.Annotations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class BlackBird : Bird
{
    public float rangeOfImpact = 5f;
    public float force = 10f;

    [ContextMenu("Kaboom")]
    protected override void ActivateAbility()
    {
        // Empty: subclasses override this
        if (rb.linearVelocity.magnitude < 1f) return;

        Collider2D[] allCollider = Physics2D.OverlapCircleAll(rb.position, rangeOfImpact);
        foreach (Collider2D col in allCollider)
        {
            Rigidbody2D body;
            if (col.TryGetComponent<Rigidbody2D>(out body))
            {
                if (body == rb) continue;

                Vector2 explosionCenterToObject = body.position - rb.position;
                body.AddForce(explosionCenterToObject.normalized * force, ForceMode2D.Impulse);

                var bloc = col.gameObject.GetComponent<BreakableBloc>();
                if(bloc != null) bloc.TakeDamage(force);

            }
        }
        abilityUsed = true;
    }
}

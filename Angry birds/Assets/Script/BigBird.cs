using UnityEngine;

public class BigBird : Bird
{
    protected override void ActivateAbility()
    {
        transform.localScale *= 2f;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.mass *= 4f;
        }

        abilityUsed = true;
        Debug.Log("BigBird SMASH!");
    }
}
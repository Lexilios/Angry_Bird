using UnityEngine;

public class YellowBird : Bird
{
    public float speedBoost = 2f;

    protected override void ActivateAbility()
    {
        rb.linearVelocity *= speedBoost;
        abilityUsed = true;
        Debug.Log("Yellow bird speed boost activated!");
    }
}

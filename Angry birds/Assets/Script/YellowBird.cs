using UnityEngine;

public class YellowBird : Bird
{
    public float speedBoost = 2f;
    private bool abilityUsed = false;

    protected override void Update()
    {
        base.Update();

        if (isLaunched && !abilityUsed && Input.GetMouseButtonDown(0))
        {
            ActivateAbility();
        }
    }

    protected override void ActivateAbility()
    {
        rb.linearVelocity *= speedBoost;
        abilityUsed = true;
        Debug.Log("Yellow bird speed boost activated!");
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float minImpactToDamage = 2f;   // Minimum relative velocity to start taking damage
    public float damageMultiplier = 10f;   // Scales how much damage force causes
    public int scoreValue = 500;

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public Color damageFlashColor = Color.red;
    public float flashDuration = 0.1f;

    private float currentHealth;
    private Color originalColor;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        // Get the relative impact strength
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce >= minImpactToDamage)
        {
            float damage = (impactForce - minImpactToDamage) * damageMultiplier;
            TakeDamage(damage);
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashDamage());

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private System.Collections.IEnumerator FlashDamage()
    {
        spriteRenderer.color = damageFlashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} was destroyed!");

        // Add score
        GameManager.Instance.AddScore(scoreValue);

        // Notify LevelController
        LevelController levelController = FindObjectOfType<LevelController>();
        if (levelController != null)
            levelController.OnEnemyDestroyed();

        // Destroy enemy
        Destroy(gameObject);
    }
}

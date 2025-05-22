using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Respawn")]
    private Transform currentRespawnPoint;
    private Rigidbody2D rb;

    public int CurrentHealth => currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentRespawnPoint = transform; // Initial spawn point
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log("Dégâts subis. Vie restante : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Le joueur est mort.");
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;

        // Désactive temporairement le mouvement
        if (TryGetComponent(out PlayerMovement movement))
            movement.Capture();

        // Optionnel : petite pause avant respawn
        Invoke(nameof(Respawn), 1f);
    }

    private void Respawn()
    {
        Debug.Log("Respawn à : " + currentRespawnPoint.position);

        currentHealth = maxHealth;
        transform.position = currentRespawnPoint.position;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.isKinematic = false;
        }

        if (TryGetComponent(out PlayerMovement movement))
            movement.Release();
    }

    public void SetRespawnPoint(Transform newPoint)
    {
        currentRespawnPoint = newPoint;
        Debug.Log("Nouveau checkpoint défini : " + currentRespawnPoint.position);
    }

    void Update()
    {
        if (transform.position.y < -10f && currentHealth > 0)
        {
            TakeDamage(maxHealth); // Tomber dans le vide = mort instantanée
        }
    }
}
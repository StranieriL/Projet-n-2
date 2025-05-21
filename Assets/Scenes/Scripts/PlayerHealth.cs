using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private Transform currentRespawnPoint;
    private Rigidbody2D rb;

    public int CurrentHealth => currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        currentRespawnPoint = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Dégâts subis. Vie restante : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Le joueur a été tué.");
        if (rb != null) rb.isKinematic = true;

        Respawn();
    }

    void Respawn()
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
    }

    public void SetRespawnPoint(Transform newPoint)
    {
        currentRespawnPoint = newPoint;
        Debug.Log("Nouveau checkpoint : " + currentRespawnPoint.position);
    }

    void Update()
    {
        if (transform.position.y < -10f)
        {
            TakeDamage(maxHealth);
        }
    }
}
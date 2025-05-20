using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private Transform currentRespawnPoint;

    void Start()
    {
        currentHealth = maxHealth;
        currentRespawnPoint = transform; // point de d�part
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("D�g�ts subis. Vie restante : " + currentHealth);

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }
   
    void Respawn()
    {
        Debug.Log("Respawn � : " + currentRespawnPoint.position);
        currentHealth = maxHealth;
        transform.position = currentRespawnPoint.position;
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
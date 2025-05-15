using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 1f;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Déplacement gauche/droite
        float moveX = Input.GetAxis("Horizontal"); // Gauche (-1) à Droite (+1)

        Vector3 movement = new Vector3(moveX, 0f, 0f); // Seulement sur l'axe X

        transform.position += movement * speed * Time.deltaTime;

        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }
    // Détecter si le joueur touche le sol
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
public class FallDetector : MonoBehaviour
{
    public float deathY = -6f; // Hauteur de chute mortelle

    void Update()
    {
        if (transform.position.y < deathY)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Le joueur est tombé !");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recharge la scène actuelle
    }
}

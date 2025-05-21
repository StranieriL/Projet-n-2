using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 1f;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isCaptured = false; // Si le joueur est capturé
    private Transform player; // Joueur capturé

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isCaptured)
            return;  // Si le joueur est capturé, ne rien faire

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

    // Méthode pour capturer le joueur
    public void Capture()
    {
        isCaptured = true;
        rb.isKinematic = true; // Désactive la physique
    }

    // Méthode pour libérer le joueur
    public void Release()
    {
        isCaptured = false;
        rb.isKinematic = false; // Réactive la physique
    }
}
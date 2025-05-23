using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Mouvement")]
    public float speed = 2f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isCaptured = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isCaptured) return;

        // Mouvement horizontal
        float moveX = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(moveX, 0f, 0f);
        transform.position += move * speed * Time.deltaTime;

        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                break;
            }
        }
    }

    // Appelé quand le joueur est attrapé par un piège
    public void Capture()
    {
        isCaptured = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }

    // Appelé après un piège ou respawn
    public void Release()
    {
        isCaptured = false;
        rb.isKinematic = false;
    }
}
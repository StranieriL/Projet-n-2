using UnityEngine;

public class DeathBySpider : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D body;
    public Vector2 respawn;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = respawn;
            body.velocity = Vector2.zero;
        }

    }
}

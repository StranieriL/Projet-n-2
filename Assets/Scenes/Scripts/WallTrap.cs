using UnityEngine;

public class WallEnemyTrap : MonoBehaviour
{
    public Transform enemy;           // L'objet ennemi à faire sortir
    public Vector2 targetOffset = new Vector2(2f, 0); // Vers où il sort (par rapport à sa position de base)
    public float speed = 5f;
    public bool returnAfterAttack = false;
    public float returnDelay = 2f;

    private Vector2 initialPosition;
    private Vector2 targetPosition;
    private bool isActivated = false;
    private bool isReturning = false;

    void Start()
    {
        initialPosition = enemy.position;
        targetPosition = initialPosition + targetOffset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            isActivated = true;
            StartCoroutine(MoveToTarget(targetPosition));

            if (returnAfterAttack)
            {
                Invoke(nameof(ReturnToWall), returnDelay);
            }
        }
    }

    private System.Collections.IEnumerator MoveToTarget(Vector2 destination)
    {
        while ((Vector2)enemy.position != destination)
        {
            enemy.position = Vector2.MoveTowards(enemy.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void ReturnToWall()
    {
        StartCoroutine(MoveToTarget(initialPosition));
        isActivated = false;
    }
}

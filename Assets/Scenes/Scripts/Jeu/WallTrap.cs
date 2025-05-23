using UnityEngine;
using System.Collections;

public class WallEnemyTrap : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Transform enemy;                      // Le monstre piégé dans le mur
    public Vector2 targetOffset = new Vector2(2f, 0); // Direction de sortie
    public float speed = 5f;                     // Vitesse de déplacement
    public float returnDelay = 2f;               // Délai avant retour dans le mur

    private Vector2 initialPosition;
    private Vector2 targetPosition;
    private Coroutine moveCoroutine;

    private Transform player;
    private bool isActivated = false;
    private bool playerCaptured = false;

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
            moveCoroutine = StartCoroutine(MoveToTarget(targetPosition, false));
        }
    }

    public bool IsPlayerCaptured()
    {
        return playerCaptured;
    }

    public void CapturePlayer(Transform target)
    {
        if (playerCaptured) return;

        player = target;
        playerCaptured = true;

        if (player.TryGetComponent(out PlayerMovement movement))
            movement.Capture();

        player.SetParent(enemy);

        if (player.TryGetComponent(out Rigidbody2D rb))
            rb.isKinematic = true;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveToTarget(initialPosition, true));
    }

    private IEnumerator MoveToTarget(Vector2 destination, bool isReturning)
    {
        while (Vector2.Distance(enemy.position, destination) > 0.01f)
        {
            Vector2 nextPosition = Vector2.MoveTowards(enemy.position, destination, speed * Time.deltaTime);
            enemy.position = nextPosition;

            if (playerCaptured && player != null)
                player.position = enemy.position;

            yield return null;
        }

        enemy.position = destination;

        if (isReturning && playerCaptured)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        if (player != null)
        {
            if (player.TryGetComponent(out PlayerHealth health))
                health.Die();

            player.SetParent(null);

            if (player.TryGetComponent(out Rigidbody2D rb))
                rb.isKinematic = false;

            if (player.TryGetComponent(out PlayerMovement movement))
                movement.Release();

            player = null;
            playerCaptured = false;
        }
    }
}
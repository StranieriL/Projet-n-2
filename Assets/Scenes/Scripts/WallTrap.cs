using UnityEngine;
using System.Collections;

public class WallEnemyTrap : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Transform enemy;                     // L'objet ennemi qui sort du mur
    public Vector2 targetOffset = new Vector2(2f, 0); // Déplacement de l'ennemi
    public float speed = 5f;                    // Vitesse de déplacement
    public bool returnAfterAttack = true;       // Revenir dans le mur après attaque
    public float returnDelay = 2f;              // Délai avant le retour

    private Vector2 initialPosition;
    private Vector2 targetPosition;
    private bool isActivated = false;

    private Transform player;                   // Référence au joueur attrapé
    private bool playerCaptured = false;
    private Coroutine moveCoroutine;

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
            player = other.transform;
            playerCaptured = true;

            // Capturer le joueur (désactiver son mouvement et sa physique)
            player.GetComponent<PlayerMovement>().Capture();

            // Fixer le joueur au piège (le joueur suit maintenant le piège)
            player.SetParent(enemy);

            // Désactiver la physique du joueur pour qu'il ne soit pas poussé par le piège
            var playerRigidbody = player.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.isKinematic = true; // Le joueur ne sera pas affecté par la physique
            }

            // Lancer la sortie du mur
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);

            moveCoroutine = StartCoroutine(MoveToTarget(targetPosition, false));

            if (returnAfterAttack)
            {
                Invoke(nameof(ReturnToWall), returnDelay);
            }
        }
    }

    private IEnumerator MoveToTarget(Vector2 destination, bool isReturning)
    {
        // Déplace l'ennemi et le joueur
        while (Vector2.Distance(enemy.position, destination) > 0.01f)
        {
            Vector2 nextPosition = Vector2.MoveTowards(enemy.position, destination, speed * Time.deltaTime);
            Vector2 offset = (Vector2)enemy.position - nextPosition;

            enemy.position = nextPosition;

            if (playerCaptured && player != null)
            {
                // Déplacer le joueur avec l'ennemi (il suit le piège)
                player.position -= (Vector3)offset;
            }

            yield return null;
        }

        // Arrivée au mur
        enemy.position = destination;

        // Si on est en train de revenir au mur, tuer le joueur après être arrivé à destination
        if (isReturning && playerCaptured)
        {
            KillPlayer();
        }
    }

    private void ReturnToWall()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveToTarget(initialPosition, true));
        isActivated = false;

        // Défixer le joueur du piège après être revenu dans le mur
        if (playerCaptured && player != null)
        {
            player.SetParent(null); // Le joueur n'est plus attaché au piège

            // Réactiver la physique du joueur
            var playerRigidbody = player.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.isKinematic = false; // Le joueur peut maintenant être affecté par la physique
            }

            // Libérer le joueur
            player.GetComponent<PlayerMovement>().Release();
        }
    }

    private void KillPlayer()
    {
        if (player != null)
        {
            // Utiliser ton système de santé/respawn
            var health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Die(); // Assure-toi que Die() appelle bien le respawn
            }
            else
            {
                Debug.LogWarning("Pas de script PlayerHealth trouvé pour gérer le respawn !");
            }

            player = null;
            playerCaptured = false;
        }
    }
}
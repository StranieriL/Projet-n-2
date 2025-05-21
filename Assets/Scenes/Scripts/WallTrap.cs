using UnityEngine;
using System.Collections;

public class WallEnemyTrap : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Transform enemy;                     // L'objet ennemi qui sort du mur
    public Vector2 targetOffset = new Vector2(2f, 0); // D�placement de l'ennemi
    public float speed = 5f;                    // Vitesse de d�placement
    public bool returnAfterAttack = true;       // Revenir dans le mur apr�s attaque
    public float returnDelay = 2f;              // D�lai avant le retour

    private Vector2 initialPosition;
    private Vector2 targetPosition;
    private bool isActivated = false;

    private Transform player;                   // R�f�rence au joueur attrap�
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

            // Capturer le joueur (d�sactiver son mouvement et sa physique)
            player.GetComponent<PlayerMovement>().Capture();

            // Fixer le joueur au pi�ge (le joueur suit maintenant le pi�ge)
            player.SetParent(enemy);

            // D�sactiver la physique du joueur pour qu'il ne soit pas pouss� par le pi�ge
            var playerRigidbody = player.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.isKinematic = true; // Le joueur ne sera pas affect� par la physique
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
        // D�place l'ennemi et le joueur
        while (Vector2.Distance(enemy.position, destination) > 0.01f)
        {
            Vector2 nextPosition = Vector2.MoveTowards(enemy.position, destination, speed * Time.deltaTime);
            Vector2 offset = (Vector2)enemy.position - nextPosition;

            enemy.position = nextPosition;

            if (playerCaptured && player != null)
            {
                // D�placer le joueur avec l'ennemi (il suit le pi�ge)
                player.position -= (Vector3)offset;
            }

            yield return null;
        }

        // Arriv�e au mur
        enemy.position = destination;

        // Si on est en train de revenir au mur, tuer le joueur apr�s �tre arriv� � destination
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

        // D�fixer le joueur du pi�ge apr�s �tre revenu dans le mur
        if (playerCaptured && player != null)
        {
            player.SetParent(null); // Le joueur n'est plus attach� au pi�ge

            // R�activer la physique du joueur
            var playerRigidbody = player.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.isKinematic = false; // Le joueur peut maintenant �tre affect� par la physique
            }

            // Lib�rer le joueur
            player.GetComponent<PlayerMovement>().Release();
        }
    }

    private void KillPlayer()
    {
        if (player != null)
        {
            // Utiliser ton syst�me de sant�/respawn
            var health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Die(); // Assure-toi que Die() appelle bien le respawn
            }
            else
            {
                Debug.LogWarning("Pas de script PlayerHealth trouv� pour g�rer le respawn !");
            }

            player = null;
            playerCaptured = false;
        }
    }
}
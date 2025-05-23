using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;        // Point de départ de la patrouille
    public Transform pointB;        // Point d’arrivée de la patrouille
    public float speed = 1f;        // Vitesse de déplacement

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = pointB.position;
    }

    void Update()
    {
        // Déplacement vers la position cible
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si l'ennemi est arrivé à la cible, on inverse la direction
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;

            // Retourne le sprite en X (si nécessaire)
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
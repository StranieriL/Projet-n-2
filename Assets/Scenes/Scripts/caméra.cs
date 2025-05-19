using UnityEngine;

public class CameraFollowWithZoneTriggers : MonoBehaviour
{
    public Transform player;            // Référence au joueur
    public float moveSpeed = 2f;        // Vitesse de déplacement vertical

    private float targetY;              // Hauteur vers laquelle on se dirige
    private bool isMovingY = false;     // Est-ce qu'on est en train de bouger verticalement ?

    void Start()
    {
        // Hauteur initiale
        targetY = transform.position.y;
    }

    void Update()
    {
        Vector3 camPos = transform.position;

        // Suivre le joueur sur l'axe X
        camPos.x = player.position.x;

        // Si un mouvement vertical est en cours, avancer vers targetY
        if (isMovingY)
        {
            camPos.y = Mathf.MoveTowards(transform.position.y, targetY, moveSpeed * Time.deltaTime);

            // Quand la position cible est atteinte, arrêter le mouvement
            if (Mathf.Approximately(camPos.y, targetY))
            {
                isMovingY = false;
            }
        }

        transform.position = new Vector3(camPos.x, camPos.y, transform.position.z);
    }

    // Méthode appelée par une zone pour déclencher le déplacement vers un Y spécifique
    public void MoveCameraToY(float newY)
    {
        targetY = newY;
        isMovingY = true;
    }
}
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;    // Le personnage à suivre
    public float smoothSpeed = 0.125f; // Pour un suivi fluide
    public Vector3 offset;    // Décalage de la caméra par rapport au personnage


    // Update est appelé une fois par frame
    void LateUpdate()
    {
        // Créer la position souhaitée de la caméra
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);

        // Interpoler la position de la caméra pour un mouvement fluide
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Appliquer la position de la caméra
        transform.position = smoothedPosition;
    }
}
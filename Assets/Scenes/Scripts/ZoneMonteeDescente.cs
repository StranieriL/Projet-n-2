using UnityEngine;

public class CameraTriggerZone : MonoBehaviour
{
    public float cameraTargetY = -2f;   // La hauteur Y vers laquelle la caméra doit se déplacer

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraFollowWithZoneTriggers camScript = Camera.main.GetComponent<CameraFollowWithZoneTriggers>();

            if (camScript != null)
            {
                camScript.MoveCameraToY(cameraTargetY);
            }
        }
    }
}
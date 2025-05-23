using UnityEngine;

public class ParallaxLoopLayer : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform cameraTransform;

    private float spriteWidth;
    private Vector3 lastCamPosition;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCamPosition = cameraTransform.position;

        // On suppose que les tuiles sont côte à côte
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            spriteWidth = sr.bounds.size.x;
    }

    void Update()
    {
        Vector3 delta = cameraTransform.position - lastCamPosition;
        transform.position += new Vector3(delta.x * speed, 0, 0);
        lastCamPosition = cameraTransform.position;

        // Déplacement infini
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= spriteWidth)
        {
            float offset = (cameraTransform.position.x - transform.position.x) % spriteWidth;
            transform.position = new Vector3(cameraTransform.position.x + offset, transform.position.y, transform.position.z);
        }
    }
}
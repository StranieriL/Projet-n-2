using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TrapMonsterContact : MonoBehaviour
{
    public Rigidbody2D body;

    public GameObject player;
    public GameObject spider;

    public PlayerMovement pm;

    public float SpiderD1;
    public float SpiderDD1;
    public float SpiderD2;
    public float PlayerD1;

    public Vector2 respawn;

    public Ease anim1;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (pm != null)
            {
                pm.enabled = false;
                body.isKinematic = true; 

                spider.transform.DOMoveX(SpiderD1, SpiderDD1)
                    .SetEase(anim1);

                StartCoroutine(SpiderReturn());
            }

        }
    }

    IEnumerator SpiderReturn()
    {
        yield return new WaitForSeconds(1f);

        spider.transform.DOMoveX (SpiderD2, SpiderDD1)
            .SetEase (anim1);

        player.transform.DOMoveX (PlayerD1, SpiderDD1)
            .SetEase(anim1);

        yield return new WaitForSeconds(1.5f);

        Vector3 respawnPosition = new Vector3(respawn.x, respawn.y, player.transform.position.z);
        player.transform.position = respawnPosition;
        body.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.2f);

        pm.enabled = true;
        body.isKinematic = false;
    }


}

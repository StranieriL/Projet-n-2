using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab; // Le prefab du personnage à instancier
    public Transform spawnPoint;       // Le point d'apparition

    private GameObject spawnedCharacter;

    void Start()
    {
        SpawnCharacter();
    }

    void Update()
    {
        // Exemple : Appuyer sur Espace pour respawner le personnage
        if (Input.GetKeyDown(KeyCode.S))
        {
            RespawnCharacter();
        }
    }

    public void SpawnCharacter()
    {
        if (characterPrefab != null && spawnPoint != null)
        {
            spawnedCharacter = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab ou spawnPoint non assigné !");
        }
    }

    public void RespawnCharacter()
    {
        if (spawnedCharacter != null)
        {
            Destroy(spawnedCharacter);
        }
        SpawnCharacter();
    }
}
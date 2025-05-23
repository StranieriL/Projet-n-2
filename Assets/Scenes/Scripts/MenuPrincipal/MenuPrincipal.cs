using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jouer()
    {
        // Charger la scène du jeu (remplace "NomDeLaSceneDuJeu" par ta scène)
        SceneManager.LoadScene("Sidescroller2D");
    }

    public void Options()
    {
        // Afficher le menu des options
        Debug.Log("Options ouvertes (à implémenter)");
    }

    public void Quitter()
    {
        // Quitte le jeu
        Debug.Log("Quitter le jeu");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Pour que ça marche dans l'éditeur
#endif
    }
}
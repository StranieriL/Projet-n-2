using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jouer()
    {
        // Charger la sc�ne du jeu (remplace "NomDeLaSceneDuJeu" par ta sc�ne)
        SceneManager.LoadScene("Sidescroller2D");
    }

    public void Options()
    {
        // Afficher le menu des options
        Debug.Log("Options ouvertes (� impl�menter)");
    }

    public void Quitter()
    {
        // Quitte le jeu
        Debug.Log("Quitter le jeu");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Pour que �a marche dans l'�diteur
#endif
    }
}
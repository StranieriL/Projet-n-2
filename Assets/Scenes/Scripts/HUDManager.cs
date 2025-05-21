using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Image heartIcon;

    private float timer = 0f;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        UpdateTimer();
        UpdateHeart();
    }

    void UpdateTimer()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        int milliseconds = Mathf.FloorToInt((timer * 1000f) % 1000f);
        timerText.text = $"Temps : {minutes:00}:{seconds:00}.{milliseconds:000}";
    }

    void UpdateHeart()
    {
        if (playerHealth == null) return;

        // ❤️ Le cœur est visible si le joueur a au moins 1 point de vie
        heartIcon.enabled = playerHealth.CurrentHealth > 1;
    }
}
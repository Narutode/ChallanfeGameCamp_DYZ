using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int score = 0;
    public GameObject gameOverSceneName;
    public GameObject winSceneName;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(10); // Réduire de 10 points de vie si touché par une balle ennemie
        }
        else if (other.CompareTag("Exit"))
        {
            WinGame(); // Gagner le jeu si atteindre la zone de sortie
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        // Vérifier si le joueur est mort
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameOverSceneName.SetActive(true);
    }

    void WinGame()
    {
        // Charger l'écran de victoire
        winSceneName.SetActive(true);
    }
    void UpdateHealthUI()
    {
        healthText.text = "PV: " + currentHealth.ToString() + " / " + maxHealth.ToString();
    }
    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
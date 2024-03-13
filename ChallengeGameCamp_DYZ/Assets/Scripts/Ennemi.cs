using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    public Player player;

    public int HPMax  = 30;

    public int HPCurrent = 0;
    // Start is called before the first frame update
    private void Start()
    {
        HPCurrent = HPMax;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // Détruire l'ennemi touché
            HPCurrent -= 10;
            player.score += 10; // Augmenter le score
            player.UpdateScoreUI();
            if (HPCurrent <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

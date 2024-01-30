using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Slider[] healthSliders; // 1. Slider oyuncunun canýný gösterir.
    float PlayerHealth = 100f;
    float BossHealth = 100f;

    public void StartingFight()
    {
        foreach (Slider healthBar in healthSliders) { healthBar.gameObject.SetActive(true); }
    }

    public void PlayerHealthUpdater()
    {
        PlayerHealth -= 10f;
    }

    public void BossHealthUpdater()
    {
        BossHealth -= 10f;
        healthSliders[1].value = BossHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Slider[] healthSliders; // 1. Slider oyuncunun can�n� g�sterir.

    public void StartingFight()
    {
        foreach (Slider healthBar in healthSliders) { healthBar.gameObject.SetActive(true); }
    }
}

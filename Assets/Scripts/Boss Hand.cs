using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    GameController gmController;
    Boss boss;

    private void Start()
    {
        gmController = FindAnyObjectByType<GameController>();
        boss = FindAnyObjectByType<Boss>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && boss.isAttacking)
        {
            gmController.PlayerHealthUpdater();
        }

    }
}

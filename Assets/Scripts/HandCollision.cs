using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollision : MonoBehaviour
{
    GameController gmController;
    PlayerController playerController;

    private void Start()
    {
        gmController = FindAnyObjectByType<GameController>();
        playerController = FindAnyObjectByType<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss") && playerController.isAttacking)
        {
            gmController.BossHealthUpdater();
        }
    }
}

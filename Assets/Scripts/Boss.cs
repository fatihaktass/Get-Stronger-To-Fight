using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public int BossLevel;
    public GameObject bossLevelText;
    Animator bossAnimator;
    NavMeshAgent bossAgent;
    PlayerController playerController;
    bool attackRange, checkRange;

    public LayerMask checkLayer;
    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        bossLevelText.GetComponent<TextMeshPro>().text = "Lv. " + BossLevel.ToString();
        gameObject.transform.localScale += BossLevel * new Vector3(0.025f, 0.025f, 0.025f);
        bossAgent = GetComponent<NavMeshAgent>();
        bossAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerController.Finished && playerController.isStarting)
        {
            PosUpdater();
        }
    }

    void PosUpdater()
    {
        bossAgent.SetDestination(playerController.transform.position);
        bossAgent.transform.LookAt(playerController.transform.position);

        checkRange = Physics.CheckSphere(transform.position, 5f, checkLayer);
        attackRange = Physics.CheckSphere(transform.position, 1.4f, checkLayer);

        if (checkRange)
        {
            bossAnimator.SetBool("Checking", true);
            bossAnimator.SetBool("Attacking", false);
        }
        if (attackRange)
        {
            bossAnimator.SetBool("Attacking", true);
        }

        
    }

}

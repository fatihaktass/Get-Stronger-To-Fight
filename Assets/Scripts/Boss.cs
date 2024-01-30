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
    public bool isAttacking;

    public LayerMask checkLayer;
    private void Start()
    {
        bossAgent = GetComponent<NavMeshAgent>();
        bossAnimator = GetComponent<Animator>();
        playerController = FindAnyObjectByType<PlayerController>();
        bossLevelText.GetComponent<TextMeshPro>().text = "Lv. " + BossLevel.ToString();
        gameObject.transform.localScale += BossLevel * new Vector3(0.025f, 0.025f, 0.025f);
        
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
        checkRange = Physics.CheckSphere(transform.position, 5f, checkLayer);
        attackRange = Physics.CheckSphere(transform.position, 1.4f, checkLayer);

        if (checkRange && !isAttacking)
        {
            bossAnimator.SetBool("Checking", true);
            bossAnimator.SetBool("Attacking", false);
            bossAgent.SetDestination(playerController.transform.position);
            bossAgent.transform.LookAt(playerController.transform.position);
            bossAgent.isStopped = false;
        }
        if (attackRange && !isAttacking)
        {
            bossAgent.isStopped = true;
            bossAnimator.SetBool("Attacking", true);
            isAttacking = true;
            Invoke("FinishedAttack", 0.6f);
        }
    }

    public void FinishedAttack()
    {
        isAttacking = false;
        Debug.LogAssertion("bb");
    }
}

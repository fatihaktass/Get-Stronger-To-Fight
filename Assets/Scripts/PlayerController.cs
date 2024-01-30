using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    int charLevel;
    public GameObject charLevelText;
    public float Speed;
    float VerticalMovement = 1;
    public GameObject Character;

    float TimerPoint = 4.4f;
    public TextMeshProUGUI timerText;

    string startText;
    public bool isStarting = false;
    public bool Finished = false;
    public bool isAttacking = false;
    public Animator anim;

    GameController gmController;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        anim = GetComponent<Animator>();
        gmController = FindAnyObjectByType<GameController>();

        startText = "GO!";
    }

    private void FixedUpdate()
    {
        if (isStarting)
        {
            float HorizontalMovement = Input.GetAxis("Horizontal");
            Vector3 tempVect = (HorizontalMovement * transform.right + VerticalMovement * transform.forward).normalized;
            rb.MovePosition(transform.position + tempVect * Speed * Time.deltaTime);
            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", VerticalMovement);
        }
        if (Finished)
        {
            VerticalMovement = Input.GetAxis("Vertical");
        }
    }

    void Update()
    {
        StartTimer(startText);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
            Attacks();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Invoke(nameof(AttackTimer), .6f);
        }
        
    }

    void StartTimer(string startText)
    {
        if (TimerPoint > 2)
        { TimerPoint -= Time.deltaTime; timerText.text = (Mathf.Round(TimerPoint - 1)).ToString(); isStarting = false; }
        else if (TimerPoint > 1.4 && TimerPoint < 2 )
        { TimerPoint -= Time.deltaTime; timerText.text = startText; }
        else
        { isStarting = true; timerText.gameObject.SetActive(false); }
    }

    void LevelChanger(bool isIncreasing)
    {
        if (isIncreasing)
        {
            charLevel++;
        }
        else
        {
            charLevel--;
        }
        charLevelText.GetComponent<TextMeshPro>().text = "Lv. " + charLevel.ToString();
    }

    void FinishArea()
    {
        Finished = true;
        timerText.gameObject.SetActive(true);
        anim.SetFloat("Vertical", 0);
        anim.SetBool("FightArea", true);
        startText = "FIGHT!";
        TimerPoint = 4.4f;
        Speed = 4f;
        FindAnyObjectByType<MouseInput>().AllowChanger();
        gmController.StartingFight();
    }

    void Attacks()
    {
        if (isAttacking)
        {
            anim.SetBool("Punch", true);
        }
        if (!isAttacking)
        {
            anim.SetBool("Punch", false);
        }
        
    }

    void AttackTimer()
    {
        isAttacking = false;
        Attacks();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("PositiveColor"))
        {
            Destroy(other.collider.gameObject);
            Character.transform.localScale += new Vector3(0.025f, 0.025f, 0.025f);
            LevelChanger(true);
        }
        if (other.collider.CompareTag("NegativeColor"))
        {
            Destroy(other.collider.gameObject);
            if (charLevel > 0)
            {
                Character.transform.localScale -= new Vector3(0.025f, 0.025f, 0.025f);
                LevelChanger(false);
            }
        }
        if (other.collider.CompareTag("Finish"))
        {
            Destroy(other.collider.gameObject);
            FinishArea();
        }
    }   
}

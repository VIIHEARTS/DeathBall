using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCtrls : MonoBehaviour
{

    public PlayerBatting ballScript;

    private float movSpeed = 5f;
    float speedX, speedY;
    private Vector2 moveInput;
    Rigidbody2D rb;
    public bool isPlayer1;

    public bool hasAttacked;
    public bool canAttack;
    public float attackCooldown;
    public float attackCooldownTicker;
    public float attackCooldownMax;

    public GameObject racket;



    private float currentSpeed;
    public float dashSpeed;
    public float dashDuration = 5f;
    public float dashCountdown;
    public float dashCooldown = 10f;
    public float dashCooldownTicker = 0f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = movSpeed;
        dashCooldownTicker = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer1)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            hasAttacked = Input.GetButton("FireP1");
        }
        else
        {
            moveInput.x = Input.GetAxisRaw("Horizontal2");
            moveInput.y = Input.GetAxisRaw("Vertical2");

            hasAttacked = Input.GetButton("FireP2");
        }
        

        rb.linearVelocity = moveInput * currentSpeed;

        Vector2 playerPos = GameObject.FindWithTag("Player").transform.position;
        ballScript.hasMoved = playerPos != Vector2.zero;

        if (isPlayer1 && Input.GetButtonDown("DashP1"))
        {
            if (dashCountdown <= 0  && dashCooldownTicker <= 0)
            {
                currentSpeed = dashSpeed;
                dashCountdown = dashDuration;
                dashCooldownTicker = dashCooldown;
            }
        }

        if (!isPlayer1 && Input.GetButtonDown("DashP2"))
        {
            if (dashCountdown <= 0 && dashCooldownTicker <= 0)
            {
                currentSpeed = dashSpeed;
                dashCountdown = dashDuration;
                dashCooldownTicker = dashCooldown;
            }
        }






        //Dash Countdown (for how long to dash)
        if (dashCountdown > 0)
        {
            dashCountdown -= Time.deltaTime;

            if (dashCountdown <= 0)
            {
                currentSpeed = movSpeed;
            }
        }

        //Dash Cooldown (for how long you cannot dash)
        if (dashCountdown <= 0 && dashCooldownTicker >= 0)
        {
            dashCooldownTicker -= Time.deltaTime;

            if (dashCooldownTicker <= 0)
            {
                dashCooldownTicker = 0; 
            }
        }



        if (hasAttacked == true)
        {
            racket.SetActive(true);
        }
        else
        {
            racket.SetActive(false);
        }


        if (isPlayer1 && Input.GetButton("FireP1"))
        {
            if (attackCooldownMax >= 0 && attackCooldownTicker == 0)
            {
                attackCooldownTicker = attackCooldownMax;
            }
        }

        if (attackCooldownTicker >= 0)
        {
            attackCooldownTicker -= Time.deltaTime;            
        }

        if (attackCooldownTicker <= 0)
        {
            attackCooldownTicker = 0;
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCtrls : MonoBehaviour
{
    private float movSpeed = 5f;
    float speedX, speedY;
    private Vector2 moveInput;
    Rigidbody2D rb;
    public bool hasMoved;


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
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = moveInput * currentSpeed;

        Vector2 playerPos = GameObject.Find("Player").transform.position;
        hasMoved = playerPos != Vector2.zero;

        if (Input.GetButtonDown("Fire1"))
        {
            if (dashCountdown <= 0  && dashCooldownTicker <= 0)
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

    }
}
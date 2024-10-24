using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerBatting : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float initialknockbackForce = 10f;
    public float maxknockbackForce = 50f;
    public float currentknockbackForce;

    private Transform currentTarget;
    private GameObject player1;
    private GameObject player2;

    private Rigidbody2D rb;

    public bool hasMoved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        if (player1 != null && player2 != null)
        {
            currentTarget = player1.transform;
        }
        else
        {

            Debug.Log("Player1 or Player2 not found in the scene");
        }

        rb = GetComponent<Rigidbody2D>();

        currentknockbackForce = initialknockbackForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasMoved)
        {
            return;
        }

        if (currentTarget == null && hasMoved)
        {
            currentTarget = player1.transform;
        }

        if (currentTarget != null)
        {
            Vector2 direction = currentTarget.position - transform.position;
            direction.Normalize();

            rb.linearVelocity = direction * currentknockbackForce;
        }
    }

    //Hitting the ball
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "RacketP1" && currentTarget == player1.transform)
        {
            ApplyKnockback(collision.transform);

            currentTarget = player2.transform;
        }
        else if (collision.gameObject.name == "RacketP2" && currentTarget == player2.transform)
        {
            ApplyKnockback(collision.transform);

            currentTarget = player1.transform;
        }
        else if (collision.gameObject.name == "Player1")
        {
            ScoreManager.Instance.UpdateScore(2);
            ResetScene();
        }
        else if (collision.gameObject.name == "Player2")
        {
            ScoreManager.Instance.UpdateScore(1);
            ResetScene();
        }
    }

    private void ApplyKnockback(Transform target)
    {
        Vector2 knockbackDirection = (transform.position - target.position).normalized;

        rb.AddForce(knockbackDirection * currentknockbackForce, ForceMode2D.Impulse);

        if (currentknockbackForce < maxknockbackForce)
        {
            currentknockbackForce += currentknockbackForce * 0.1f;
            currentknockbackForce = Mathf.Min(currentknockbackForce, maxknockbackForce);
        }

    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

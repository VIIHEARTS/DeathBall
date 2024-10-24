using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public int player1Score = 0;
    public int player2Score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(int playerNumber)
    {
        if(playerNumber == 1)
        {
            player1Score++;
        }
        else if (playerNumber == 2)
        {
            player2Score++;
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (player1ScoreText != null && player2ScoreText != null)
        {
            player1ScoreText.text = player1Score.ToString();
            player2ScoreText.text = player2Score.ToString();
        }
    }
}

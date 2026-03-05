using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private int score = 0;
    private int highScore = 0;
    private int multiplier = 1;

    void Awake()
    {
        Instance = this;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void AddScore(int amount)
    {
        score += amount * multiplier;
        UpdateUI();
    }

    public void SetMultiplier(int val) => multiplier = val;
    public void ResetMultiplier() => multiplier = 1;
    public int GetScore() => score;
    void Start() => UpdateUI();

    void UpdateUI()
    {
        
        if (scoreText) scoreText.text = "Score: " + score;
        if (highScoreText) highScoreText.text = "Best: " + highScore;
    }

    public void SaveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
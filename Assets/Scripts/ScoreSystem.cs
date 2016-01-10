using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [Space(10)]
    [Header("Score System")]

    public float currentScore;

    public float pointsPerAsteroid;

    public Text scoreText;

    private void Start()
    {
    }

    private void Update()
    {
        scoreText.text = "Score: " + Mathf.Round(currentScore).ToString();
    }

    public void AddScore(float increment)
    {
        currentScore += increment;
    }
}
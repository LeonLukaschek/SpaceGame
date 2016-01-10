using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Asteroids")]
    public GameObject[] asteroids;

    [Space(10)]
    [Header("Waves")]
    public float waveCount;

    public float asteroidsPerWave;
    public float timeBetweenWaves;
    public float timeBetweenSpawns;
    public float startDelay;
    public int curentWave;

    [Space(5)]
    public bool increaseNumberOfAsteroids;

    public bool randomIncrease;
    public float asteroidIncreaseNum;

    [Space(5)]
    public Text waveText;

    [Space(10)]
    [Header("Game Over")]
    public Text gameOverText;

    public Text gameOverScoreText;
    public Text playerHealthText;

    public GameObject gameOverPlane;

    public float fadeTime;

    private bool gameOver;
    private ScoreSystem scoreSystem;

    private void Start()
    {
        StartCoroutine("WaveManager");
        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        gameOver = false;
    }

    private void Update()
    {
        if (gameOver)
        {
            StopCoroutine(WaveManager());
        }
    }

    private IEnumerator WaveManager()
    {
        yield return new WaitForSeconds(startDelay);

        for (int i = 0; i < waveCount; i++)
        {
            curentWave = i + 1;

            waveText.text = "CURRENT WAVE: " + curentWave;

            for (int y = 0; y < asteroidsPerWave; y++)
            {
                SpawnAsteroid();

                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            if (increaseNumberOfAsteroids)
            {
                if (randomIncrease)
                {
                    asteroidsPerWave += Random.Range(1, 10);
                }
                else
                {
                    asteroidsPerWave += asteroidIncreaseNum;
                }
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void SpawnAsteroid()
    {
        int randNummer = Random.Range(0, asteroids.Length);

        Vector3 randomPosition = new Vector3(Random.Range(-7.5f, 7.5f), 8, 1);

        GameObject spawnedAsteroid = Instantiate(asteroids[randNummer], randomPosition, Quaternion.identity) as GameObject;

        spawnedAsteroid.transform.SetParent(GameObject.Find("Holder").transform);
    }

    public void GameOver()
    {
        gameOver = true;

        Color c = gameOverPlane.GetComponent<Renderer>().material.color;
        c.a += fadeTime;
        gameOverPlane.GetComponent<Renderer>().material.color = c;

        playerHealthText.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(true);

        gameOverScoreText.gameObject.SetActive(true);

        gameOverScoreText.text = "Score:                   " + scoreSystem.currentScore.ToString() + "\n+ Wave bonus (" + curentWave.ToString() + " x5): " + curentWave * 5 + "\nTotal Score:           " + (scoreSystem.currentScore + (curentWave * 5));
    }
}
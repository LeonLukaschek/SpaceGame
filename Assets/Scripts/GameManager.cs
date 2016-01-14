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
    private float _asteroidsPerWave;
    public float timeBetweenWaves;
    public float timeBetweenSpawns;
    public float startDelay;
    public int curentWave;
    private int _curentWave;

    [Space(5)]
    public bool increaseNumberOfAsteroids;

    public bool randomIncrease;
    public float asteroidIncreaseNum;

    [Space(5)]
    public Text waveText;

    [Space(10)]
    [Header("Game Over")]
    public bool gameOver;

    public float fadeTime;
    public float restartDelay;

    public Text gameOverText;

    public Text gameOverScoreText;
    public Text playerHealthText;

    public GameObject gameOverPlane;

    private ScoreSystem scoreSystem;
    private PlayerController player;

    private void Start()
    {
        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine("WaveManager");
        gameOver = false;

        _asteroidsPerWave = asteroidsPerWave;
    }

    private void Update()
    {
        if (gameOver)
        {
            StopAllCoroutines();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Restart();
        }
    }

    private IEnumerator WaveManager()
    {
        yield return new WaitForSeconds(startDelay);

        for (int i = 0; ; i++)
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

    private void Restart()
    {
        //No more gameOver
        gameOver = false;

        //Revert the fade in and remove the gameOverPanel
        Color c = gameOverPlane.GetComponent<Renderer>().material.color;
        c.a = 0;
        gameOverPlane.GetComponent<Renderer>().material.color = c;
        //Enable hp text
        playerHealthText.gameObject.SetActive(true);
        //disable gameover Text
        gameOverText.gameObject.SetActive(false);
        //disable game over score
        gameOverScoreText.gameObject.SetActive(false);
        //reset game over text
        gameOverScoreText.text = "";
        //Player health = starting health
        player.health = player._health;
        //reset score
        scoreSystem.currentScore = 0;
        //reset asteroids per wave
        asteroidsPerWave = _asteroidsPerWave;
        //Extra starting delay
        startDelay = restartDelay;
        //restart wave manager for asteroids spawning
        StartCoroutine(WaveManager());
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
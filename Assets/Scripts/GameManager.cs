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

    private void Start()
    {
        StartCoroutine("WaveManager");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAsteroid();
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
                Debug.Log("Spawing asteroid");
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
}
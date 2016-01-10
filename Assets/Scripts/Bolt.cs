using System.Collections;
using UnityEngine;

public class Bolt : MonoBehaviour
{

    [Header("Movement")]
    public float speed;

    private ScoreSystem scoreSystem;

    private void Start()
    {
        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(0, speed * Time.deltaTime, 0);
        this.transform.position += move;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            scoreSystem.AddScore(scoreSystem.pointsPerAsteroid);
        }

        Destroy(this.gameObject);
    }
}
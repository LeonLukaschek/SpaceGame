using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    [Header("Shooting")]
    public float timeBetweenShots;

    public GameObject shotSpawnPoint;
    public GameObject bolt;

    [Header("Player boundary")]

    public float xMin;

    public float xMax;

    public float yMin, yMax;

    private void Start()
    {
    }

    private void Update()
    {
        //Moving
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        transform.position += move * speed * Time.deltaTime;

        //Limit player position
        transform.position = new Vector3
             (
                 Mathf.Clamp(transform.position.x, xMin, xMax),
                 Mathf.Clamp(transform.position.y, yMin, yMax),
                 1
                 );

        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(bolt, shotSpawnPoint.transform.position, transform.rotation);
    }
}
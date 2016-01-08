using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    [Space(15)]
    [Header("Shooting")]
    public float timeBetweenShots;

    public GameObject shotSpawnPoint;
    public GameObject bolt;

    private void Start()
    {
    }

    private void Update()
    {
        //Moving
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;

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
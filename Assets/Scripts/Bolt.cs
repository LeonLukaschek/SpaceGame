using System.Collections;
using UnityEngine;

public class Bolt : MonoBehaviour
{

    [Header("Movement")]
    public float speed;

    private void Update()
    {
        Vector3 move = new Vector3(0, speed * Time.deltaTime, 0);
        this.transform.position += move;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject, 1f);
    }
}
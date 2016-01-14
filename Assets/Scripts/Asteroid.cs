using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [Header("Movement")]
    public float speed;

    public float rotSpeed;

    [Space(10)]
    [Header("Explusion")]
    public Animator anim;

    private Quaternion start;

    private Quaternion end;

    private void Start()
    {
        anim = GetComponent<Animator>();
        end.z = Random.Range(50, 300);
    }

    private void Update()
    {
        this.transform.position -= new Vector3(0, 1, 0) * speed * Time.deltaTime;
        Vector3 rot = new Vector3(0, 0, rotSpeed);
        transform.Rotate(rot);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bolt")
        {
            anim.SetBool("shouldExplode", true);
            Destroy(this.gameObject);
        }
    }
}
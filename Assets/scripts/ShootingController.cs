using UnityEngine;
using System.Collections;

public class ShootingController : MonoBehaviour {

    public float speed;
    public float distance;

    private Rigidbody rb;
    private Vector3 startPosition;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        startPosition = rb.position;
        rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        // destroy wave after traveling a certain distance
        if (rb.position.z > startPosition.z + distance)
        {
            Destroy(gameObject);
        }
    }
}

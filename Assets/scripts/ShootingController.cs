using UnityEngine;
using System.Collections;

public class ShootingController : MonoBehaviour {

    public float movementX;
    public float movementZ;
    public float speed;
    
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    void Update()
    {
        bool mouseInput;

        if (gameObject.CompareTag("Left Wave"))
        {
            mouseInput = Input.GetMouseButtonDown(0);
        } else
        {
            mouseInput = Input.GetMouseButtonDown(1);
        }

        // left mouse button pressed 
        if (mouseInput)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementZ);
            rb.AddForce(movement * speed);
        }

       
    }
}

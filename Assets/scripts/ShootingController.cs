using UnityEngine;
using System.Collections;

public class ShootingController : MonoBehaviour {

    public float movementX;
    public float movementZ;
    public float speed;
    public GameObject boat;

    private Rigidbody rb;
    private Vector3 initPosition; //The initial position of wave
    private bool shot;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        initPosition = gameObject.transform.position;
        shot = false;
    }

    void Update()
    {
        bool mouseInput;

        // Keep waves to the sides of the boat if they haven't been shot
        if (!shot)
        {
            Vector3 boatPosition = boat.transform.position;
            float offsetX = 10.2f;

            if (gameObject.CompareTag("Left Wave"))
            {
                offsetX = -1 * offsetX;
            }
 
            gameObject.transform.position = new Vector3(initPosition.x + offsetX, initPosition.y, boatPosition.z + 8);
        }
        

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
            shot = true;
            Vector3 movement = new Vector3(movementX, 0.0f, movementZ);
            rb.AddForce(movement * speed);
        }

       
    }
}

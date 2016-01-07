using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class boat : MonoBehaviour {

    public float turnSpeed = 1000f;
    public float accelerateSpeed = 1000f;

	public Transform explosionPrefab;
    private Rigidbody rbody;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Turn right or left.
        rbody.AddTorque(0f, h * turnSpeed * Time.deltaTime, 0);

        //Move Forward
        rbody.AddForce(transform.forward * accelerateSpeed * Time.deltaTime);

	}

    
    void OnCollisionEnter(Collision col)
    {
        //Any part of the boat hits an object

        //Hits any rock
		if (col.collider.name == "Group003" || col.collider.name == "toad1" )
        {
			ContactPoint contact = col.contacts[0];
			Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
			Vector3 pos = contact.point;
			Instantiate(explosionPrefab, pos, rot);
			gameObject.transform.localScale = new Vector3 (0, 0, 0);
			StartCoroutine(WaitAndRestart(0.5F));
        }
        else if (col.collider.name == "toad1")
        {
            print("Boat hits monster");
        }
    }

	IEnumerator WaitAndRestart(float waitTime) {
		Destroy(gameObject,1);
		yield return new WaitForSeconds(waitTime);
		print ("waiting...");
		SceneManager.LoadScene(0);
	}
    
}

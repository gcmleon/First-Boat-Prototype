using UnityEngine;
using System.Collections;

public class moveRightWave : MonoBehaviour {

	public GameObject boat;
	public float offset;

	private float boatSideSpeed;
	private float boatAccelerationSpeed;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		boatSideSpeed = boat.GetComponent<boat>().sideSpeed;
		boatAccelerationSpeed = boat.GetComponent<boat>().accelerateSpeed;
	}

	// Update is called once per frame
	void Update () {

		Vector3 boatPosition = boat.transform.position;

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");



		//Move Left or right along with the boat
		transform.position = new Vector3(boatPosition[0]+offset, 1, 0);


		//Move Forward
		//rb.AddForce(transform.forward * boatAccelerationSpeed * Time.deltaTime);
	}
}

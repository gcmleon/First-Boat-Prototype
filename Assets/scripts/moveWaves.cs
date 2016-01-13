using UnityEngine;
using System.Collections;

public class moveWaves : MonoBehaviour {

	public GameObject boat;
	public GameObject rightWave;
	public GameObject leftWave;
	public float offset;

	private float boatSideSpeed;
	private float boatAccelerationSpeed;

	private Rigidbody rightWaveBody;
	private Rigidbody leftWaveBody;

	// Use this for initialization
	void Start () {
		rightWaveBody = rightWave.GetComponent<Rigidbody>();
		leftWaveBody = leftWave.GetComponent<Rigidbody> ();

		boatSideSpeed = boat.GetComponent<boat>().sideSpeed;
		boatAccelerationSpeed = boat.GetComponent<boat>().accelerateSpeed;
	}

	// Update is called once per frame
	void Update () {

		Vector3 boatPosition = boat.transform.position;

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if (h > 0) {
			leftWaveBody.transform.position = new Vector3(boatPosition[0]-offset, leftWaveBody.transform.position[1], boatPosition[2]);
		} else if (h < 0) {
			//Move rightWave along with the boat
			rightWave.transform.position = new Vector3(boatPosition[0]+offset, rightWaveBody.transform.position[1], boatPosition[2]);
		} else {
			
		}




		//Move Forward
		//rb.AddForce(transform.forward * boatAccelerationSpeed * Time.deltaTime);
	}
}

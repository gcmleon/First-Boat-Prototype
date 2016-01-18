using UnityEngine;
using System.Collections;

public class moveWaves : MonoBehaviour {

	public GameObject boat;
	public GameObject rightWave;
	public GameObject leftWave;
	public float offset;//Determines the distance betweeen the wave and the boat
	public float maxY; //Controls the max height of the waves

	private float boatSideSpeed;
	private float boatAccelerationSpeed;

	private float accRight = 0; //Acceleration of the rightWave
	private float accLeft = 0; //Acceleration of the leftWave
	private Vector3 initRightPosition; //The initial position of rightWave
	private Vector3 initLeftPosition; //The innitial position of leftwave

	// Use this for initialization
	void Start () {

		boatSideSpeed = boat.GetComponent<boat>().sideSpeed;
		boatAccelerationSpeed = boat.GetComponent<boat>().accelerateSpeed;

		initRightPosition = leftWave.transform.position;
		initLeftPosition = rightWave.transform.position;
	}

	// Update is called once per frame
	void Update () {

		Vector3 boatPosition = boat.transform.position;

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if (h > 0) {
			//Move leftWave along with the boat
			if (accLeft < maxY) {
				accLeft += 0.001f;
				leftWave.transform.position = new Vector3 (boatPosition [0] - offset, leftWave.transform.position [1] + accLeft, boatPosition [2]);
			} else {
				leftWave.transform.position = new Vector3(boatPosition[0]-offset, leftWave.transform.position[1], boatPosition[2]);
			}
			accRight = 0;
			rightWave.transform.position = new Vector3(boatPosition[0]+offset, initRightPosition[1], initRightPosition[2]);

		} else if (h < 0) {
			//Move rightWave along with the boat
			if (accRight < maxY) {
				accRight += 0.001f;
				rightWave.transform.position = new Vector3 (boatPosition [0] + offset, rightWave.transform.position [1] + accRight, boatPosition [2]);
			} else {
				rightWave.transform.position = new Vector3(boatPosition[0]+offset, rightWave.transform.position[1], boatPosition[2]);
			}
			accLeft = 0;
			leftWave.transform.position = new Vector3(boatPosition[0]-offset, initLeftPosition[1], initLeftPosition[2]);

		} else {
			//Stop the movement of the waves
			accLeft = 0;
			accRight = 0;
			leftWave.transform.position = new Vector3(boatPosition[0]-offset, initLeftPosition[1], initLeftPosition[2]);
			rightWave.transform.position = new Vector3(boatPosition[0]+offset, initRightPosition[1], initRightPosition[2]);

		}




		//Move Forward
		//rb.AddForce(transform.forward * boatAccelerationSpeed * Time.deltaTime);
	}
}

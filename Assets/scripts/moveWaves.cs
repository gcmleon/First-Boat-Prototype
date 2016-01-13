using UnityEngine;
using System.Collections;

public class moveWaves : MonoBehaviour {

	public GameObject boat;
	public GameObject rightWave;
	public GameObject leftWave;
	public float offset;

	private float boatSideSpeed;
	private float boatAccelerationSpeed;

	private float accRight = 0;
	private float accLeft = 0;
	private Vector3 initRightPosition;
	private Vector3 initLeftPosition;

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
			accLeft += 0.001f;
			accRight = 0;
			leftWave.transform.position = new Vector3(boatPosition[0]-offset, leftWave.transform.position[1]+accLeft, boatPosition[2]);
			rightWave.transform.position = new Vector3(boatPosition[0]+offset, initRightPosition[1], initRightPosition[2]);

		} else if (h < 0) {
			//Move rightWave along with the boat
			accRight += 0.001f;
			accLeft = 0;
			rightWave.transform.position = new Vector3(boatPosition[0]+offset, rightWave.transform.position[1]+accRight, boatPosition[2]);
			leftWave.transform.position = new Vector3(boatPosition[0]-offset, initLeftPosition[1], initLeftPosition[2]);

		} else {
			accLeft = 0;
			accRight = 0;
			leftWave.transform.position = new Vector3(boatPosition[0]-offset, initLeftPosition[1], initLeftPosition[2]);
			rightWave.transform.position = new Vector3(boatPosition[0]+offset, initRightPosition[1], initRightPosition[2]);

		}




		//Move Forward
		//rb.AddForce(transform.forward * boatAccelerationSpeed * Time.deltaTime);
	}
}

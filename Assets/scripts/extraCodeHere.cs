//Turn right or left.
//rbody.AddTorque(0f, h * accelerateSpeed * Time.deltaTime, 0);

//Needed if the boat implements side acceleration

/*if (i == 0) {
			last_value = h;
		}
		if (h == 0 || changeDirection) {
			acc = 0;
		}
		else if (h < 0){
			if (h != last_value) {
				changeDirection = true;
			} else {
				last_value = h;
			}
		}
		else{
			if (h != last_value) {
				changeDirection = true;
			} else {
				last_value = h;
			}
		}
		if (acc < maxSideSpeed) {
			acc += 0.001f;
		}
		*/

/*Related to Collision in the boat.cs
ContactPoint contact = col.contacts[0];
Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
Vector3 pos = contact.point;
Instantiate(explosionPrefab, pos, rot);
gameObject.transform.localScale = new Vector3(0, 0, 0);
StartCoroutine(WaitAndRestart(0.5F));*/

//Related to Collisioon in monsterToad.cs
//print("I'm toad - sth collided with me: " + collision.collider.name);
/*
        void OnCollisionEnter(Collision collision)
    {
        //print("I'm toad - sth collided with me: " + collision.collider.name);

        if (collision.collider.name == "Boat")
        {
            print("I'm toad - boat collided with me!");
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(explosionPrefab, pos, rot);
            Destroy(gameObject);
        }

}
*/

//Code if we separate the waves

//Wave Left

/*public GameObject boat;
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
	transform.position = new Vector3(boatPosition[0]-offset, transform.position[1], boatPosition[2]);


	//Move Forward
	//rb.AddForce(transform.forward * boatAccelerationSpeed * Time.deltaTime);
}
*/

//RightWave

/*
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

	if (h != 0)
	{

	}

	//Move Left or right along with the boat
	transform.position = new Vector3(boatPosition[0]+offset, transform.position[1], boatPosition[2]);


	//Move Forward
	//rb.AddForce(transform.forward * boatAccelerationSpeed * Time.deltaTime);
}
*/
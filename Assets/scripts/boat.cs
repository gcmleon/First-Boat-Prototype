using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class boat : MonoBehaviour {

    public float sideSpeed = 10f;
	public float maxSideSpeed;
    public float accelerateSpeed = 1000f;

	public Transform leftLimit;
	public Transform rightLimit;

    // Shooting waves
    public GameObject shot;
    public Transform leftShot;
    public Transform rightShot;
    public float fireRate;

	public Transform explosionPrefab;
    private Rigidbody rbody;

	private float acc = 1; //Acceleration of the boatToRight
	private bool changeDirection = false;
	private float last_value;
	private int i = 0;

    // threshold for shooting waves
    private float nextFire;

    // Healthbar
    public RectTransform healthTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    public int maxHealth;
    public Text healthText;
    public Image visualHealth;
    private int currentHealth;
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
            HandleHealth();
        }
    }

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody>();

        // Healthbar
        cachedY = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - healthTransform.rect.width;
        CurrentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Turn right or left.
		//rbody.AddTorque(0f, h * accelerateSpeed * Time.deltaTime, 0);

		//Needed if we implement side acceleration

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

		//Move Left or right
		//Validates if the boat is colliding with the left limit or the right limit
		if ((Mathf.Abs(rbody.transform.position [0]) < Mathf.Abs(rightLimit.position [0]))) {
			rbody.transform.Translate (h * sideSpeed * Time.deltaTime * acc, 0, 0);
			if ((Mathf.Abs (rbody.transform.position [0]) < Mathf.Abs (leftLimit.position [0]))) {
				rbody.transform.Translate (h * sideSpeed * Time.deltaTime * acc, 0, 0);
			} else {
				rbody.transform.Translate (-1 * sideSpeed * Time.deltaTime * acc, 0, 0);
				//print ("You cannot cross right limits");
			}

		} else {
			rbody.transform.Translate (sideSpeed * Time.deltaTime * acc, 0, 0);
			//print ("You cannot cross left limits");
		}
        //Move Forward
        rbody.AddForce(transform.forward * accelerateSpeed * Time.deltaTime);

        // Shooting left wave
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, leftShot.position, leftShot.rotation);
        }

        // Shooting right wave
        if (Input.GetMouseButtonDown(1) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, rightShot.position, rightShot.rotation);
        }
    }

    private void HandleHealth()
    {
        if (currentHealth > 0)
        { healthText.text = "Health:" + currentHealth; }
        else
        {
            currentHealth = 0;
            healthText.text = "Health:" + currentHealth;
        }
        float currentXValue = MapVlaues(currentHealth, 0, maxHealth, minXValue, maxXValue);
        healthTransform.position = new Vector3(currentXValue, cachedY);



        if (currentHealth > maxHealth / 2) //more than 50
        {
            visualHealth.color = new Color32((byte)MapVlaues(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }
        else // less than 50
        {
            visualHealth.color = new Color32(255, (byte)MapVlaues(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        //Any part of the boat hits an object

        if (currentHealth > 0)
        {     // Boat hits any rock, rock gets destroyed
            if (col.collider.name == "Group003")
            {

                print("boat hits rock - destroy rock and take life from boat");
                CurrentHealth -= 24;
            }
            else if (col.collider.name == "toad 1")
            {
                print("boat hits monster - destroy monster and take life from boat");
                CurrentHealth -= 45;
                /*
                ContactPoint contact = col.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;
                Instantiate(explosionPrefab, pos, rot);
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                StartCoroutine(WaitAndRestart(0.5F));*/
            }
            else if (col.collider.name == "Shooting_Wave(Clone)")
            {
                print("Boat hit by Shooting Wave");
            }
            else if (col.collider.name == "Right Shooting")
            {
                print("Boat hit by Right Wave");
            }
            print("BOAT COLLISION " + col.collider.name);
        }
        else
        {
           
            StartCoroutine(WaitAndRestart(0.5F));
        }

    }

	IEnumerator WaitAndRestart(float waitTime) {
		Destroy(gameObject, 1);
		yield return new WaitForSeconds(waitTime);
		print ("waiting to restart...");
		SceneManager.LoadScene(0);
	}

    private float MapVlaues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

    }


}

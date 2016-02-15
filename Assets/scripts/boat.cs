using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class boat : MonoBehaviour {

	private int firstlevel = 1;

	//It controls the speed of the boat, to the sides or to the front.
    public float sideSpeed = 10f;
	public float maxSideSpeed;
    public float accelerateSpeed = 1000f;

	//The side limits, which the boat can not cross.
	public Transform leftLimit;
	public Transform rightLimit;

    // Shooting waves
    public GameObject shot;
    public Transform leftShot;
    public Transform rightShot;
    public float fireRate;

	public Transform explosionPrefab;
    private Rigidbody rbody;

    // threshold for shooting waves
    private float nextFire;

    // Healthbar variables declerations 
    public RectTransform healthTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    public int maxHealth;
    public Text healthText;
    public Image visualHealth;
    private Vector3 movment;
    public float healthSpeed;
    public Canvas canvas; // health bar canvas
    private float currentXValue;
    public float coolDown;// taking damage after a period of time 
    private bool onCD; // checks if it is in the cooldown period (can take damage or not )or not 
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
           // HandleHealth();
        }
    }

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody>();

      //***** Healthbar***************

        // caches the healthbar as start positions
        cachedY = healthTransform.position.y;

        //The max value of the x-Position 
        maxXValue = healthTransform.position.x;

        // Calculating the min value of the x-Position       
        minXValue = healthTransform.position.x - healthTransform.rect.width * canvas.scaleFactor;

        // Set the current health to the max health which is 100 
        currentHealth = maxHealth;

        // set the cooldamage to false
        onCD = false;
       
    }
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        HandleHealth();

		//Move Left or right
		//Validates if the boat is colliding with the left limit or the right limit
		if ((Mathf.Abs(rbody.transform.position [0]) < Mathf.Abs(rightLimit.position [0]))) {
			rbody.transform.Translate (h * sideSpeed * Time.deltaTime, 0, 0);
			if ((Mathf.Abs (rbody.transform.position [0]) < Mathf.Abs (leftLimit.position [0]))) {
				rbody.transform.Translate (h * sideSpeed * Time.deltaTime, 0, 0);
			} else {
				rbody.transform.Translate (-1 * sideSpeed * Time.deltaTime, 0, 0);
			}

		} else {
			rbody.transform.Translate (sideSpeed * Time.deltaTime, 0, 0);
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

    // Function to handle the healthbar 
    private void HandleHealth()
    {
        if (currentHealth > 0)
        { healthText.text = "Health:" + currentHealth; }
        else
        {
            currentHealth = 0;
            //healthText.text = "Health:" + currentHealth;
        }

        // maps the x-min and x-max postion to the health range [0, 100]
        currentXValue = MapVlaues(currentHealth, 0, maxHealth, minXValue, maxXValue);
        // sets the position of the health after reduction
        healthTransform.position = new Vector3(currentXValue, cachedY);
        //healthTransform.position = Vector3.Lerp(healthTransform.position, new Vector3(currentXValue, cachedY), Time.deltaTime);


        //******Changing the healthbar color 
        //more than 50 healthbar is in green color
        if (currentHealth > maxHealth / 2) 
        {
            visualHealth.color = new Color32((byte)MapVlaues(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }
        // less than 50 changed to yellow and red
        else
        {
            visualHealth.color = new Color32(255, (byte)MapVlaues(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        //Any part of the boat hits an object

        if (currentHealth > 0)
        {     // Boat hits any rock, rock gets destroyed
            if (!onCD && col.collider.name == "Group003")
            {

                print("boat hits rock - destroy rock and take life from boat");
                StartCoroutine(CooldownDmg());
                CurrentHealth -= 24;
            }
            else if (!onCD && col.collider.name == "toad 1")
            {
                print("boat hits monster - destroy monster and take life from boat");
                StartCoroutine(CooldownDmg());
                CurrentHealth -= 45;
            }
            else if (col.collider.name == "Shooting_Wave(Clone)")
            {
                print("Boat hit by Shooting Wave");
            }
            //print("BOAT COLLISION " + col.collider.name);
        }
        else
        {
            ContactPoint contact = col.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(explosionPrefab, pos, rot);
            //gameObject.transform.localScale = new Vector3(0, 0, 0);
            StartCoroutine(WaitAndRestart(0.5F));
            
        }

    }
    IEnumerator CooldownDmg()
    {
        onCD = true;
        yield return new WaitForSeconds(coolDown);
        onCD = false;
    }

    IEnumerator WaitAndRestart(float waitTime) {
		Destroy(gameObject, 1);
		yield return new WaitForSeconds(waitTime);
		print ("waiting to restart...");
		SceneManager.LoadScene(firstlevel);
	}

    private float MapVlaues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

    }


}

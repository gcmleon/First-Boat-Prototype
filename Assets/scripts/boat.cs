using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Collections.Generic;
public class boat : MonoBehaviour {

	private int firstlevel = 1;

	//It controls the speed of the boat, to the sides or to the front.
    public float sideSpeed = 10f;
	public float maxSideSpeed;
    public float accelerateSpeed = 5000f;
    public float inputSpeed = 2.5f;
	public float inputUsed = 0; //0: Keyboard, 1: Android
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

    // Healthbar variables declarations 
    public RectTransform healthTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    private int maxHealth = 100;
    public Text healthText;
    public Image visualHealth;
    private Vector3 movment;
    public float healthSpeed;
    public Canvas canvas; // health bar canvas
    private float currentXValue;
    public float coolDown;// taking damage after a period of time 
    private bool onCD; // checks if it is in the cooldown period (can take damage or not )or not 
    private static int currentHealth = 100;
    public GameObject BH;
    public string objectName = "Ball";

    //h
    public float h = 0;


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
        minXValue = healthTransform.position.x - healthTransform.rect.width;
       

		// Calculating the min value of the x-Position       
		minXValue = healthTransform.position.x - healthTransform.rect.width * canvas.scaleFactor;

		// Set the current health to the max health which is 100 
		currentHealth = maxHealth;

		// set the cooldamage to false
		onCD = false;

        try
        {
            if (s != null)
            {
                s = null;
            }
            mRunning = true;
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    myIp = addr.ToString();
                }
            }
            ThreadStart ts = new ThreadStart(myServ);
            mThread = new Thread(ts);
            if (!mThread.IsAlive)
            {
                IPAddress ipAd = IPAddress.Parse(myIp); //Dns.GetHostEntry("localhost").AddressList[0];
                myList = new TcpListener(ipAd, 5149);
                myList.Start();
                mThread.Start();
            }
        }
        catch (Exception e)
        {
            //new ReadWrite().writefile(e.Message);
            myList.Stop();
        }
    }
	
	// Update is called once per frame
	void Update () {

        HandleHealth();
        if (inputUsed == 0) {
			h = Input.GetAxis ("Horizontal");	
		} else if (inputUsed == 1) {
			//Move Left the boat with the device
			/*if (inputSpeed > 2) {
				h = 1;
			} else if (inputSpeed < -2) {
				//Move Right the boat with the device
				h = -1;
			} else {
				h = 0;
			}
			*/
			h = (float)Math.Pow(inputSpeed/10, 3);
		}

		//print (h);
        
        //float v = Input.GetAxis("Vertical");        


		//Move Left or right
		//Validates if the boat is colliding with the right limit
		if (h < 0) {
			print ("h:"+h);
			if (rbody.transform.position [0] > leftLimit.position [0]) {
				rbody.transform.Translate (h * sideSpeed * Time.deltaTime, 0, 0);

			}else {
				rbody.transform.Translate (1 * sideSpeed * Time.deltaTime, 0, 0);
				print ("Collision left");

			}
		}
		if (h > 0) {
			if (rbody.transform.position [0] < rightLimit.position [0]) {
				rbody.transform.Translate (h * sideSpeed * Time.deltaTime, 0, 0);

			} else {
				rbody.transform.Translate (-1 * sideSpeed * Time.deltaTime, 0, 0);
				print ("Collision right");
			}
		}


        //Move Forward
        rbody.AddForce(transform.forward * accelerateSpeed * Time.deltaTime);

        // Shooting left wave
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, leftShot.position, leftShot.rotation);
        }
        // Why did you comment the previous if?
        if (vibFlag.Equals("true"))
        {
            //print("shottttttttttttttttttttttttttttt");
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
            else if (col.collider.name.Contains(objectName))
            {
                print("boat hits ball - destroy ball and add life to boat");
                print(col.gameObject.name);
                BH = GameObject.Find(col.gameObject.name);
                if (currentHealth != 100)
                {
                    Destroy(BH);
                    CurrentHealth += 24;
                }
                else
                {
                    Destroy(BH);
                }
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
		cleanstuff ();
		Destroy(gameObject, 1);
		yield return new WaitForSeconds(waitTime);
		print ("waiting to restart...");
		int currentScene = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene(currentScene);
	}

    private float MapVlaues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

    }

    // Server code
    Socket s;
    string msg = "0.5";
    string vibFlag = "false";
    Thread mThread;
    TcpListener myList = null;
    public static bool mRunning = false;
    string myIp = "192.168.0.105";
    public void stopListening()
    {
        mRunning = false;
    }

    void myServ()
    {
        if (s != null)
        {
            s.Close();
            s = null;
        }
        try
        {
            // s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //print("inside THREADDDDDDDDDDDDDDDDDDDDDD");
            while (mRunning)
            {
                if (!myList.Pending())
                    Thread.Sleep(100);
                else
                {
                    s = myList.AcceptSocket();
                    try
                    {
                        byte[] b = new byte[102400];
                        while (true)
                        {
                            //int k = s.Receive(b);
                            ////.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                            ////dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf(","));
                            //string dataFromClient = System.Text.Encoding.ASCII.GetString(b, 0, k);
                            //string[] words = dataFromClient.Split(',');
                            //x = words[0];
                            //y = words[1];
                            //z = words[2];
                            //myY = words[3];
                            //foreach (string word in words)
                            //{
                            //x = float.Parse(words[0]);
                            //y = float.Parse(words[1]);
                            //z = float.Parse(words[2]);
                            //          Console.Write(word);
                            //}
                            //    Console.WriteLine();

                            //for (int i = 0; i < k; i++)
                            //  Console.Write(Convert.ToChar(b[i]));

                            //ASCIIEncoding asen = new ASCIIEncoding();
                            //s.Send(asen.GetBytes(y));
                            //Console.WriteLine("\nSent Acknowledgement");
                            //s.Close();

                            int k = s.Receive(b);
                            string dataFromClient = System.Text.Encoding.ASCII.GetString(b, 0, k);
                            string[] words = dataFromClient.Split(',');
                            msg = words[3];
                            inputSpeed = float.Parse(words[3].Substring(0, 3));
                            vibFlag = words[4].Substring(0, 4);
                            print(inputSpeed);
                            if (mRunning == false)
                                break;

                            if (k == 0)
                            {
                                cleanstuff();
                                break;
                            }
                        }
                    }
                    catch (Exception ee)
                    {
                        // new ReadWrite().writefile("server exception" + ee.Message);
                        cleanstuff();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            cleanstuff();
        }

    }
    void cleanstuff()
    {
        //myList.Stop();
        mRunning = false;
        s = null;
        myList = null;
        mThread.Abort();
    }

    void OnApplicationQuit()
    {
        cleanstuff();
    }

}

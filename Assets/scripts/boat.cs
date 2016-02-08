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
    public float accelerateSpeed = 1000f;
    public float inputSpeed = 2.5f;
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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");        

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
        //if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        //{
        //    nextFire = Time.time + fireRate;
        //    Instantiate(shot, leftShot.position, leftShot.rotation);
        //}
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

                //print("boat hits rock - destroy rock and take life from boat");
                CurrentHealth -= 24;
            }
            else if (col.collider.name == "toad 1")
            {
                //print("boat hits monster - destroy monster and take life from boat");
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
           
            StartCoroutine(WaitAndRestart(0.5F));
        }

    }

	IEnumerator WaitAndRestart(float waitTime) {
        cleanstuff();
		Destroy(gameObject, 1);
		yield return new WaitForSeconds(waitTime);
		print ("waiting to restart...");
		SceneManager.LoadScene(firstlevel);
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

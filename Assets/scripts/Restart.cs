using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {
	private int firstlevel = 1;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        //print ("Hey this is update");
        //print (Input.GetKeyDown (KeyCode.R));
        if (Input.GetKeyDown(KeyCode.R))
        {
            boat.mRunning = false;
            SceneManager.LoadScene(firstlevel); //or whatever number your scene is
        }
	}
}

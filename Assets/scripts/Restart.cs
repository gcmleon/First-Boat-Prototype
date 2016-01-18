using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		print ("Hey this is update");
		print (Input.GetKeyDown (KeyCode.R));
		if(Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(0); //or whatever number your scene is

	}
}

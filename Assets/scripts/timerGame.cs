using UnityEngine;
using System.Collections;

public class timerGame : MonoBehaviour {

	float currentTimer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentTimer += Time.deltaTime;
	}

	void OnGUI(){
		GUI.Label (new Rect(50, 50, 200, 100), "Time: " + (int)currentTimer);
	}
}

using UnityEngine;
using System.Collections;

public class timerGame : MonoBehaviour {

	private static float currentTimer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentTimer += Time.deltaTime;
	}

	void OnGUI(){
		GUI.Label (new Rect(625, 280, 200, 100), "Time: " + (int)currentTimer);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public GameObject pausePanel;
	public bool isPaused;
	public int foundPieces;
	public GameObject ImageOnPanel;
	public Texture2D[] tridentPieces = new Texture2D [6]; 
	private RawImage img;



	void Start () {
		isPaused = false;
		foundPieces = 0;
		img = (RawImage)ImageOnPanel.GetComponent<RawImage> ();
		img.texture = tridentPieces[foundPieces];
	}
	
	// Update is called once per frame
	void Update () {
		//Restart with R-key
		if(Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(0); //or whatever number your scene is

		//only to check Trident Images
		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			if (foundPieces<5)
				foundPieces++;
			img.texture = tridentPieces[foundPieces];
		}
		if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			if (foundPieces>0)
				foundPieces--;
			img.texture = tridentPieces[foundPieces];
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			foundPieces = 5;
			img.texture = tridentPieces[foundPieces];
		}

		if (isPaused)
			PauseGame (true);
		else if (!isPaused)
			PauseGame(false);

		if (Input.GetKeyDown (KeyCode.P)) {
			img.texture = tridentPieces[foundPieces];
			SwitchPause ();
		}			
	}

	void PauseGame (bool state){
		if (state) { //paused
			pausePanel.SetActive(true);
			Time.timeScale = 0.0f;
		} 
		else //un-paused 
		{
			Time.timeScale = 1.0f;
			pausePanel.SetActive(false);

		}

	}

	//change Pausestate
	public void SwitchPause(){
		if (isPaused) {
			isPaused = false;
		} else {
			isPaused = true;
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public Canvas canvas;
	public GameObject trident;
	int currentSelection;
	float tridentYpos;
	int firstlevel = 1;
	int introScene = 5;

	// Use this for initialization
	void Start () {
		currentSelection = 0;
		Vector2 tridentPos = trident.GetComponent<RectTransform> ().anchoredPosition;
		tridentYpos = tridentPos.y;
		canvas = canvas.GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		//navigating through the list
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (currentSelection == 0 )
				currentSelection = 3;
			else 
				currentSelection--;
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (currentSelection == 3)
				currentSelection = 0;
			else
				currentSelection++;
		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			goToPage (currentSelection);
		}

		updateTridentPos (currentSelection);
	}

	void goToPage (int selected)
	{
		if (selected == 3)
			Application.Quit ();
		
		switch (selected) {
		case 0: //start the intro
			SceneManager.LoadScene (introScene);
			break;
		case 1:  //start endless mode
			SceneManager.LoadScene (firstlevel);
			break;
		case 2: //go to credits
			print ("Credits");
			break;		
		}


	}

	void updateTridentPos(int selectedPos)
	{
		tridentYpos = -(selectedPos * 40);
		Vector2 tridentPos = trident.GetComponent<RectTransform> ().anchoredPosition;
		trident.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (tridentPos.x, tridentYpos);
		//print (trident.GetComponent<RectTransform> ().anchoredPosition);
	}
}
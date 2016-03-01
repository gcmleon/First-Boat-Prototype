using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinText : MonoBehaviour {

	public Text winText;
	public float fadeSpeed = 1f;
	public bool endscene;
	public GameObject Canvas;
	public ParticleSystem Fireworks;


	private bool finish;

	private int firstlevel = 1;
	private int secondlevel = 2;
	private int thirdlevel = 3;
	private int _endscene = 4;
    private int sceneToLoad;

	void Awake () 

	{
		winText = Canvas.GetComponentInChildren<Text> ();
		//winText.color = Color.clear;

        // if it is the 1st level, 2nd comes
        if (SceneManager.GetActiveScene().buildIndex == firstlevel)
        {
            sceneToLoad = secondlevel;
        } else if (SceneManager.GetActiveScene().buildIndex == secondlevel)
        {
            sceneToLoad = thirdlevel;
        } else {
            sceneToLoad = firstlevel;
        }

	}

	void Update () 

	{
		//print (finish);
		ColorChange();
	}

	void OnTriggerEnter (Collider col)
	{
		if (!endscene) 
		{
			print ("player hit trigger cube");
			finish = true;

			//Fireworks.GetComponent<ParticleSystem>.enableEmission = true;
			Fireworks.enableEmission = true;


			StartCoroutine (WaitAndRestart (3F));
		}

		if (endscene) 
		{
			SceneManager.LoadScene(_endscene);	
		}

	}

	void ColorChange () 
	{
		if (finish)
		{
			//winText.color = Color.Lerp (winText.color, Color.red, fadeSpeed * Time.deltaTime);
		}
	}

	IEnumerator WaitAndRestart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		print ("waiting...");
		SceneManager.LoadScene(sceneToLoad);
	}
}

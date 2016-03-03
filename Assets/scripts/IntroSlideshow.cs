using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSlideshow : MonoBehaviour {
	public Canvas canvas;
	public RawImage ImageOnPanel;
	public Text text;
	public Texture2D[] introPics = new Texture2D [4]; 
	public string[] introTexts = new string[4];

	private RawImage img;
	private int picNumber = 0;
	private int textNumber = 0;
	private float currentTime = 0;
	// Use this for initialization
	void Start () {

		img = (RawImage)ImageOnPanel.GetComponent<RawImage> ();
		img.texture = introPics[picNumber];
		text.text = introTexts [textNumber];


	}

	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime >= 5.0 && picNumber < 4) {
			picNumber++;
			textNumber++;
			ChangeSlide (picNumber);
			currentTime = 0;
		}

		if (picNumber > 3) {
			SceneManager.LoadScene (1);
		}



	}

	void ChangeSlide(int picNumber)
	{
		//img.CrossFadeAlpha (0.3F, 1F, false);
		//text.CrossFadeAlpha (0.0F, 0.5F, false);
		//WaittoLoad (1F);
		img.texture = introPics[picNumber];
		text.text = introTexts [textNumber];


	}

	void LoadImage(int picNumber) {
		img.CrossFadeAlpha (1.0F, 0.5F, false);
		text.CrossFadeAlpha (1.0F, 0.5F, false);
	}

	IEnumerator WaittoLoad(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		LoadImage (picNumber);

	}
}

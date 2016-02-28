using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreManager : MonoBehaviour
{

    public static int score = 0;
    Text text;

    // Use this for initialization
    void Start()
    {
        // get text reference
        text = GetComponent<Text>();

    }

    public static void UpdateScore(int newScoreValue)
    {
        score += newScoreValue;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + score;
    }
}
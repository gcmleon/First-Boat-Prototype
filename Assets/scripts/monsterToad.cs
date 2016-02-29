using UnityEngine;
using System.Collections;

public class monsterToad : MonoBehaviour {

    //Code for movement taken from http://answers.unity3d.com/questions/14279/make-an-object-move-from-point-a-to-point-b-then-b.html

    public Transform farEnd;

    private Vector3 frometh;
    private Vector3 untoeth;
    private string objectName;
    private string monster = "monster";
    private string jump = "Jump";
    public float secondsForOneLength = 5f;

    // Use this for initialization
    void Start () {
        frometh = transform.position;
        untoeth = farEnd.position;
        objectName = this.gameObject.name;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(frometh, untoeth,
        Mathf.SmoothStep(0f, 1f,
        Mathf.PingPong(Time.time / secondsForOneLength, 1f)
        ));

        // Attack
        if (objectName.Contains(monster))
        {
            this.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            this.GetComponent<Animation>().CrossFade("Mon_T_Attack01");
        }

        if (objectName.Contains(jump))
        {
            this.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            this.GetComponent<Animation>().CrossFade("Mon_T_Attack");
        }

    }
		
}

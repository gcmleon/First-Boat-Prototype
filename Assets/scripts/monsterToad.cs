using UnityEngine;
using System.Collections;

public class monsterToad : MonoBehaviour {

    //Code for movement taken from http://answers.unity3d.com/questions/14279/make-an-object-move-from-point-a-to-point-b-then-b.html

    public Transform farEnd;

    private Vector3 frometh;
    private Vector3 untoeth;
    private float secondsForOneLength = 5f;

    // Use this for initialization
    void Start () {
        frometh = transform.position;
        untoeth = farEnd.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(frometh, untoeth,
        Mathf.SmoothStep(0f, 1f,
        Mathf.PingPong(Time.time / secondsForOneLength, 1f)
        ));

		//Atack
		//this.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		//this.GetComponent<Animation>().CrossFade("Mon_T_Attack01");
    }

    void OnCollisionEnter(Collision collision)
    {
        //print("I'm toad - sth collided with me: " + collision.collider.name);
        /*
        if (collision.collider.name == "Boat")
        {
            print("I'm toad - boat collided with me!");
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(explosionPrefab, pos, rot);
            Destroy(gameObject);
        }*/
        
    }
}

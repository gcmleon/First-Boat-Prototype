using UnityEngine;
using System.Collections;

public class rock : MonoBehaviour {

    private Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        //If the front of the boat (Cube 3) hits a rock
        if (col.collider.name == "Cube (3)")
        {
            print("Hit by the front of the boat");
            //rend.material.color = Color.blue;
        }
        
    }
}

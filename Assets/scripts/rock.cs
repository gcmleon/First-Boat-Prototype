using UnityEngine;
using System.Collections;

public class rock : MonoBehaviour {

    public Transform explosionPrefab;

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
        // if the front of the boat (Cube 3) hits a rock
        if (col.collider.name == "Cube (3)")
        {
            print("Rock hit by the front of the boat");
            /*ContactPoint contact = col.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(explosionPrefab, pos, rot);
            // Destroy rock
            Destroy(rend.gameObject);*/
        }

        if (col.collider.name == "Shooting_Wave(Clone)")
        {
            Destroy(col.gameObject);
        }
        
    }
}

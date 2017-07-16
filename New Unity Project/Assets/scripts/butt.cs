using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void OnCollisionEnter(Collision coll)
    {
        
        Destroy(coll.gameObject);
    }
}

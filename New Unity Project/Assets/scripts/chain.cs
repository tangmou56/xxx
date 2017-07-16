using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chain : MonoBehaviour {
    private GameObject[] target;
    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectsWithTag("Player");
        GetComponent<FixedJoint>().connectedBody = target[0].GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

       /* transform.position = target[0].transform.position + target[0].transform.forward *1;
        transform.rotation = Quaternion.Euler(target[0].transform.eulerAngles);*/

    }
}

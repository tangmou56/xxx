using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_bar : MonoBehaviour {
    public GameObject player;
    public Transform Camera;
    public float offset;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = (player.GetComponent<Rigidbody>().position+ transform.up * offset);
    }
}

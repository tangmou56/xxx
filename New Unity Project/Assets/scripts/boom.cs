using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour {
    public float boomTime = 5;
    float mytime = 0;
    public GameObject trigger;
    public GameObject light;
    public GameObject particle;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mytime += Time.deltaTime;
        if (mytime > boomTime)
        {

            GameObject clone1 = Instantiate(trigger, transform.position, Quaternion.Euler(0, 0, 0));
            GameObject clone2 = Instantiate(light, transform.position, Quaternion.Euler(0, 0, 0));
            GameObject clone3 = Instantiate(particle, transform.position+transform.up*3, Quaternion.Euler(-90, 0, 0));
            Destroy(clone1,0.2f);
            Destroy(clone2, 0.1f);
            Destroy(clone3,5.0f);
            Destroy(gameObject);
            
        }
    }
}

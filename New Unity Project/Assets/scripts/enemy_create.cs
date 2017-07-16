using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_create : MonoBehaviour {
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    static public int n_enemy = 0;
    public float rateTime = 5;
    float mytime=0;
    float time_harder=0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
            mytime += Time.deltaTime;
            time_harder += Time.deltaTime;
            if (time_harder > 45)
            {
                NewBehaviourScript.enemy_str += 0.3f;
                rateTime -= 0.5f;
                time_harder -= 45;
            }
            if (rateTime <= 0.5)
                rateTime = 0.5f;
                

            if (mytime > rateTime)
            {
                Vector2 r = Random.insideUnitCircle.normalized * 30;
                int random = Random.Range(0, 1000) % 100;
                if (n_enemy < 15)
                {
                    if (random > 50 && random <= 85)
                        Instantiate(enemy1, transform.position + new Vector3(r.x, 2, r.y), Quaternion.identity);
                    else if (random > 85 && random < 93)
                        Instantiate(enemy2, transform.position + new Vector3(r.x, 2, r.y), Quaternion.identity);
                    else if (random >= 93)
                        Instantiate(enemy3, transform.position + new Vector3(r.x, 2, r.y), Quaternion.identity);
                }
                mytime -= rateTime;
                if(random>50&&n_enemy<15)
                    n_enemy++;
                
            }
        
	}
}

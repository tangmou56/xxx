using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
   // public GameObject target;
    public float speed = 1;

    public float hp_max = 3f;
    public float hp = 3f;
    public Texture2D bloodBarRed;
    public Transform bloodBar;


    public Rigidbody rb;
    public float exp;

    public int imtime = 30;
    private int imtimecount = 0;

    public GameObject blood;
    public GameObject hpbag;
    private AudioSource[] m_ArrayMusic;
    private GameObject[] gos;
    
    public int attack = 0;
    private float time_fire=2;
    private float time_fire_count = 0;
    public GameObject bullet;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_ArrayMusic = gameObject.GetComponents<AudioSource>();
        bloodBarRed = new Texture2D(512, 32); // 新建一个贴图，用以变化后赋给Cube
        bloodBarRed.wrapMode = TextureWrapMode.Clamp; // 拉伸该图
    }

    void OnTriggerStay(Collider coll)
    {
        
        float damage = 0;
        float force = 0;
        float force_up = 0;
        if (coll.tag == "fire"||coll.tag == "ball"|| coll.tag == "boom")
        {
            if (coll.tag == "ball")
            {
                damage = 1f * NewBehaviourScript.str;

                imtime = 30;
                force = 2000f;
                force_up = 10f;
            }

            if (coll.tag == "fire")
            {
                damage = 0.05f * NewBehaviourScript.str;
                imtime = 3;
                force = 100f;
                force_up = 5f;
            }
            if (coll.tag == "boom")
            {
                damage = 10f;
                imtime = 50;
                force = 2000f;
                force_up = 10f;
            }


            if (imtimecount >= imtime)
            {

                int ran = Random.Range(0, 1000) % 3;

                rb.AddForce(Vector3.forward * force);
                rb.velocity = new Vector3(0, force_up, 0);
                hp = hp - damage;
                imtimecount = 0;
                m_ArrayMusic[ran].Play();

                if (hp <= 0)
                {
                    die(coll);

                }



            }
        }
    }


    void OnTriggerEnter(Collider coll)
    {

        float damage=0;
        float force=0;
        float force_up=0;
        if ( coll.tag == "bullet")
        {
                
            if (coll.tag == "bullet")
            {
                damage = 0.5f*NewBehaviourScript.str; 
                imtime = 3;
                force = 100f;
                force_up = 5f;
            }
                if (imtimecount>= imtime)
            {


                int ran = Random.Range(0, 1000) % 3;
               
                rb.AddForce(Vector3.forward * force);
                rb.velocity = new Vector3(0, force_up, 0);
                hp =hp-damage;
                imtimecount = 0;
                m_ArrayMusic[ran].Play();
                
                if (hp <= 0) {
                    die(coll);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        UpDateBloodBar();
        if (attack == 1)
        {
            time_fire_count+=Time.deltaTime;
            if (time_fire <= time_fire_count)
            {
                time_fire_count -= time_fire;
                fire(10);
            }
        }
        if (imtimecount < 100)
        {
            imtimecount++;
        }
        else
        {
            imtimecount = 100;
        }
        GameObject[] target =  GameObject.FindGameObjectsWithTag("Player");
        transform.LookAt(target[0].GetComponent<Rigidbody>().position);
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

    }


    public void fire(int firespeed)
    {
        GameObject clone = Instantiate(bullet, transform.position + transform.forward * 3, Quaternion.Euler(transform.eulerAngles));
        m_ArrayMusic[3].Play();
        clone.GetComponent<Rigidbody>().velocity = clone.transform.forward * firespeed;
        Destroy(clone, 5.0f);
    }

    public void die(Collider coll)
    {
        int random = Random.Range(0, 1000) % 100;
        if(random<20)
            Instantiate(hpbag, transform.position+transform.up*3, Quaternion.Euler(0,0,0));
        GameObject bloodc = Instantiate(blood, transform.position, Quaternion.Euler(coll.GetComponent<Transform>().eulerAngles));
        NewBehaviourScript.exp += exp;
        Destroy(gameObject);
        enemy_create.n_enemy--;
    }



    public void UpDateBloodBar()
    {
        float cunrrentRed = hp * bloodBarRed.width / hp_max;  //直接除，会因为两个整型得到0，所以先乘以后除
        for (var x = 0; x < bloodBarRed.width; x++)
        { // 对每个坐标点
            for (var y = 0; y < bloodBarRed.height; y++)
            {//循环执行y轴从0开始，y轴小于血条的宽的话执行下面，否则+
                if (x < cunrrentRed)
                    bloodBarRed.SetPixel(x, y, Color.red);   //x小于血条长度的范围涂成红色，
                else
                    bloodBarRed.SetPixel(x, y, Color.gray);         //其他部位涂成黑色
            }
        }
        bloodBarRed.Apply(); // 应用该图
        bloodBar.gameObject.GetComponent<Renderer>().material.mainTexture = bloodBarRed; // 将修改后的贴图给血条Cube

    }

}

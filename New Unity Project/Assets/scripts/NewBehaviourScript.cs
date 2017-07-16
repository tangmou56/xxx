using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public Camera cam;
    // We need to actually hit an object
    RaycastHit hitt = new RaycastHit();

    public float speed=6;
    //public float rotate=100;
    public float jumpspeed = 5;
    private Rigidbody rb;
    private float distToGround;
    private AudioSource audio;
    public float dodgetime = 50f;
    public float dodgespeed = 12;
    private float dodgecount = 0f;
    private bool shooting = false;
    private int imtime=50;
    private int imcount = 0;
    public GameObject hammer;
    public GameObject bullet;
    public GameObject bullet_light;
    public GameObject fire;
    public GameObject boom;
    public GameObject levelup;
    public GameObject hpbag_particle;
    private AudioSource[] m_ArrayMusic;
    
    public float hp_max = 10;
    public float hp = 10;
    static public float str = 2;
    static public int level = 1;
    static public float exp = 0;
    static public float enemy_str=1;
    static public int weapon = 1;
    public float exp_max = 100;

    private GameObject fire_clone=null;


    public Texture2D bloodBarRed;
    public Transform bloodBar;

    public Texture2D expBarRed;
    public Transform expBar;


    private GameObject clone_hammer;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        distToGround = GetComponent<Renderer>().bounds.extents.y;
        m_ArrayMusic = gameObject.GetComponents<AudioSource>();
        bloodBarRed = new Texture2D(512, 32); // 新建一个贴图，用以变化后赋给Cube
        bloodBarRed.wrapMode = TextureWrapMode.Clamp; // 拉伸该图
        expBarRed = new Texture2D(512, 32); // 新建一个贴图，用以变化后赋给Cube
        expBarRed.wrapMode = TextureWrapMode.Clamp; // 拉伸该图

}
    
   

    // Update is called once per frame


    public bool grounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void OnTriggerStay(Collider coll)
    {

        if (coll.tag == "hpbag"&&hp!=hp_max )
        {
            Destroy(coll.gameObject);
            GameObject clone = Instantiate(hpbag_particle, transform.position , Quaternion.Euler(transform.eulerAngles));
            Destroy(clone,5f);
            hp ++;
            if (hp>hp_max)
            {
                hp = hp_max;
            }

        }
        if (coll.tag == "drop_ball")
        {
            //clone_hammer=Instantiate(hammer, transform.position+transform.forward*1, Quaternion.Euler(transform.eulerAngles));
            weapon = 0;
            Destroy(coll.gameObject);

        }


        if (coll.tag == "bullet_enemy"|| coll.tag == "enemy"|| coll.tag == "boom")
        {
            if (imtime == imcount)
            {
                
                
                rb.AddForce(coll.GetComponent<Transform>().forward * 3000);
                rb.velocity = new Vector3(0, 10, 0);
                hp=hp-1*enemy_str;
                imcount = 0;
                
            }
        }
        if (hp <= 0)
        {
            print("dead");
            Destroy(gameObject);
        }

    }
    bool at_fire=false;
    bool at_boom = false;



    void Update () {


        UpDateBloodBar();
        UpDateExpBar();



        if (exp >= exp_max)
            levelUP();



        imcount++;
        if (imcount > imtime)
            imcount = imtime;


        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitt, 100);
        //    Debug.DrawLine(cam.transform.position, ray.direction,Color.red);
        if (null != hitt.transform)
        {


            transform.LookAt(new Vector3(hitt.point.x, 0, hitt.point.z));
        }


        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        if (Input.GetKey(KeyCode.W) && !audio.isPlaying)  //有移动键按下并且声音并不是播放状态
        {
            audio.Play();                                                         //播放声音
        }
        if (!Input.anyKey && audio.isPlaying)                         //没有任何键按下并且声音是播放状态
        {
            audio.Stop();                                                         //停止播放声音
        }


        if (Input.GetKey(KeyCode.W))
        {
          //  transform.eulerAngles = new Vector3(0, 0, 0);
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
           // transform.position += transform.forward * speed * Time.deltaTime;
        }
        



        if (Input.GetKey(KeyCode.S))
        {
            //transform.position -= transform.forward * speed * Time.deltaTime;
            transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        //    transform.eulerAngles= new Vector3(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Rotate (Vector3.up * Time.deltaTime*-rotate);
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
           // transform.eulerAngles = new Vector3(0, -90, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
           // transform.eulerAngles = new Vector3(0, 90, 0);
            //transform.Rotate(Vector3.up * Time.deltaTime * rotate);
        }


        /*      if (Input.GetKey(KeyCode.W)&& Input.GetKey(KeyCode.D))
          {
              transform.eulerAngles = new Vector3(0, 45, 0);
          }
          if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
          {
              transform.eulerAngles = new Vector3(0, -45, 0);
          }
          if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
          {
              transform.eulerAngles = new Vector3(0, 135, 0);
          }
          if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
          {
              transform.eulerAngles = new Vector3(0, -135, 0);
          }*/


        if (dodgecount > dodgetime)
        {
            dodgecount = dodgetime;
        }


            if (Input.GetKey(KeyCode.LeftShift))
        {
            if (dodgecount > 0)
            {

                //Vector3 disPos = transform.forward * 200;
                //transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * 0.1f);
                speed = dodgespeed;
                dodgecount--;
            }
            if (dodgecount <= 0)
            {
                speed = dodgespeed / 2;
            }
        }
        else
        {
            speed = dodgespeed / 2;
            dodgecount = dodgecount + 0.5f;
        }


        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded())
            {
                //rb.AddForce(Vector3.up * jumpspeed);
                rb.velocity = new Vector3(0, jumpspeed, 0);

            }


        }


        if (weapon != 0)
        {
            if (clone_hammer != null)
                Destroy(clone_hammer);
        }




        if (Input.GetKey(KeyCode.Mouse0)&&weapon==1)
        {
            if (!shooting)
            {
                GameObject clone_light = Instantiate(bullet_light, transform.position + transform.forward * 1, Quaternion.Euler(transform.eulerAngles));
                Destroy(clone_light, 0.08f);

                GameObject clone = Instantiate(bullet, transform.position + transform.forward * 3, Quaternion.Euler(transform.eulerAngles));

                m_ArrayMusic[1].Play();
                clone.GetComponent<Rigidbody>().velocity = clone.transform.forward * 50;
                Destroy(clone, 1.0f);
                shooting = true;
            }

        }
        else
        {
            shooting = false;
        }

        if (Input.GetKey(KeyCode.Mouse0)&&weapon==2)
        {
            if (!at_fire)
            {
                fire_clone = Instantiate(fire, transform.position + transform.forward * 2, Quaternion.Euler(transform.eulerAngles));
                at_fire = true;

           
            }
            else
            {
                fire_clone.GetComponent<Transform>().position = transform.position + transform.forward * 2;
                fire_clone.GetComponent<Transform>().rotation = Quaternion.Euler(transform.eulerAngles);
            }

        }
        else
        {
            
            at_fire = false;
            if (fire_clone != null)
            {
                Destroy(fire_clone);
                fire_clone = null;
            }
        }

        if (Input.GetKey(KeyCode.F))
        {
            if (!at_boom)
            {
                GameObject clone_boom = Instantiate(boom, transform.position + transform.forward * 3, Quaternion.Euler(transform.eulerAngles));
                m_ArrayMusic[2].Play();
                clone_boom.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
                clone_boom.GetComponent<Rigidbody>().velocity = clone_boom.transform.forward * 15;
                at_boom = true;
            }

        }
        else
        {
            at_boom = false;
        }
    



    }


    public void levelUP()
    {
        GameObject clone = Instantiate(levelup, transform.position + transform.up * 3, Quaternion.identity);
        Destroy(clone, 6.0f);
        hp_max += 3;
        hp += 3;
        str++;
        level++;
        exp = exp - exp_max;
        exp_max = 100;
        for (int i = 1; i < level; i++)
            exp_max = exp_max * 1.3f;
        
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
    public void UpDateExpBar()
    {
        float cunrrentRed = exp * expBarRed.width / exp_max;  //直接除，会因为两个整型得到0，所以先乘以后除
        for (var x = 0; x < expBarRed.width; x++)
        { // 对每个坐标点
            for (var y = 0; y < expBarRed.height; y++)
            {//循环执行y轴从0开始，y轴小于血条的宽的话执行下面，否则+
                if (x < cunrrentRed)
                    expBarRed.SetPixel(x, y, Color.yellow);   //x小于血条长度的范围涂成红色，
                else
                    expBarRed.SetPixel(x, y, Color.gray);         //其他部位涂成黑色
            }
        }
        expBarRed.Apply(); // 应用该图
        expBar.gameObject.GetComponent<Renderer>().material.mainTexture = expBarRed; // 将修改后的贴图给血条Cube

    }

}

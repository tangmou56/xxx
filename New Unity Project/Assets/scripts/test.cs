using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    public Camera cam;
    // We need to actually hit an object
    RaycastHit hitt = new RaycastHit();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitt, 100);
        //    Debug.DrawLine(cam.transform.position, ray.direction,Color.red);
        if (null != hitt.transform)
        {
            print(hitt.point);//鼠标点击的坐标
        }
    }
}

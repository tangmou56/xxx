using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour
{
    public Transform target;
    public float smothing = 5f;
    Vector3 offset;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCampos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCampos, smothing * Time.deltaTime);//摄像机自身位置到目标位置
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood : MonoBehaviour {
    private AudioSource[] m_ArrayMusic;
    // Use this for initialization
    void Start () {
        Destroy(gameObject,6);
        m_ArrayMusic = gameObject.GetComponents<AudioSource>();
        m_ArrayMusic[0].Play();
        
    }
	



}

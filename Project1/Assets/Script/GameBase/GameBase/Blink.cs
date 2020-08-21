using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

    public GameObject go;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //blink
        //Renderer renderer = GetComponent<Renderer>();
        if (go != null)
        {
            if (Time.fixedTime % 0.5f < 0.2f) { go.SetActive(true); }
            else { go.SetActive(false); }
        }
    }
}

/*
	void Update () 
    {
        //blink
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            if (Time.fixedTime % 0.5f < 0.2f) { renderer.enabled = false; }
            else { renderer.enabled = true; }
        }
    }
*/

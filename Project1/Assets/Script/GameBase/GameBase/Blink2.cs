using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //blink
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            if (Time.fixedTime % 0.5f < 0.2f) { renderer.enabled = false; }
            else { renderer.enabled = true; }
        }
    }
}

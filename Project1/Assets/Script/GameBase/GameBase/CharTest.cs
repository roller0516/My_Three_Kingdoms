using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTest : MonoBehaviour {

    Animation curAnimation = null;

    // Use this for initialization
    void Start ()
    {
        curAnimation = this.GetComponentInChildren<Animation>();
        curAnimation.Play("Stand");
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            curAnimation.Play("Run");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            curAnimation.Play("Attack01");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            curAnimation.Play("Damage");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            curAnimation.Play("Death");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStar : MonoBehaviour {
    
   
	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Throwable")
        {

            gameObject.SetActive(false);
          
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

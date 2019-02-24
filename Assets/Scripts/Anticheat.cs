using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Anticheat : MonoBehaviour {
    public Material playMaterial;
    public Material cheatMaterial;
    public Material originalMaterial;

    public bool outsidePlatform = false;
    public bool ballOutside = false;
    public bool handOutside = false;

    public GameObject ball;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
         //ball.GetComponent<Renderer>().material = playMaterial;

    }

    public void Cheat()
    {
        
        ball.GetComponent<Renderer>().material = cheatMaterial;
        Collider col = ball.GetComponent<SphereCollider>();
        col.enabled = false;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LeftHand")
        {
            outsidePlatform = false;
            handOutside = false;
            
           }
        


        if (other.gameObject.CompareTag("Throwable"))
        {
            ballOutside = false;
            Collider col = ball.GetComponent<SphereCollider>();
            col.enabled = true;
            other.GetComponent<Renderer>().material = originalMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.name == "LeftHand")
          {
            //outsidePlatform = true;
            handOutside = true;
         }
         
        

        if (other.gameObject.CompareTag("Throwable"))
        {
            ballOutside = true;
        }

        if (!handOutside && ballOutside)
        {
            Play();

        }
        if (!handOutside && !ballOutside)
        {
            Play();
        }
        if (handOutside && ballOutside)
        {
            outsidePlatform = true;
            Cheat();


        }



    }

}



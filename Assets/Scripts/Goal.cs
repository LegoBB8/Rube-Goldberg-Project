using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
   public SteamVR_LoadLevel loadLevel;
    public BallReset ballReset;
    public float tweakPosition = 0.00000001f;
    //public GameObject ball;
   // public Material cheatMaterial;
    public Anticheat anticheat;
    public string sceneToLoad;
    public UnloadLevel unloadLevel;
    public GameObject[] throwObjects;
    public GameObject ballObject;
    public GameObject player;


    // public string previousScene;


    // Use this for initialization
    void Start () {
       
    }

  

    void OnTriggerEnter(Collider other)
    {
        //if cheat return
        if (anticheat.outsidePlatform == true)
          return; 
     

        if (other.gameObject.tag == "Throwable" && ballReset.AllStarsCollected())
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (!rb.isKinematic)
            {
                rb.isKinematic = true;
            }
            other.gameObject.transform.position = transform.position + (transform.up * tweakPosition);

            throwObjects = GameObject.FindGameObjectsWithTag("Structure");
            foreach (GameObject item in throwObjects)
            {
                print("Item: " + item);
                Destroy(item);
            }

            ballObject = GameObject.FindGameObjectWithTag("Throwable");
            Destroy(ballObject);

           

            SteamVR_LoadLevel.Begin(sceneToLoad);

            player = GameObject.FindGameObjectWithTag("Player");
            Destroy(player);

            //unloadLevel.changingLevel();
        }
    }


   

    // Update is called once per frame
    void Update () {

    }
}

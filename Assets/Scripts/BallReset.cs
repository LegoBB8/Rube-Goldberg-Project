using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{



    Vector3 originalPos;
    Vector3 initialVelocity;
    Vector3 initialAngularVelocity;
    private Rigidbody rb;
    public List<GameObject> stars = new List<GameObject>();
    public GameObject storeCollectibles;


    // Use this for initialization
    void Start()
    {
        originalPos = gameObject.transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        initialVelocity = rb.velocity;
        initialAngularVelocity = rb.angularVelocity;

        
            foreach (Transform child in storeCollectibles.transform)
            {
            if (child.gameObject.CompareTag("Star"))
            {
                stars.Add(child.gameObject);
            }
             

            }

        }

    public bool AllStarsCollected()
    {
        int starsCollected = 0;
        foreach (GameObject collectible in stars)
        {
            if (!collectible.activeSelf)
            {
                starsCollected++;
            }
        }
        return starsCollected == stars.Count;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Ground")
        {

            gameObject.transform.position = originalPos;
            rb.velocity = initialVelocity;
            rb.angularVelocity = initialAngularVelocity;
            foreach (GameObject collectible in stars)
            {
                collectible.SetActive(true);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
  
}

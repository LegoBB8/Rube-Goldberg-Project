using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTrampoline : MonoBehaviour {
    public float bounceAmount;
    public bool bounce;
    public Transform ball;
    private Rigidbody rb;

    // Use this for initialization

    void Start () {
        bounceAmount = 5;
        bounce = false;
        rb = ball.GetComponent<Rigidbody>();

    }



 
void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Throwable")
        {
            bounce = true;
        }
    }

   
    // Update is called once per frame
    void Update () {
        if (bounce)
        {
            Vector3 v = rb.velocity;
            v.y = 0;
            rb.velocity = v;
            rb.AddForce(0, bounceAmount, 0, ForceMode.Impulse);
            bounce = false;
        }
    }
}

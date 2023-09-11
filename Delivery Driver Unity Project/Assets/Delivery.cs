using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Ouch");

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // if what we trigger is the package, print 'package picked up" to console

        if (other.tag == "Package")
        {
            Debug.Log("package picked up");
        }

        if (other.tag == "Customer")
        {
            Debug.Log("delivered package");
        }
    }        

}

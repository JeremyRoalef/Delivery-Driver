using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{

    bool boolHasPackage; //bool logic starts off as false unless otherwise assigned
    [SerializeField]float fltDestroyDelay = 1f;

    void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Ouch");

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // if what we trigger is the package, print 'package picked up" to console

        if (other.tag == "Package" && !boolHasPackage)  //! means 'not'
        {
            Debug.Log("package picked up");
            boolHasPackage = true;
            Destroy(other.gameObject, fltDestroyDelay);
        }

        if (other.tag == "Customer" && boolHasPackage) //bool in if statements auto assigned to true
        {
            Debug.Log("delivered package");
            boolHasPackage = false;
        }
    }        

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{

    [SerializeField] Color32 clrHasPackage = new Color32(1,1,1,1); //using Color32 to change the color of our sprite.
    [SerializeField] Color32 clrNoPackage = new Color32(0,0,0,0);  //note: Color32 uses 0-255 values, not 0-1 as the video does


    bool boolHasPackage; //bool logic starts off as false unless otherwise assigned
    [SerializeField]float fltDestroyDelay = 1f;

    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>(); //getting a component (spriterenderer) & storing it in a variable
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Ouch");

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // if what we trigger is the package, print 'package picked up" to console

        if (other.tag == "Package" && !boolHasPackage)  //! means 'not'
        {
            spriteRenderer.color = clrHasPackage;
            Debug.Log("package picked up");
            boolHasPackage = true;
            Destroy(other.gameObject, fltDestroyDelay);

        }

        if (other.tag == "Customer" && boolHasPackage) //bool in if statements auto assigned to true
        {
            spriteRenderer.color = clrNoPackage;
            Debug.Log("delivered package");
            boolHasPackage = false;
        }
    }        

}

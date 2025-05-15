using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Attributes")]
    [SerializeField] 
    float fltSteerSpeed = 300;
    
    [SerializeField]
    float fltMoveSpeed = 10f;
    
    [SerializeField]
    float fltSlowSpeed = 5f;
    
    [SerializeField]
    float fltBoostSpeed = 20f;


    [Header("Package Attributes")]
    [SerializeField]
    Color32 clrHasPackage = new Color32(1, 1, 1, 1);

    [SerializeField]
    Color32 clrNoPackage = new Color32(0, 0, 0, 0);


    [Header("Misc")]
    [SerializeField]
    float fltForce = 20f;

    SpriteRenderer spriteRenderer;

    float fltSteerAmount;
    float fltMoveAmount;
    bool boolHasPackage;
    public bool BoolHasPackage
    {
        get { return boolHasPackage; }
    }

    const string SPEED_UP_TAG_STRING = "SpeedUp";
    const string CUSTOMER_TAG_STRING = "Customer";

    private void Awake()
    {
        //Get components
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //player controls
        MovePlayer();
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        //Get the horizontal (a,d, left arrow, right arrow) input
        fltSteerAmount = Input.GetAxis("Horizontal") * fltSteerSpeed * Time.deltaTime;

        //Rotate about z-axis to change where the player is looking
        transform.Rotate(0, 0, -fltSteerAmount);
    }

    private void MovePlayer()
    {
        //Get the vertical (w, s, up arrow, down arrow) input
        fltMoveAmount = Input.GetAxis("Vertical") * fltMoveSpeed * Time.deltaTime;

        //Move player forward based on the given input (local position)
        transform.Translate(0, fltMoveAmount, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            //Make player fast
            case SPEED_UP_TAG_STRING:
                SetSpeed(fltBoostSpeed);
                break;
            //Give customer package
            case CUSTOMER_TAG_STRING:
                if (boolHasPackage) GiveCustomerPackage();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Ouch");
        SetSpeed(fltSlowSpeed);
        
        if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            Debug.Log("Applying force");
            rb.AddForce(transform.up * fltForce);
        }
    }

    private void SetSpeed(float fltNewSpeed)
    {
        fltMoveSpeed = fltNewSpeed;
    }

    private void GiveCustomerPackage()
    {
        spriteRenderer.color = clrNoPackage;
        Debug.Log("delivered package");
        boolHasPackage = false;
    }

    public void Pickup()
    {
        if (!boolHasPackage)
        {
            spriteRenderer.color = clrHasPackage;
            Debug.Log("package picked up");
            boolHasPackage = true;
        }
    }

    public void Deliver()
    {
        Debug.Log("Package delivered");
        boolHasPackage = false;
    }
}
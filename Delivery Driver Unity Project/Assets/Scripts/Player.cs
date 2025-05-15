using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Attributes")]
    [SerializeField] 
    float fltSteerSpeed = 300;
    
    [SerializeField]
    float fltDefaultSpeed = 10f;
    
    [SerializeField]
    float fltSlowSpeed = 5f;
    
    [SerializeField]
    float fltBoostSpeed = 20f;

    [SerializeField]
    float fltBoostDuration = 3f;

    [Header("Package Attributes")]
    [SerializeField]
    Color32 clrHasPackage = new Color32(1, 1, 1, 1);

    [SerializeField]
    Color32 clrNoPackage = new Color32(0, 0, 0, 0);

    [SerializeField]
    float timeIncreaseFromDelivery = 7.5f;

    [Header("Misc")]
    [SerializeField]
    float fltForce = 20f;

    [SerializeField]
    PlayerHealth playerHealth;

    [SerializeField]
    UIHandler uiHandler;

    [SerializeField]
    int baseScore = 500;

    SpriteRenderer spriteRenderer;
    Score playerScore;

    float fltMoveSpeed;
    float fltSteerAmount;
    float fltMoveAmount;
    bool boolHasPackage;

    public bool BoolHasPackage
    {
        get { return boolHasPackage; }
    }

    const string SPEED_UP_TAG_STRING = "SpeedUp";
    const string CUSTOMER_TAG_STRING = "Customer";
    const string BOUNDARY_TAG_STRING = "Boundary";

    private void Awake()
    {
        //Get components
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSpeed(fltDefaultSpeed);

        playerScore = new Score(baseScore);
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
        if (other.gameObject.CompareTag(BOUNDARY_TAG_STRING)) { return; }
        Debug.Log("Ouch");
        SetSpeed(fltSlowSpeed);
        HandleHealth();
        playerScore.IncrementCrash();

        if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            Debug.Log("Applying force");
            rb.AddForce(transform.up * fltForce);
        }
    }

    private void OnDestroy()
    {
        //Death logic
        playerScore.AddGameDuration(uiHandler.GetGameDuration());
        uiHandler.ShowDeathPanel(playerScore);
        Debug.Log($"Player Score: {playerScore.GetFinalScore()}");
    }

    private void HandleHealth()
    {
        playerHealth.CurrentHealth--;
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
            SetColor(clrHasPackage);
            Debug.Log("package picked up");
            boolHasPackage = true;
        }
    }

    private void SetColor(Color32 newColor)
    {
        spriteRenderer.color = newColor;
    }

    public void Deliver()
    {
        uiHandler.CurrentTime += timeIncreaseFromDelivery;
        playerHealth.CurrentHealth++;
        Debug.Log("Package delivered");
        SetColor(clrNoPackage);
        boolHasPackage = false;
        SetSpeed(fltBoostSpeed);
        playerScore.IncrementDeliveries();
        Invoke("HandleBoostSpeedLoss", fltBoostDuration);
    }

    void HandleBoostSpeedLoss()
    {
        //If the player crashed in between the speed up and slow down
        //Dont change the slow speed
        if (fltMoveSpeed == fltSlowSpeed) return;

        SetSpeed(fltDefaultSpeed);
    }
}
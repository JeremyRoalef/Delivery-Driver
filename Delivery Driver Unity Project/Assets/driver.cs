using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driver : MonoBehaviour
{
    //note to self: don't assign variables in Start()
    [SerializeField] float fltSteerSpeed = 300;
    [SerializeField] float fltMoveSpeed = 10f;
    [SerializeField] float fltSlowSpeed = 5f;
    [SerializeField] float fltBoostSpeed = 20f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float fltSteerAmount = UnityEngine.Input.GetAxis("Horizontal") * fltSteerSpeed * Time.deltaTime;
        float fltMoveAmount = UnityEngine.Input.GetAxis("Vertical") * fltMoveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -fltSteerAmount);
        transform.Translate(0, fltMoveAmount, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpeedUp")
        {
            fltMoveSpeed = fltBoostSpeed;
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        fltMoveSpeed = fltSlowSpeed;
    }
}


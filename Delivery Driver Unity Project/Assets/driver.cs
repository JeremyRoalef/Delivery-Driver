using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driver : MonoBehaviour
{
    //note to self: don't assign variables in Start()
    [SerializeField]  float fltSteerSpeed = 1f;
    [SerializeField]  float fltMoveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, fltSteerSpeed);
        transform.Translate(0, fltMoveSpeed, 0);
    }
}


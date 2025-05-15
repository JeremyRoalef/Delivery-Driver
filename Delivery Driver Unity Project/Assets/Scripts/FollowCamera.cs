using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] 
    GameObject player;

    float fltCameraZPos;

    private void Awake()
    {
        fltCameraZPos = transform.position.z;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        if (player == null)
        {
            Debug.Log("No Player. Deleting script");
            Destroy(this);
        }
    }

    void LateUpdate()
    {
       transform.position = player.transform.position + new Vector3 (0, 0, fltCameraZPos);
    }
}

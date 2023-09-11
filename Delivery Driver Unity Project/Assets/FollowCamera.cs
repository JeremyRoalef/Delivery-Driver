using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] GameObject thingToFollow;
    //Camera's position should be same as car's position
    //change Update() to LateUpdate() Unity engine performs tasks in specific order. correct order creates smooth gameplay

    void LateUpdate()
    {
       //set camera position to thingToFollow. thingToFollow is set to car object in Unity.
       //new Vector3 adds -10 to the camera's z axis. this is so the camera shows the environment instead of showing a blank screen.
       transform.position = thingToFollow.transform.position + new Vector3 (0,0,-10);
    }
}

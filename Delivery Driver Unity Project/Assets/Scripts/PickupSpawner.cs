using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField]
    int intMaxNumOfPickupsInScene = 10;

    [SerializeField]
    Transform[] pickupSpawnPoints;

    [SerializeField]
    GameObject pickupPrefab;

    [SerializeField]
    float fltPickupSpawnDelay;


    //Local attributes
    Dictionary<Transform, GameObject> pickupsInScene = new Dictionary<Transform, GameObject>();



    private void Awake()
    {
        for (int i = 0; i < intMaxNumOfPickupsInScene; i++)
        {
            if (i >= pickupSpawnPoints.Length) { break; }
            //Create new pickup
            GameObject newPickup = Instantiate(
                pickupPrefab, 
                pickupSpawnPoints[i].transform.position, 
                Quaternion.identity);

            //Set pickup parent
            newPickup.transform.parent = transform;

            //Add pickup position-gameobject pair to dictionary
            pickupsInScene.Add(pickupSpawnPoints[i].transform, newPickup);
        }
    }

    private void Start()
    {
        InitializePickups();

        StartCoroutine(SpawnPickup());
    }

    void InitializePickups()
    {
        foreach (KeyValuePair<Transform, GameObject> keyValuePair in pickupsInScene)
        {
            keyValuePair.Value.SetActive(true);
        }
    }

    IEnumerator SpawnPickup()
    {
        while (true)
        {
            yield return new WaitForSeconds(fltPickupSpawnDelay);
            Debug.Log("Spawning pickup");

            //Get random pickup spawn point
            int randomPickup = Random.Range(0, pickupsInScene.Count + 1);

            //Iterate through pickups in scene until reaching the random number
            int iterator = 0;
            foreach (KeyValuePair<Transform, GameObject> keyValuePair in pickupsInScene)
            {
                if (iterator != randomPickup)
                {
                    iterator++;
                    continue;
                }

                //Set pickup active
                Debug.Log(keyValuePair.Value.name);
                keyValuePair.Value.SetActive(true);
                break;
            }
        }
    }
}

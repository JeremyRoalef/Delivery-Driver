using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySpawner : MonoBehaviour
{
    [SerializeField]
    int intMaxNumOfDeliveriesInScene = 10;

    [SerializeField]
    Transform[] deliverySpawnPoints;

    [SerializeField]
    GameObject deliveryPrefab;

    [SerializeField]
    float fltDeliverySpawnDelay;


    //Local attributes
    Dictionary<Transform, GameObject> deliveriesInScene = new Dictionary<Transform, GameObject>();



    private void Awake()
    {
        for (int i = 0; i < intMaxNumOfDeliveriesInScene; i++)
        {
            if (i >= deliverySpawnPoints.Length) { break; }
            //Create new pickup
            GameObject newPickup = Instantiate(
                deliveryPrefab,
                deliverySpawnPoints[i].transform.position,
                Quaternion.identity);

            //Set pickup parent
            newPickup.transform.parent = transform;

            //Add pickup position-gameobject pair to dictionary
            deliveriesInScene.Add(deliverySpawnPoints[i].transform, newPickup);
        }
    }

    private void Start()
    {
        InitializePickups();

        StartCoroutine(SpawnPickup());
    }

    void InitializePickups()
    {
        foreach (KeyValuePair<Transform, GameObject> keyValuePair in deliveriesInScene)
        {
            keyValuePair.Value.SetActive(true);
        }
    }

    IEnumerator SpawnPickup()
    {
        while (true)
        {
            yield return new WaitForSeconds(fltDeliverySpawnDelay);
            Debug.Log("Spawning pickup");

            //Get random pickup spawn point
            int randomPickup = Random.Range(0, deliveriesInScene.Count + 1);

            //Iterate through pickups in scene until reaching the random number
            int iterator = 0;
            foreach (KeyValuePair<Transform, GameObject> keyValuePair in deliveriesInScene)
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

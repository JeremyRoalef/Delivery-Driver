using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SpawnPointHider : MonoBehaviour
{
    [SerializeField]
    GameObject[] parentSpawnPointHolders;

    [SerializeField]
    bool toggleVisibility = false;

    void Update()
    {
        if (!Application.isPlaying && toggleVisibility)
        {
            toggleVisibility = false;
            ToggleVisibility();
        }
        else if (Application.isPlaying)
        {
            HideSpawnPoints();
        }
}

    void HideSpawnPoints()
    {
        foreach (GameObject spawnPointHolder in parentSpawnPointHolders)
        {
            foreach (Transform child in spawnPointHolder.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void ToggleVisibility()
    {
        foreach (GameObject spawnPointHolder in parentSpawnPointHolders)
        {
            foreach (Transform child in spawnPointHolder.transform)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
    }
}

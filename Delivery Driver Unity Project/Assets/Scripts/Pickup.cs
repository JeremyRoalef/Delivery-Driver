using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    float fltDestroyDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<Player>(out Player player))
        {
            if (player.BoolHasPackage) { return; }
            player.Pickup();
            gameObject.SetActive(false);
        }
    }
}

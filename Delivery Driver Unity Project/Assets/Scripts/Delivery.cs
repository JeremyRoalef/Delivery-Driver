using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (!player.BoolHasPackage) return;
            player.Deliver();
            gameObject.SetActive(false);
        }
    }
}

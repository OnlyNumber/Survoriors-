using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AmmoItem : NetworkBehaviour, IPickupItem
{
    public void DoOnPickUp()
    {
        Runner.Despawn(GetComponent<NetworkObject>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == IPickupItem.PLAYER_LAYER)
        {
            collision.GetComponentInChildren<WeaponNetwork>().TakeAmmo();

            DoOnPickUp();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BombItem : NetworkBehaviour, PickupItem
{
    [SerializeField]
    private float _bombRadius;

    public void DoOnPickUp()
    {

        Collider2D[] _hits = Physics2D.OverlapCircleAll(transform.position, _bombRadius, PickupItem.ENEMY_LAYER);

        foreach(var hit in _hits)
        {

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == PickupItem.PLAYER_LAYER)
        {
            DoOnPickUp();
        }
    }


}

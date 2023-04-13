using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BombItem : NetworkBehaviour, IPickupItem
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float _bombRadius;

    public void DoOnPickUp()
    {

        Collider2D[] _hits = Physics2D.OverlapCircleAll(transform.position, _bombRadius);

        //Debug.Log(_hits.Length);

        foreach (var hit in _hits)
        {
            if(hit.gameObject.layer == IPickupItem.ENEMY_LAYER)
            hit.GetComponent<IDamageAble>().TakeDamage(damage);

        }

        Runner.Despawn(GetComponent<NetworkObject>());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == IPickupItem.PLAYER_LAYER)
        {
            DoOnPickUp();
        }
    }


}

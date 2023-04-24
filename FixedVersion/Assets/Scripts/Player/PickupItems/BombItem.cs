using UnityEngine;
using Fusion;

public class BombItem : NetworkBehaviour, IPickupItem
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float _bombRadius;

    private PlayerScore playerScr = null;

    public void DoOnPickUp()
    {

        Collider2D[] _hits = Physics2D.OverlapCircleAll(transform.position, _bombRadius);

        //Debug.Log(_hits.Length);

        foreach (var hit in _hits)
        {
            if(hit.gameObject.layer == IPickupItem.ENEMY_LAYER)
            hit.GetComponent<IDamageAble>().TakeDamage(damage, playerScr);

        }

        Runner.Despawn(GetComponent<NetworkObject>());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == IPickupItem.PLAYER_LAYER)
        {
            playerScr = collision.gameObject.GetComponent<PlayerScore>();

            DoOnPickUp();
            
        }
    }


}

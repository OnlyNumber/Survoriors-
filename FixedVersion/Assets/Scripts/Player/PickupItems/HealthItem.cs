using UnityEngine;
using Fusion;

public class HealthItem : NetworkBehaviour, IPickupItem
{
    [SerializeField]
    private int _healthValue;

    public void DoOnPickUp()
    {

        Runner.Despawn(GetComponent<NetworkObject>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == IPickupItem.PLAYER_LAYER)
        {

            collision.GetComponent<IDamageAble>().TakeDamage(-_healthValue);

            DoOnPickUp();

        }
    }


}

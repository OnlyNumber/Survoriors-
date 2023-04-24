using UnityEngine;

public class Zombie : EnemyNetwork
{
    [SerializeField]
    private float radius;

    private const int PLAYER_LAYER = 7;

    protected override void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach(var hit in hits)
        {
            if(hit.gameObject.layer == PLAYER_LAYER)
            {
                hit.GetComponent<IDamageAble>().TakeDamage(damage);
            }
        }
    }


    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (GetDistance() != -1 && GetDistance() < distanceForAttack && _isCanAttack)
        {
            Attack();

            StartCoroutine(AttackReload(_timeBetweenAttack));
        }
    }

}

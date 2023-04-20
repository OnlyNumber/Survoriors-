using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Skeleton : EnemyNetwork
{
    [SerializeField]
    private NetworkObject _bulletPrefab;

    protected override void Attack()
    {
        Vector3 direction = Target.transform.position - transform.position;

        direction.z = 0;

        Runner.Spawn(_bulletPrefab, transform.position, null, null,(runner, bullet) =>
        {
            bullet.GetComponent<BulletNetwork>().InitializeDamage(damage, direction);
        });
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        //Debug.Log("Updating");

        if(GetDistance() != -1 && GetDistance() < distanceForAttack && _isCanAttack )
        {
            Attack();

            StartCoroutine(AttackReload(_timeBetweenAttack));
        }
    }

}

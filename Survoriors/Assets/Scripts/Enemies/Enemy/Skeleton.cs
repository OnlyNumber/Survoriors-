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

        if(Vector2.Distance(Target.transform.position, transform.position) < distanceForAttack && _isCanAttack )
        {
            Attack();

            StartCoroutine(AttackReload(_timeBetweenAttack));
        }
    }

}

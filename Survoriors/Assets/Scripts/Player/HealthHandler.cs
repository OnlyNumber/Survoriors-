using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class HealthHandler :  NetworkBehaviour, IDamageAble
{
    [SerializeField]
    private int _maxhealthPoints;

    public int HealthPoints
    {
        get
        {
            return _healthPoints;
        }
        private set
        {
            _healthPoints = value;
            if(_healthPoints > _maxhealthPoints)
            {
                _healthPoints = _maxhealthPoints;
            }

            if (_healthPoints < 0)
            {
                Die();
            }

        }

    }

    [SerializeField]
    private int _healthPoints;

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void Rpc_RequestChangehealth(int damage, RpcInfo info = default)
    {
        HealthPoints -= damage;

        Debug.Log("health " + HealthPoints);

    }

    public void TakeDamage(int dmg)
    {
        Rpc_RequestChangehealth(dmg);
    }

    private void Die()
    {
        Runner.Despawn(GetComponent<NetworkObject>());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;


public class HealthHandler :  NetworkBehaviour, IDamageAble
{
    [SerializeField]
    private int _maxhealthPoints;

    public delegate void OnTakeDamage();

    public event OnTakeDamage takeDamageEvent;

    public int HealthPoints
    {
        get
        {
            return _healthPoints;
        }
        private set
        {
            //Debug.Log($"on damage event damage: {value} health {_healthPoints}");


            

            




            if (value < _healthPoints)
            {

                takeDamageEvent?.Invoke();
            }

            _healthPoints = value;

            if (_healthPoints > _maxhealthPoints)
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

       // Debug.Log("health " + HealthPoints);

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

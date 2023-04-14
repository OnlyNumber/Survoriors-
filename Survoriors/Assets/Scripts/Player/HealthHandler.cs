using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;


public class HealthHandler :  NetworkBehaviour, IDamageAble
{
    [SerializeField]
    public int _maxhealthPoints { get; private set; }

    public delegate void OnTakeDamage();

    public event OnTakeDamage takeDamageEvent;

    public event OnTakeDamage onChangeHealth;

    [SerializeField]
    private NetworkObject corpse;

    public int HealthPoints
    {
        get
        {
            return _healthPoints;
        }
        private set
        {
            
            if (value < _healthPoints)
            {

                takeDamageEvent?.Invoke();
            }

            _healthPoints = value;


            if (HasInputAuthority)
            {
                onChangeHealth?.Invoke();
            }

            if (_healthPoints > _maxhealthPoints)
            {
                _healthPoints = _maxhealthPoints;
            }

            if (_healthPoints < 0)
            {
                Runner.Spawn(corpse, transform.position);

                Die();
            }

        }

    }

    [SerializeField]
    private int _healthPoints;

    private void Start()
    {
        _maxhealthPoints = _healthPoints;
    }


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

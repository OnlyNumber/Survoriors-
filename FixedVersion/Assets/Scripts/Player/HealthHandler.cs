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

    [SerializeField]
    private int _healthPoints;

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

            if (_healthPoints <= 0)
            {
                if(corpse != null)
                Runner.Spawn(corpse, transform.position);

                Die();
            }

        }

    }

    

    private void Start()
    {
        _maxhealthPoints = _healthPoints;
    }


    [Rpc(RpcSources.All, RpcTargets.All)]
    private void Rpc_RequestChangehealth(int damage/*, PlayerScoreV2 playerScore = null*/, RpcInfo info = default)
    {
        /*if (playerScore != null)
        {
            //Debug.Log("work");

            if(HealthPoints > damage)
            {
                playerScore.IncreaseScore(damage);
            }
            else
            {
                playerScore.IncreaseScore(HealthPoints);
                playerScore.IncreaseKills(1);

            }
        }*/

        Debug.Log("Health");

        HealthPoints -= damage;

        

    }

    public void TakeDamage(int dmg/*, PlayerScoreV2 playerScore = null*/)
    {
        Rpc_RequestChangehealth(dmg/*, playerScore*/);

        /*if (playerScore != null)
        {
            //Debug.Log("work");

            playerScore.Increase();

            if (HealthPoints > dmg)
            {
                playerScore.IncreaseScore(dmg);
            }
            else
            {
                playerScore.IncreaseScore(HealthPoints);
                playerScore.IncreaseKills(1);

            }
        }*/

    }

    public void TakeDamage(int damage, PlayerScrV3 playerScr = null)
    {

        Rpc_RequestChangehealth(damage/*, playerScore*/);

        if (playerScr != null)
        {
            //Debug.Log("work");

            

            if (HealthPoints > damage)
            {
                playerScr.Rpc_RequestChangeScore(damage);
            }
            else
            {
                playerScr.Rpc_RequestChangeScore(HealthPoints);
                playerScr.Rpc_RequestChangeKill(1);

            }
        }

    }



    private void Die()
    {
        Runner.Despawn(GetComponent<NetworkObject>());
    }

    }

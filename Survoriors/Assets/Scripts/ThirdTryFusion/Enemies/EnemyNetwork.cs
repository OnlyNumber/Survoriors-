using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EnemyNetwork : NetworkBehaviour
{
    /*struct NetworkHealth : INetworkStruct
    {
        public int health;
    }

    [Networked(OnChanged = nameof(OnHealthChanged))]
    private NetworkHealth _networkHealth; */
    
    private NetworkRunner _networkRunner;

    [SerializeField]
    protected float Speed;

    private List<GameObject> _players;

    private GameObject _target;

    protected bool _isCanAttack;

    protected int _healthPoints;

    


    [SerializeField]
    protected float _timeBetweenAttack;

    protected virtual void Attack() {}

    private void Awake()
    {
        _players = new List<GameObject>();
    }

    public override void FixedUpdateNetwork()
    {
        FindTarget();

        MoveToTarget();
    }

    private void FindTarget()
    {
        if(_players.Count == 0)
        {
            FindPlayers();
        }

        if (_players.Count == 0)
        {
            _target = null;
            return;
        }

        float dist;
        dist = Vector2.Distance(_players[0].transform.position, transform.position);

        _target = _players[0];

        foreach (var item in _players)
        {
            if(dist > Vector2.Distance(item.transform.position, transform.position))
            {
                dist = Vector2.Distance(item.transform.position, transform.position);

                _target = item;
            }

        }
    }

    private void MoveToTarget()
    {
        if (_target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, Speed);
        }
    }

    public void FindPlayers()
    {
        foreach (var item in FindObjectsOfType<PlayerInputHandler>())
        {
            _players.Add(item.gameObject);
            Debug.Log(item.gameObject.name);

        }
    }

    public void InitializeNetwork(NetworkRunner networkRunner)
    {
        this._networkRunner = networkRunner;
    }
    
    private IEnumerator CR_Timer(float time)
    {
        _isCanAttack = false;

        yield return new WaitForSecondsRealtime(time);


        _isCanAttack = true;
    }

    /*private static void OnHealthChanged(Changed<EnemyNetwork> changed)
    {
        changed.Behaviour.OnSkinChange();
    }*/

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void Rpc_RequestChangehealth(int damage, RpcInfo info = default)
    {
        //Debug.Log($"Recive RPC request for player {transform.name} DataHolder ID {DataHolderPlayer.playerSkin}");
        Debug.Log($"Damage {damage}  health {_healthPoints}");

        _healthPoints -= damage;

        Debug.Log("health " + _healthPoints);

    }

}

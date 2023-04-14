using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EnemyStateMachine : NetworkBehaviour
{
    [SerializeField]
    private Animator animator;

    public enum States
    {
        Idle = 0,
        Moving = 1,
        Dead = 2,
        Hit = 3
    }

    private List<State> _states = new List<State>();

    private State currentState;

    private Animator _currentAnimator;

    //private SkinController _skinController;

    private EnemyNetwork _enemyNetwork;

    private void Awake()
    {
        _currentAnimator = GetComponent<Animator>();

        _enemyNetwork = GetComponentInParent<EnemyNetwork>();

        //_skinController = GetComponentInParent<SkinController>();

        _states.Add(new Idle("Idle", _currentAnimator));
        _states.Add(new Moving("Moving", _currentAnimator));
        _states.Add(new Dead("Dead", _currentAnimator));
        _states.Add(new Dead("Hit", _currentAnimator));


        currentState = _states[(int)States.Idle];
    }

    public override void FixedUpdateNetwork()
    {

        currentState.OnUpdate();

            if (_enemyNetwork.GetDistance() <= _enemyNetwork.distanceForAttack)
            {
                GetNextState((int)States.Idle);
            }
            else
            {
                GetNextState((int)States.Moving);
            }

    }

    public void GetNextState(int numberOfState)
    {
        if (_states[numberOfState] == currentState)
        {
            return;
        }

        Rpc_ChangeState(numberOfState);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_ChangeState(int numberOfState)
    {
        currentState.OnExit();

        currentState = _states[numberOfState];

        currentState.OnEnter();
    }




}

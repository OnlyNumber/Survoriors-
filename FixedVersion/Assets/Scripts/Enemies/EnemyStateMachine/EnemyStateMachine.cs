using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EnemyStateMachine : NetworkBehaviour
{
    [SerializeField]
    private Animator animator;

    public enum StatesEnemy
    {
        
        Moving = 0,
        Dead = 1,
        Hit = 2
    }

    private List<StateEnemy> _states = new List<StateEnemy>();

    private StateEnemy currentState;

    private Animator _currentAnimator;

    private HealthHandler _healthHandler;

    private void Awake()
    {
        _currentAnimator = GetComponent<Animator>();

        _healthHandler = GetComponentInParent<HealthHandler>();

        _healthHandler.takeDamageEvent += GetNextStateHit;

        _states.Add(new MovingEnemy("Run", _currentAnimator, this));
        _states.Add(new DeadEnemy("Dead", _currentAnimator, this));
        _states.Add(new HitEnemy("Hit", _currentAnimator, this));


        currentState = _states[(int)StatesEnemy.Moving];
    }

    public override void FixedUpdateNetwork()
    {
        currentState.OnUpdate();
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

    public void GetNextStateHit()
    {
        GetNextState((int)StatesEnemy.Hit);
    }


}

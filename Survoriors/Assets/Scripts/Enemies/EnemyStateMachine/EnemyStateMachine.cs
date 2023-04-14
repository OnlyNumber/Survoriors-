using System.Collections;
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

    private List<State> _states = new List<State>();

    private State currentState;

    private Animator _currentAnimator;

    //private SkinController _skinController;

    private HealthHandler _healthHandler;

    private void Awake()
    {
        _currentAnimator = GetComponent<Animator>();

        _healthHandler = GetComponentInParent<HealthHandler>();

        _healthHandler.takeDamageEvent += GetNextStateHit;

        //_skinController = GetComponentInParent<SkinController>();

        _states.Add(new MovingEnemy("Run", _currentAnimator));
        _states.Add(new DeadEnemy("Dead", _currentAnimator));
        _states.Add(new HitEnemy("Hit", _currentAnimator));


        currentState = _states[(int)StatesEnemy.Moving];
    }

    public override void FixedUpdateNetwork()
    {

        currentState.OnUpdate();
        

        //GetNextState((int)StatesEnemy.Moving);
        
    
    }

    public void GetNextState(int numberOfState)
    {
        if(gameObject.name == "SkeletonEnemy")
        {
            Debug.Log(currentState.ToString());
        }

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

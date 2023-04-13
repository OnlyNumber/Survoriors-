using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class StateMachine : NetworkBehaviour
{
    [SerializeField]
    private Animator animator;

    public enum States
    {
        Idle = 0,
        Moving = 1,
        Dead = 2
    }

    private List<State> _states = new List<State>();

    private State currentState;

    [SerializeField]
    private RuntimeAnimatorController[] _animatorChoose;

    private Animator _currentAnimator;

    //private SkinController _skinController;

    private void Awake()
    {
        _currentAnimator = GetComponent<Animator>();

        //_skinController = GetComponentInParent<SkinController>();

        _states.Add(new Idle("Idle",_currentAnimator));
        _states.Add(new Moving("Moving",_currentAnimator));
        _states.Add(new Dead("Dead",_currentAnimator));

        currentState = _states[(int)States.Idle];
    }

    public void ChangeSkin(int changedSkin)
    {
        _currentAnimator.runtimeAnimatorController = _animatorChoose[changedSkin];


    }

    public override void FixedUpdateNetwork()
    {

        currentState.OnUpdate();

        if (GetInput(out NetworkInputData networkInput))
        {
            if (Mathf.Abs(networkInput.movementAxisInput.x) > 0 || Mathf.Abs(networkInput.movementAxisInput.y) > 0)
            {
                GetNextState((int)States.Moving);
            }
            else
            {
                GetNextState((int)States.Idle);
            }
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

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void Rpc_ChangeState(int numberOfState)
    {
        currentState.OnExit();

        currentState = _states[numberOfState];

        currentState.OnEnter();
    }




}

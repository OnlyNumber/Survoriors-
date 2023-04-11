using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fusion;

public class StateManager : NetworkBehaviour
{
    public enum States
    {
        Idle = 0 ,
        Moving = 1,
        Dead = 2
    }

    private List<State> _states = new List<State>();

    private State currentState;

   // [SerializeField]
   // private Animator[] _animatorChoose;

    private Animator _currentAnimator;

    private void Awake()
    {
        _currentAnimator = GetComponent<Animator>();

        

        _states.Add(new Idle(_currentAnimator, this));
        _states.Add(new Moving(_currentAnimator, this));
        _states.Add(new Dead(_currentAnimator, this));

        currentState = _states[(int)States.Idle];

        

    }

    public override void FixedUpdateNetwork()
    {

        currentState.OnUpdate();

        if(GetInput(out NetworkInputData networkInput))
        {
            //Debug.Log(networkInput.movementAxisInput.x + " " + networkInput.movementAxisInput.y);

            if (Mathf.Abs(networkInput.movementAxisInput.x )> 0 || Mathf.Abs(networkInput.movementAxisInput.y) > 0)
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

        /* currentState.OnExit();

         currentState = _states[typeof(TState)];

         currentState.OnEnter();

         Debug.Log(_states[typeof(TState)].ToString());*/
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void Rpc_ChangeState(int numberOfState)
    {
        /*if (_states[typeof(TState)] == currentState)
        {
            return;
        }*/

        currentState.OnExit();

        currentState = _states[numberOfState];

        currentState.OnEnter();

        //Debug.Log(_states[typeof(TState)].ToString());
    }



}

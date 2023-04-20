using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateEnemy
{
    protected string stateString;
    protected Animator animator;
    protected EnemyStateMachine stateMachine;

    public StateEnemy(string stateString, Animator animator, EnemyStateMachine stateMachine)
    {
        this.stateString = stateString;

        this.animator = animator;

        this.stateMachine = stateMachine;
    }

    public abstract void OnEnter();

    public abstract void OnUpdate();

    public abstract void OnExit();


}

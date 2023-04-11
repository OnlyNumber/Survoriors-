using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Animator animator;
    protected StateManager stateManager;

    public State(Animator animator, StateManager stateManager)
    {
        this.animator = animator;
        this.stateManager = stateManager;
    }


    public abstract void OnEnter();

    public abstract void OnUpdate();

    public abstract void OnExit();

}

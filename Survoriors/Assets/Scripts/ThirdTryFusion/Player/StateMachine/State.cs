using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected string stateString;
    protected Animator animator;


    public State(string stateString, Animator animator)
    {
        this.stateString = stateString;

        this.animator = animator;
    }

    public abstract void OnEnter();

    public abstract void OnUpdate();

    public abstract void OnExit();


}

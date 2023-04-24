using UnityEngine;

public class Idle : State
{
    public Idle(string stateString, Animator animator) : base(stateString, animator)
    {

    }

    public override void OnEnter()
    {
        animator.CrossFade(stateString, 0,0);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {

    }
}

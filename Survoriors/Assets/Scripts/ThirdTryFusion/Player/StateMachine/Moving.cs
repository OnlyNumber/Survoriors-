using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : State
{
    public Moving(Animator animator, StateManager stateManager) : base(animator, stateManager)
    {

    }

    public override void OnEnter()
    {
        animator.SetBool("IsMoving", true);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        animator.SetBool("IsMoving", false);
    }
}

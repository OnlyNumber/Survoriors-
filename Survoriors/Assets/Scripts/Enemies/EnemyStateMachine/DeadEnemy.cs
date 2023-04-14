using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : State
{
    public DeadEnemy(string stateString, Animator animator) : base(stateString, animator)
    {

    }

    public override void OnEnter()
    {
        animator.CrossFade(stateString, 0, 0);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {

    }
}

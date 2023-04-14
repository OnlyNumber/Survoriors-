using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : StateEnemy
{
    public DeadEnemy(string stateString, Animator animator, EnemyStateMachine stateMachine) : base(stateString, animator, stateMachine)
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

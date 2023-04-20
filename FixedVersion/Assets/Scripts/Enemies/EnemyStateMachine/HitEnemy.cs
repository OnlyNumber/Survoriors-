using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class HitEnemy : StateEnemy
{
    TickTimer timer;

    public HitEnemy(string stateString, Animator animator, EnemyStateMachine stateMachine) : base(stateString, animator, stateMachine)
    {

    }

    public override void OnEnter()
    {
        

        timer = TickTimer.CreateFromSeconds(stateMachine.Runner, 0.2f);

        animator.CrossFade(stateString, 0, 0);

    }

    public override void OnUpdate()
    {
        if(timer.Expired(stateMachine.Runner))
        {
            stateMachine.GetNextState((int)EnemyStateMachine.StatesEnemy.Moving);
        }
    }

    public override void OnExit()
    {

    }
}

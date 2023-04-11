using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : State
{

    public Dead(Animator animator, StateManager stateManager) : base(animator, stateManager)
    {
    }


    public override void OnEnter()
    {
        animator.SetTrigger("DeathTrigger");
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {

    }

}

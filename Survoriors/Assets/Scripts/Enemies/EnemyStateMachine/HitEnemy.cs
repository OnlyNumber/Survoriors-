using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : State
{
    float timer = 1;

    public HitEnemy(string stateString, Animator animator) : base(stateString, animator)
    {

    }

    public override void OnEnter()
    {
        Debug.Log("Hitted");

        animator.CrossFade(stateString, 0, 0);

    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {

    }
}

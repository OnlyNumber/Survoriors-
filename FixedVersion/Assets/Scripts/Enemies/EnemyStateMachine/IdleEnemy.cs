using UnityEngine;

public class IdleEnemy : State
{
    public IdleEnemy(string stateString, Animator animator) : base(stateString, animator)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Disappearing : NetworkBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    public override void FixedUpdateNetwork()
    {

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - Time.deltaTime);

        //Debug.Log(sprite.color.a);


        if(sprite.color.a <= 0)
        {
            Runner.Despawn(GetComponent<NetworkObject>());
        }

    }


}

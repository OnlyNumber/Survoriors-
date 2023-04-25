using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class FlipManagerEnemy : NetworkBehaviour
{
    private bool isFlipped = false;

    public void CheckFlip(Vector2 position)
    {
       
            if (position.x - transform.position.x < 0 && !isFlipped)
            {
            Rpc_Flip();
            }
            else if (position.x - transform.position.x > 0 && isFlipped)
            {
            Rpc_Flip();
            }
        
    }

    [Rpc]
    private void Rpc_Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isFlipped = !isFlipped;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class FlipManager : NetworkBehaviour
{
    private bool isFlipped = false;

    public delegate void OnChangeFlip();

    public event OnChangeFlip flipChange;

    public bool IsFlipped
    {
        get
        {
            return isFlipped;
        }
        set
        {
            flipChange?.Invoke();

            isFlipped = value;

        }
    }

    

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData networkInputData))
        {
            if(networkInputData.movementAxisInput.x < 0 && !isFlipped)
            {
                Flip();
            }
            else if(networkInputData.movementAxisInput.x > 0 && isFlipped)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        IsFlipped = !IsFlipped;
    }

}

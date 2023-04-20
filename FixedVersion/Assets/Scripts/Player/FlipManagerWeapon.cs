using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class FlipManagerWeapon : NetworkBehaviour
{
    private bool isFlipped = false;
    
    private Vector3 saveScale;
    
    private FlipManager flipManager;



    private void Start()
    {
        flipManager = GetComponentInParent<FlipManager>();

        flipManager.flipChange += ChangeScaleX;

        saveScale = transform.localScale;

    }



    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            if (networkInputData.mousePosition != Vector2.zero)
            {
                if (networkInputData.mousePosition.x < 0 && !isFlipped)
                {
                    Flip();
                }
                else if (networkInputData.mousePosition.x > 0 && isFlipped)
                {
                    Flip();
                }
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
    }

    private void Flip()
    {
        Vector3 scale = saveScale;

        scale.y *= -1;
        transform.localScale = scale;

        saveScale = transform.localScale;
        isFlipped = !isFlipped;
    }

    private void ChangeScaleX()
    {
       // Debug.Log("Work");

        Vector3 scale = saveScale;

        scale.x *= -1;

        transform.localScale = scale;
        saveScale = transform.localScale;


    }

    /*private void Flip()
    {
        Vector3 scale = transform.localScale;


        scale.y *= -1;
        transform.localScale = scale;
        isFlipped = !isFlipped;
    }

    private void ChangeScaleX()
    {
        Debug.Log("Work");

        Vector3 scale = transform.localScale;

        scale.x *= -1;

        transform.localScale = scale;

    }*/


}

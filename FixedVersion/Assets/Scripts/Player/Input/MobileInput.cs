using UnityEngine;
using Fusion;

public class MobileInput : IInput
{
    FixedJoystick weapnJoystick;
    FixedJoystick moveJoysticl;
    
    public void Start()
    {

        moveJoysticl = GameObject.Find("MovingJoystick").GetComponent<FixedJoystick>(); 

        weapnJoystick = GameObject.Find("WeaponJoystick").GetComponent<FixedJoystick>();

    }

    public void GetInput(out Vector2 moveInputVector, out Vector2 mousePosition, out NetworkBool isCanShoot)
    {
        moveInputVector.x = moveJoysticl.Horizontal;
        moveInputVector.y = moveJoysticl.Vertical;
        mousePosition.x = weapnJoystick.Horizontal;
        mousePosition.y = weapnJoystick.Vertical;
        isCanShoot = true;

        if (mousePosition == Vector2.zero)
        {
            isCanShoot = false;
        }


    }
}

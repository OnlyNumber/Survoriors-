using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayerController : NetworkBehaviour
{
    [SerializeField]
    float speed;

    float horizontalInput;
    float verticalInput;

    [SerializeField]
    Rigidbody2D rigidbody;

    [SerializeField]
    WeaponRotater weaponRotater;

    public override void FixedUpdateNetwork()
    {
        Move();
    }

    private void Move()
    {
        if (GetInput(out NetworkInputData networkInput))
            {
                rigidbody.MovePosition(transform.position + (Vector3)networkInput.movementAxisInput * speed);
                weaponRotater.RotateWeapon(networkInput.mousePosition);
            }

        /*horizontalInput = Input.GetAxisRaw("Horizontal");

        verticalInput = Input.GetAxisRaw("Vertical");

        rigidbody.MovePosition(transform.position + new Vector3(horizontalInput, verticalInput) * speed * Time.deltaTime);
        */
    }

    public void RPC_SetNickName()
    {
    }

}

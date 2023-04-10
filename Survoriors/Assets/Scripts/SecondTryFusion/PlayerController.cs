using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerController : NetworkBehaviour
{
    Rigidbody2D rigidBody;

    private float speed = 70;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        Moving();
    }

    private void Moving()
    {


        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if(rigidBody != null)
        rigidBody.MovePosition(transform.position + new Vector3(horizontalInput, verticalInput) * speed * Time.deltaTime);

    }


}

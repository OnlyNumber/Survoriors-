using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour
{
    Rigidbody2D rigidBody;

    [SerializeField]
    private float speed;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    private void Moving()
    {


        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");


        rigidBody.MovePosition(transform.position + new Vector3(horizontalInput, verticalInput) * speed * Time.deltaTime);

    }
}

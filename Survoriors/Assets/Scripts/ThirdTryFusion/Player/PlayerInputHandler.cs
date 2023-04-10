using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    Vector3 moveInputVector = Vector2.zero;

    private void Update()
    {
        moveInputVector.x = Input.GetAxisRaw("Horizontal");

        moveInputVector.y = Input.GetAxisRaw("Vertical");
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        networkInputData.movementAxisInput = moveInputVector;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        networkInputData.mousePosition = Camera.main.ScreenToWorldPoint(mousePos);




        return networkInputData;
    }

}

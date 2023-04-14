using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector3 _moveInputVector = Vector2.zero;
    private NetworkBool _isFireButtonPressed = false;
    private NetworkBool _isReloadButtonPressed = false;


    private FixedJoystick _joystick;


    private void Update()
    {
        _moveInputVector.x = Input.GetAxisRaw("Horizontal");

        _moveInputVector.y = Input.GetAxisRaw("Vertical");



        if (Input.GetButton("Fire1"))
        {
            _isFireButtonPressed = true;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            _isReloadButtonPressed = true;
        }


    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        networkInputData.movementAxisInput = _moveInputVector;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        networkInputData.mousePosition = Camera.main.ScreenToWorldPoint(mousePos);

        networkInputData.isFireButtonPressed = _isFireButtonPressed;
        networkInputData.isReloadButtonPressed = _isReloadButtonPressed;


        _isFireButtonPressed = false;
        _isReloadButtonPressed = false;


        return networkInputData;
    }

}

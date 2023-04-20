using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 _mousePosition = Vector2.zero;
    private Vector2 _moveInputVector = Vector2.zero;
    private NetworkBool _isFireButtonPressed = false;
    private NetworkBool _isReloadButtonPressed = false;
    private IInput moveInput;

    //private FixedJoystick _joystick;
    private void Start()
    {
        moveInput = new MobileInput();

        moveInput.Start();
    }

    private void Update()
    {
        moveInput.GetInput(out _moveInputVector, out _mousePosition, out _isFireButtonPressed);

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
        networkInputData.mousePosition = _mousePosition;//Camera.main.ScreenToWorldPoint(mousePos);

        networkInputData.isFireButtonPressed = _isFireButtonPressed;
        networkInputData.isReloadButtonPressed = _isReloadButtonPressed;


        _isFireButtonPressed = false;
        _isReloadButtonPressed = false;


        return networkInputData;
    }

}

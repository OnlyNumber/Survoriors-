using UnityEngine;
using Fusion;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 _mousePosition = Vector2.zero;
    private Vector2 _moveInputVector = Vector2.zero;
    private NetworkBool _isFireButtonPressed = false;
    private NetworkBool _isReloadButtonPressed = false;
    private IInput moveInput;

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

        //Vector3 mousePos = Input.mousePosition;
       // mousePos.z = Camera.main.nearClipPlane;
        networkInputData.mousePosition = _mousePosition;

        networkInputData.isFireButtonPressed = _isFireButtonPressed;
        networkInputData.isReloadButtonPressed = _isReloadButtonPressed;


        _isFireButtonPressed = false;
        _isReloadButtonPressed = false;


        return networkInputData;
    }

}

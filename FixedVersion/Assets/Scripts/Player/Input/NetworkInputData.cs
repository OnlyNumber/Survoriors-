using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{

    public Vector2 movementAxisInput;
    public Vector2 mousePosition;
    public NetworkBool isFireButtonPressed;
    public NetworkBool isReloadButtonPressed;
}

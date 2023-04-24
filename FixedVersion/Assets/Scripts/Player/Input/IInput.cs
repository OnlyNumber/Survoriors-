using UnityEngine;
using Fusion;
public interface IInput 
{

    public void Start();

    public void GetInput(out Vector2 moveInputVector, out Vector2 mousePosition, out NetworkBool isCanShoot);

}

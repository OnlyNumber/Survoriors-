using UnityEngine;

public class WeaponRotater : MonoBehaviour
{
    public void RotateWeapon(Vector3 rotatePos)
    {
        transform.right = rotatePos.normalized;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotater : MonoBehaviour
{
    private void Update()
    {

        /*Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 direction = worldPos - transform.position;
        direction.z = 0;

        transform.right = direction.normalized;*/
    }

    public void RotateWeapon(Vector3 rotatePos)
    {
        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = Camera.main.nearClipPlane;
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 direction = rotatePos - transform.position;
        direction.z = 0;

        transform.right = direction.normalized;
    }


}

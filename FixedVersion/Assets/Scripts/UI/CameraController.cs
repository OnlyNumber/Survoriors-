using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CameraController : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera cam;

    [SerializeField]
    private HealthHandler _healthHandler;

    private void Start()
    {
        cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();

        //cam[0].Follow = transform;

        if(GetComponent<NetworkObject>().HasInputAuthority)
        {
            cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
            cam.Follow = transform;
        }

        _healthHandler.onDeath += CameraOnDeath;

    }

    private void CameraOnDeath()
    {
        cam.Follow = FindObjectOfType<NetworkPlayer>().transform;
    }



}

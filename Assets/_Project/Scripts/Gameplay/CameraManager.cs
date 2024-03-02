using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    public void SetCameraFollow(GameObject target)
    {
        virtualCamera.Follow = target.transform;
        virtualCamera.LookAt = target.transform;
    }
}

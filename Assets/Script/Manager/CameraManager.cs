using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : CinemachineExtension
{
    [SerializeField] private float clampAngel = 80f;
    [SerializeField] private float verticalSpeed = 10f;
    private Vector3 startingRotation;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                float CameraAxisY = Input.GetAxis("Mouse Y");
                startingRotation.y += CameraAxisY * verticalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngel, clampAngel);
                state.RawOrientation = Quaternion.Euler(startingRotation.y, 0, 0);
            }
        }
    }
}



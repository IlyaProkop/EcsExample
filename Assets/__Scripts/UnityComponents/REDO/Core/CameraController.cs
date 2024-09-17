using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private PanAndZoom panAndZoom;
    [SerializeField] private MobileOrthoZoom mobileOrthoZoom;
    [SerializeField] private GameObject defaultCamera;
    [SerializeField] private Cinemachine.CinemachineImpulseSource shakeSource;

    private void Initialize()
    {

    }

    public void Shake()
    {
        shakeSource.GenerateImpulse(transform.forward);
    }

    public void SetZoom(float zoom)
    {
        mobileOrthoZoom.Zoom(zoom);
    }
}

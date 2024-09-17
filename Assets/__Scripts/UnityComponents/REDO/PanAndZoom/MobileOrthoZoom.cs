using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MobileOrthoZoom : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera cineCamera;

    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            TouchZoom(difference * 0.01f);
        }
    }

    public void Zoom(float increment)
    {
        cineCamera.m_Lens.OrthographicSize = Mathf.Clamp(cineCamera.m_Lens.OrthographicSize + increment, zoomOutMin, zoomOutMax);
    }

    public void TouchZoom(float increment)
    {
        cineCamera.m_Lens.OrthographicSize = Mathf.Clamp(cineCamera.m_Lens.OrthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}

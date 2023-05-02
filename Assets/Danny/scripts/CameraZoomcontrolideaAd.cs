using System;
using System.Collections;
using System.Collections.Generic;
using Jacob.Scripts.Controllers;
using UnityEngine;

public class CameraZoomcontrolideaAd : MonoBehaviour
{
    public Cam cam;
    public GameObject IdeaAd;
    public bool testCameraZoomOutBool;
    public float zoomOutScale;
    public float zoomInScale;

    private void Update()
    {
        /*
        if (IdeaAd.activeInHierarchy)
        {
            testCameraZoomOutBool = true;
        }
        else
        {
            testCameraZoomOutBool = false;
        }
        if (testCameraZoomOutBool)

        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
        */
    }


    private void OnEnable()
    {
        ZoomOut();
    }

    private void OnDisable()
    {
        ZoomIn();
    }

    public void ZoomOut()
    {
        if (!cam) return;
        cam.Camera.orthographicSize = zoomOutScale;
    }
    public void ZoomIn()
    {
        if (!cam) return;
        cam.Camera.orthographicSize = zoomInScale;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Jacob.Scripts.Controllers;
using Jacob.Scripts.Data;
using UnityEngine;

public class CameraZoomcontrolideaAd : MonoBehaviour
{
    public Cam cam;
    public GameObject IdeaAd;
    public bool testCameraZoomOutBool;
    public float zoomOutScale;
    public float zoomInScale;

    private void Start()
    {
        cam = Camera.main.GetComponent<Cam>();
        //  IdeaAd = FindObjectOfType<CameraZoomcontrolideaAd>().GetComponent<GameObject>();
    }

    public void Clamp()
    {
        cam = Camera.main.GetComponent<Cam>();
        // Set the Clamping type of the Cam to use Manual Coordinates
        cam.clampingTypes = ClampingTypes.ManualCoordinates;
        // Clamp the vertical position (this is a top down stage)
        cam.clampVerticalPosition = true;
        // Turn on clamping
        cam.Clamp = true;
    }

    private void OnEnable()
    {
        Clamp();
    }

    private void OnDisable()
    {
        cam.Clamp = false;
    }

    private void Update()
    {
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
    }
    public void ZoomOut()
    {
        cam.Camera.orthographicSize = zoomOutScale;
    }
    public void ZoomIn()
    {
        cam.Camera.orthographicSize = zoomInScale;
    }
}

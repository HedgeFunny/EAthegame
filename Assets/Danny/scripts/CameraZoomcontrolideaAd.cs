using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomcontrolideaAd : MonoBehaviour
{
    public Camera cam;
    public GameObject IdeaAd;
    public bool testCameraZoomOutBool;
    public float zoomOutScale;
    public float zoomInScale;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
      //  IdeaAd = FindObjectOfType<CameraZoomcontrolideaAd>().GetComponent<GameObject>();
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
        cam.orthographicSize = zoomOutScale;
    }
    public void ZoomIn()
    {
        cam.orthographicSize = zoomInScale;
    }
}

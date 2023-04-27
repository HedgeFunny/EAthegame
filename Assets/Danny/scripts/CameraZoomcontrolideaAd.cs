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
    public void ZoomOut()
    {
        cam.orthographicSize = zoomOutScale;
    }

    public void ZoomIn()
    {
        cam.orthographicSize = zoomInScale;
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
            cam.orthographicSize = zoomOutScale;
        }
        else
        {
            cam.orthographicSize = zoomInScale;
        }
    }

 /*   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ZoomOut();
        }
    } */
}

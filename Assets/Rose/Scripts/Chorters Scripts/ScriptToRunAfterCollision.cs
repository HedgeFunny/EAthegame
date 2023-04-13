using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScriptToRunAfterCollision : MonoBehaviour
{
    //Variables
    private Camera mainCamera;
    private string Chorter;


    public void RunScript()
    {
        mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(Chorter));
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jacob.Scripts.Controllers;

public class DoorSound : MonoBehaviour
{
    public TwoTongueSoundManager soundManager;

    public void PlayDoorSlam()
    {
        soundManager.PlayDoorSlam();
    }
}

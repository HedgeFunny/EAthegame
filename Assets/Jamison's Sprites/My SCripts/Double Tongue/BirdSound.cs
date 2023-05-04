using Jacob.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSound : MonoBehaviour
{
    public TwoTongueSoundManager soundManager;

    public void PlayShotgun()
    {
        soundManager.PlayShotgunNoise();
    }
}

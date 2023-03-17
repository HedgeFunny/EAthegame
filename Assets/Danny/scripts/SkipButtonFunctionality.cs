using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButtonFunctionality : MonoBehaviour
{
    public GameObject nextAd;
    public GameObject previousAd;
    public void SkipButton()
    {
        previousAd.SetActive(false);
        nextAd.SetActive(true);
    }
}

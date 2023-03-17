using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButtonFunctionality : MonoBehaviour
{
    public GameObject nextAd;
    public GameObject previousAd;
    public bool PenultimateAd = false;
    public GameObject BackGround;
    public void SkipButton()
    {
        previousAd.SetActive(false);
        nextAd.SetActive(true);
        if (PenultimateAd)
        {
            BackGround.SetActive(false);
        }
    }
}

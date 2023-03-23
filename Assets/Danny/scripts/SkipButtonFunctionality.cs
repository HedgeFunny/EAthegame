using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButtonFunctionality : MonoBehaviour
{
    public GameObject[] previousAd;
    public GameObject[] nextAd;
    private int nextAdIndex;
    private int previousAdIndex;
    public bool PenultimateAd = false;
    public GameObject BackGround;
    public void SkipButton()
    {
        foreach (GameObject previousAdIndex in previousAd)
        {
            previousAdIndex.SetActive(false);
        }

        foreach (GameObject nextAdIndex in nextAd)
        {
            nextAdIndex.SetActive(true);
            if (PenultimateAd)
            {
                BackGround.SetActive(false);
            }
        }                     
    }
}

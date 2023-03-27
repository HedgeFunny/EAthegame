using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SkipTimer : MonoBehaviour
{
    private float waitTime = 5f;
    private float timePerClockRotation = 1.25f;
    public bool introManAd = false;
    public float customWaitTime;
    public TextMeshProUGUI skipWaitTimer;
    public GameObject skipButton;
    public GameObject clock;

    // Start is called before the first frame update
    void Start()
    {
        if (introManAd)
        {
            waitTime = customWaitTime;
        }
        skipWaitTimer.text = " " + waitTime;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {

        while(waitTime > 0)
        {
            yield return new WaitForSeconds(timePerClockRotation);
            waitTime--;
            skipWaitTimer.text = " " + waitTime;
        }
        skipWaitTimer.text = " ";
        Invoke("ActivateButton", 0);


    }

    void ActivateButton()
    {
        skipWaitTimer.text = " ";
        clock.SetActive(false);
        skipButton.SetActive(true);
        

    }
}

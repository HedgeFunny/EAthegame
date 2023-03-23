using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SkipTimer : MonoBehaviour
{
    public float waitTime = 5f;
    public TextMeshProUGUI skipWaitTimer;
    public GameObject skipButton;
    public GameObject clock;

    // Start is called before the first frame update
    void Start()
    {

        skipWaitTimer.text = " 5";
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {

        yield return new WaitForSeconds(1f);
        waitTime--;
        skipWaitTimer.text = " " + waitTime;
        yield return new WaitForSeconds(1f);
        waitTime--;
        skipWaitTimer.text = " " + waitTime;
        yield return new WaitForSeconds(1f);
        waitTime--;
        skipWaitTimer.text = " " + waitTime;
        yield return new WaitForSeconds(1f);
        waitTime--;
        skipWaitTimer.text = " " + waitTime;
        yield return new WaitForSeconds(1f);
        waitTime--;
        skipWaitTimer.text = " " + waitTime;

        Invoke("ActivateButton", 0);


    }

    void ActivateButton()
    {
        skipWaitTimer.text = " ";
        clock.SetActive(false);
        skipButton.SetActive(true);
        

    }
}

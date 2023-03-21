using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SkipTimer : MonoBehaviour
{
    public float waitTime = 5f;
    public TextMeshProUGUI skipWaitTimer;
    public GameObject skipButton;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
        skipButton.SetActive(true);
    }
}

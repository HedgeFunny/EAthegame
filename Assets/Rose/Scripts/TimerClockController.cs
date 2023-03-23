using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerClockController : MonoBehaviour
{
    //Variable
    public Animator anim;
    public float loopCount = 5;
    public bool IsAnimationOver = true;
    public GameObject timerClock;


    //Coroutines
    public float IsCooldownOver;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        timerClock = GameObject.Find("Timer-Clock").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Starts the Animation loop
        anim.SetBool("isPlaying", IsAnimationOver);

        //Counts the number of times the loop happens and stops it after 5 loops
       /* if ()

        {

        }*/

        //When the animation looped the set amount of times
       if(loopCount <= 0)
        {
            IsAnimationOver = false;
        } 
       different idea to fix this */ 
      while(loopCount >= 0)
        {
            new WaitForSeconds(1);
            loopCount--;
        }
        IsAnimationOver = false;
        timerClock.SetActive(false);

    }


    IEnumerator CountDown()
    {
        loopCount--;

        yield return new WaitForSeconds(IsCooldownOver);

    }
}

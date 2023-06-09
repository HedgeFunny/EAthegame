using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour


{
    //This script is meant to be used by the player.
    //This script's function is to know when the player touches a hazard, play the appropriate animation, and then bring up the watch ad screen.


    //Animation variables
    [Header("Animation Variables")]
    public bool IsDead;
    public bool DiedbyFire;
    public bool DiedToPit;
    public bool DiedToSpikes;
    public bool DiedtoMugging;

    //GameObjects
    [Header("GameObjects")]
    public GameObject WatchAd;
    public bool IsDeathCooldownover;
    public GameObject AdAnchor;

    //Other
    [Header("Other")]
    public float TimeBetweenDeathScreen;

    //Components
    [Header("Components")]
    public Animator animator;
    public CapsuleCollider2D CapColl;

    // Start is called before the first frame update
    void Start()
    {
        //Component Declarations
        animator = gameObject.GetComponent<Animator>();
        CapColl = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Component Variable Declarations
        animator.SetBool("DiedToFire", DiedbyFire);
        animator.SetBool("DiedToPit", DiedToPit);
        animator.SetBool("DiedToMugging", DiedtoMugging);

        //Brings up death screen when cooldown is over
        if (IsDeathCooldownover == true)
        {
            WatchAd.SetActive(true);

        //Makes bool False quickly after to prevent multiple instantiations
        IsDeathCooldownover = false;
        }

    }

    //This part checks to see if any object that the player collides with is a hazard
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Pit Hazard Check
        if (collision.gameObject.CompareTag("Fire"))
        {
            DiedbyFire = true;

            //Starts Cooldown
            StartCoroutine(SummonCoolDown());

            //destroys gameobject responsible of collision
            Destroy(collision.gameObject);
        }

        //Fire Hazard Check
       else if (collision.gameObject.CompareTag("Fall"))
        {
            DiedToPit = true;

            //Starts Cooldown
            StartCoroutine(SummonCoolDown());

            //destroys gameobject responsible of collision
            Destroy(collision.gameObject);
        }

        //Mugging Hazard Check
       else if (collision.gameObject.CompareTag("Mugging"))
        {
            DiedtoMugging = true;

            //Starts Cooldown
            StartCoroutine(SummonCoolDown());

            //destroys gameobject responsible of collision
            Destroy(collision.gameObject);

        }
       else if (collision.gameObject.CompareTag("Spike"))
       {
            DiedToSpikes = true;

            //Starts Cooldown
            StartCoroutine(SummonCoolDown());

            //destroys gameobject responsible of collision
            Destroy(collision.gameObject);

        }

    }

    //This brings up the Watch Ad Gameobject after some time.
    public IEnumerator SummonCoolDown()
    {
        yield return new WaitForSeconds(TimeBetweenDeathScreen);
       
        IsDeathCooldownover = true;


    
    }

    //Resets Booleans when called on
    public void BoolResetter()
    {

    }

}

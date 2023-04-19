using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
<<<<<<< HEAD
    //This script is meant to be used by the main Player character.
    //This scripts purpose is to know when the player touched a hazard, play the appropriate death animation, and bring up the Watch Ad Screen.

    //Animation variables
    [Header("Death Animation Variables")]
    public bool IsDead;
    public bool DeathbyFalling;
    public bool DeathbyFire;


    //GameObjects
    [Header("GameObjects")]
    public GameObject AdWatch;
=======
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
>>>>>>> 5a8e79101ec75b4a3ab13ed0581d237e09fe15e2

    //Components
    [Header("Components")]
    public Animator animator;
<<<<<<< HEAD
    public BoxCollider2D HitBox;
    public PolygonCollider2D HitPoly;
=======
    public CapsuleCollider2D CapColl;
>>>>>>> 5a8e79101ec75b4a3ab13ed0581d237e09fe15e2

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        
=======
        //Component Declarations
        animator = gameObject.GetComponent<Animator>();
        CapColl = gameObject.GetComponent<CapsuleCollider2D>();
>>>>>>> 5a8e79101ec75b4a3ab13ed0581d237e09fe15e2
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        //Animation Variable Declarations
    }

    //This checks object collisions for the right tag
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    //Kills the player, then brings up the watch Ad screen.
    void Death()
    {

    }
=======
        //Component Variable Declarations
        animator.SetBool("DiedToFire", DiedbyFire);
        animator.SetBool("DiedToPit", DiedToPit);
        animator.SetBool("DiedToMugging", DiedtoMugging);
    }

    //This part checks to see if any object that the player collides with is a hazard
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Pit Hazard Check
        if (collision.gameObject.CompareTag("Fire Hazard"))
        {
            DiedbyFire = true;

            //Starts Cooldown
            StartCoroutine(SummonCoolDown());

            //Brings up death screen when cooldown is over
            if (IsDeathCooldownover == true)
            {
                Instantiate(WatchAd, AdAnchor.transform.position, AdAnchor.transform.rotation);

                //Makes bool False quickly after to prevent multiple instantiations
                IsDeathCooldownover = false;
            }
        }

        //Fire Hazard Check
        if (collision.gameObject.CompareTag("Falling Hazard"))
        {
            DiedToPit = true;

            //Starts Cooldown
            StartCoroutine(SummonCoolDown());

            //Brings up death screen when cooldown is over
            if (IsDeathCooldownover == true)
            {
                Instantiate(WatchAd, AdAnchor.transform.position, AdAnchor.transform.rotation);

                //Makes bool False quickly after to prevent multiple instantiations
                IsDeathCooldownover = false;
            }
        }

        //Mugging Hazard Check
        if (collision.gameObject.CompareTag("Mugging Hazard"))
        {
            DiedtoMugging = true;

            //Starts Cooldown
            StartCoroutine(SummonCoolDown());

            //Brings up death screen when cooldown is over
            if (IsDeathCooldownover == true)
            {
                Instantiate(WatchAd,AdAnchor.transform.position, AdAnchor.transform.rotation);

                //Makes bool False quickly after to prevent multiple instantiations
                IsDeathCooldownover = false;
            }
            
        }

    }

    //This brings up the Watch Ad Gameobject after some time.
    public IEnumerator SummonCoolDown()
    {
        yield return new WaitForSeconds(TimeBetweenDeathScreen);

        IsDeathCooldownover = true;
        IsDeathCooldownover = false;
    }
    
    //Resets Booleans when called on
    public void BoolResetter()
    {

    }
    
>>>>>>> 5a8e79101ec75b4a3ab13ed0581d237e09fe15e2
}

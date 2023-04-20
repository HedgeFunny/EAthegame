using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchAd : MonoBehaviour
{
    //This script is meant to be used by the Watch Ad screen.
    //This Scripts funtion is to take boolean information from the deathmanager script, play a specific ad using that information, then run the boolResetter method in the deathmanager script.

    //Ads
    [Header("Ads")]
    public GameObject AdtoPlay;
    public GameObject ChedChortersAd;
    public GameObject IDEAad;
    public GameObject KerfuffleOfKongregationsAd;
    public GameObject TwoTongueAd;

    //DeathManager - Acsessing Variables
    [Header("Death Manager")]
    public GameObject Player;
    public DeathManager deathMan;
    public GameObject adAnchor;

    //Other


    // Start is called before the first frame update
    void Start()
    {
        deathMan = Player.GetComponent<DeathManager>();
    }

    //Brings up Specific Ads If player has died in a Specific Way
    
    void AdMaker()
    {
        //Declarations
        adAnchor = deathMan.AdAnchor;

        if(deathMan.DiedbyFire == true)
        {

            Instantiate(ChedChortersAd, adAnchor.transform.position, adAnchor.transform.rotation);

            deathMan.DiedbyFire = false;
        }

        if (deathMan.DiedToPit == true)
        {

            Instantiate(IDEAad, adAnchor.transform.position, adAnchor.transform.rotation);

            deathMan.DiedToPit = false;
        }

        if (deathMan.DiedtoMugging == true)
        {

            Instantiate(TwoTongueAd, adAnchor.transform.position, adAnchor.transform.rotation);

            deathMan.DiedtoMugging = false;
        }
    
    }
    
}

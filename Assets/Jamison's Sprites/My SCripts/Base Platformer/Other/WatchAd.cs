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

    private void Update()
    {
        //Declarations
        adAnchor = deathMan.AdAnchor;

        //Sets Adtoplay to a specific Gameobject depending on which deathmanager boolean is true
        if (deathMan.DiedbyFire == true)
        {
            AdtoPlay = ChedChortersAd;
        }
        else if (deathMan.DiedToPit == true)
        {
            AdtoPlay = IDEAad;
        }
        else if (deathMan.DiedtoMugging == true)
        {
            AdtoPlay = TwoTongueAd;
        }
    }

    //plays AdToPlay when button is pressed
    public void HahaAdGoBrrrrrr()
    {
        Instantiate(AdtoPlay, adAnchor.transform.position, adAnchor.transform.rotation);
    }
    
    
    
}

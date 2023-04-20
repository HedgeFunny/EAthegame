using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jacob.Scripts.Controllers;
public class CameraFindAnObjectTaggedAsPlayer : MonoBehaviour
{
    [Header("This Script can be on any object, just needs to be active so that it can tell the camera what the current player game object is so that the camera will follow the current player")]
    public GameObject currentPlayer;
    private FollowSomthingScript followScript;
    private Cam camScript;
    // Start is called before the first frame update
    void Start()
    {
        // followScript = FindObjectOfType<FollowSomthingScript>();
      //  camScript = FindObjectOfType<Cam>().GetComponent<Cam>();
    }

    // Update is called once per frame
    void Update()
    {
        camScript = FindObjectOfType<Cam>().GetComponent<Cam>();
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
      //  followScript.ThingYouWantThisObjectToFollow = currentPlayer;
        camScript.followedObject = currentPlayer.transform;
    }
}

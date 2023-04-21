using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSomthingScript : MonoBehaviour
{
    //use this for when you want an object to follow another object. can also add an offset if disired. if this is used for the camera then make sure the offset on the Z value is -10.
    public GameObject ThingYouWantThisObjectToFollow;
    public Vector3 offset;
  //  private bool FollowX = true;
  //  private bool FollowY = true;
    void Update()
    {

      /*  if (FollowX)
        {
            
        }

        if (FollowY)
        {

        }
      */
        // transform.position = ThingYouWantThisObjectToFollow.transform.position + offset;
    }
}

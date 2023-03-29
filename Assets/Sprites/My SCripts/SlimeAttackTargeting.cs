using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackTargeting : MonoBehaviour
{
    //This script is used in the kerfuffle of kongregations ad, specifically the enemies use this to move.

    //This makes the slimes move and attack. They prioritize specific 



    //Moving
    [Header("Movement Variables")]
    public Vector2 CurrentTarget;
    public float speed;
    public Vector2 CurrentPosition;

    //Destroying
    [Header("Destruction")]
    public string tagToDestroy;
    public GameObject destroyMe;

    //Other
    [Header("Other")]
    public GameObject TManager;
    public TargetingManager targetManager;
    

    // Start is called before the first frame update
    void Start()
    {
        targetManager = TManager.GetComponent<TargetingManager>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

   
    //Destroys Objects by Tag
    public void DestroyByTag(string tagToDestroy)
    {
        GameObject[] thingsToDestroy = GameObject.FindGameObjectsWithTag(tagToDestroy);

        GameObject closestObject = thingsToDestroy[0];
        float distanceToClosestObject = (closestObject.transform.position - transform.position).magnitude;
        
        foreach(GameObject destroyMe in thingsToDestroy)
        {
            //Finds Closest Building
           float distanceToDestroyMe = (destroyMe.transform.position - transform.position).magnitude;
            if(distanceToDestroyMe < distanceToClosestObject)
            {
                distanceToClosestObject = distanceToDestroyMe;
                closestObject = destroyMe;

            }

            //Find the direction to the closest object
            /*
             (destination.transform.postion - origin.transform.position).normalize <-- Vector2

             */
            //Move To Building
            transform.Translate((destroyMe.transform.position - gameObject.transform.position) * speed * Time.deltaTime);

            
           
        }


        Destroy(closestObject);
    }

    //Finds Each Building
    void FindTargets()
    {
        if (targetManager.Cannons >= 1)
        {
            DestroyByTag("Cannon");
        }
        
        if(targetManager.GStorages >= 1 && targetManager.Cannons <= 0)
        {
            DestroyByTag("GoldStorage");
        }

        if (targetManager.EStorages >= 1 && targetManager.GStorages <= 0)
        {
            DestroyByTag("ElixirStorage");
        }
    }

    //Moves to Closest building
    void Movement()
    {
     
    }
}

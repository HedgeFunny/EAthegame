using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jacob.Scripts.Controllers;

public class ChortersBag : MonoBehaviour
{

    public int collisionCount = 0;
    public int targetCollisionCount = 3;
    public GameObject otherObject;
    public ScriptToRunAfterCollision scriptToRun;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == otherObject)
        {
            collisionCount++;
            if (collisionCount >= targetCollisionCount)
            {
                scriptToRun.RunScript();
                collisionCount = 0;
                targetCollisionCount = Random.Range(2, 5);
            }
        }
    }
}

public class ScriptToRunAfterCollision : MonoBehaviour
{
    private FlamingAd flamingAd;

    private void Start()
    {
        //flamingAd = GetComponent
    }
    public void RunScript()
    {
        
        Debug.Log("The random number of collisions has been hit!");
        // Add any code you want to run after the random number of collisions has been hit
    }
}

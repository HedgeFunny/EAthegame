using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChortersBag : MonoBehaviour
{

    public int collisionCount = 0;
    public int targetCollisionCount = 3;
    public GameObject otherObject;
    public ScriptToRunAfterCollision scriptToRun;

    private void Start()
    {
        scriptToRun = GetComponent<ScriptToRunAfterCollision>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == otherObject)
        {
            collisionCount++;
            if (collisionCount >= targetCollisionCount)
            {
                scriptToRun.RunScript();
                collisionCount = 0;
            }
        }
    }
}


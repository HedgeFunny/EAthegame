using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlime : MonoBehaviour
{
    //Variables

    public GameObject slimePrefab;
    public GameObject spawnPoint;
    private int randomNumber;
 
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AssignRandomNumber(int number)
    {
        randomNumber = number;
        Debug.Log("Assigned random number " + randomNumber + " to " + gameObject.name); // Optional debug message to confirm that the random number was assigned correctly
    }


    /*
    public void SpawnSlimes1()
    {
        //Internal Variables
        Vector2 spawnPos = new Vector2(-12, 10);
        Instantiate(slimePrefab, spawnPos, slimePrefab.transform.rotation);
    }

    public void SpawnSlimes2()
    {
        //Internal Variables
        Vector2 spawnPos = new Vector2(12, 10);
        Instantiate(slimePrefab, spawnPos, slimePrefab.transform.rotation);
    }

    public void SpawnSlimes3()
    {
        //Internal Variables
        Vector2 spawnPos = new Vector2(-12, -10);
        Instantiate(slimePrefab, spawnPos, slimePrefab.transform.rotation);
    }

    public void SpawnSlimes4()
    {
        //Internal Variables
        Vector2 spawnPos = new Vector2(12, -10);
        Instantiate(slimePrefab, spawnPos, slimePrefab.transform.rotation);
    }
    */
    public void SpawnSlimes()
    {
        Vector2 spawnPos = spawnPoint.transform.position;
        Instantiate(slimePrefab, spawnPos, slimePrefab.transform.rotation);

    }
}

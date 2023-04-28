using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlime : MonoBehaviour
{
    //Variables

    public GameObject slimePrefab;
    private SlimeSpawnerManager slimeManager;

    private int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        slimeManager = GameObject.Find("DemonSlimeSpawnerManager").GetComponent<SlimeSpawnerManager>();

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

    public void SpawnSlimes()
    {
        Vector2 position = gameObject.transform.position;
        Instantiate(slimePrefab, position, Quaternion.identity);
    }
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnerManager : MonoBehaviour
{
    //Variables
    private int maxRounds = 3;
    private int rounds = 0;
    public SpawnSlime slimeSpawner;
    public int numberOfSlimes = 0;
    public GameObject[] demonSlimeSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        slimeSpawner = GameObject.Find("DemonSlimeSpawner").GetComponent<SpawnSlime>();
        int numberOfSlimes = GameObject.FindGameObjectsWithTag("Slime").Length;
        foreach (GameObject obj in demonSlimeSpawner)
        {
            int randomNumber = Random.Range(0, 5); // Generate a random number between 1 and 100
            obj.GetComponent<SpawnSlime>().AssignRandomNumber(randomNumber); // Call a function in MyScript to assign the random number to the game object
        }

        SpawnWave();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnWave()
    {
        if (rounds <= maxRounds)
        {
            rounds++;
           // slimeSpawner.SpawnSlimes();
            yield return new WaitForSeconds(30);
            SpawnWave();
        }
        yield return new WaitForSeconds(30);
    }
}

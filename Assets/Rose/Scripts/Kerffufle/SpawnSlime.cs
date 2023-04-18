using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlime : MonoBehaviour
{
    //Variables

    public GameObject slimePrefab;
    public Vector2[] spawnPos;
    public int index ;
    public int numberOfSlimes = 0;
    private int maxRounds = 3;
    private int rounds = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int numberOfSlimes = GameObject.FindGameObjectsWithTag("Slime").Length;
    }

IEnumerator SpawnWave()
    {
        if(rounds <= maxRounds)
        {
            rounds++;
            SpawnSlimes(index);
            yield return new WaitForSeconds(30);
            SpawnWave();
        }
        yield return new WaitForSeconds(30);
    }

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

    public void SpawnSlimes(int index)
    {
        Instantiate(slimePrefab, spawnPos[index], slimePrefab.transform.rotation);

    }
}

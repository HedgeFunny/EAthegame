using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnCannon : MonoBehaviour
{
    //Variables
    //Spawnpoints for Cannons
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;

    //Buutons
    public Button Postions;
    public Button buyCannon;
    public Button location1;
    public Button location2;
    public Button location3;
    public Button location4;
    public Button Buy;

    //Misc
    public GameObject PositionButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Turns on spawnpoints

    public void TurnOnPositions()
    {
        PositionButtons.SetActive(true);
    }


    //Spawns Cannon at Position 1

    public void ToPostion1()
    {
        Instantiate()
    }
    //Spawns Cannon at Position 2
    //Spawns Cannon at Position 3
    //Spawns Cannon at Position 4
}

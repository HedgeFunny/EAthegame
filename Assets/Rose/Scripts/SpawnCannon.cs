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
    public Button buyCannon;
    public Button Buy;

    // Spawn Points
    private Vector2 Spawn1 = new Vector2(-5.7f, 1.8f);
    private Vector2 Spawn2 = new Vector2(5.7f, 1.8f);
    private Vector2 Spawn3 = new Vector2(-5.7f, -4.1f);
    private Vector2 Spawn4 = new Vector2(5.7f, 4.1f);

    //Misc
    public GameObject PositionButtons;//Sorting GameObject to turn on and off Position Buttons all at once
    public GameObject CannonLeft;
    public GameObject CannonRight;

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


    //Spawns Cannon at Position 1 and then turns of the position buttons
    public void ToPostion1()
    {
        Instantiate(CannonLeft,Spawn1, transform.rotation);
        PositionButtons.SetActive(false);
    }
    //Spawns Cannon at Position 2 and then turns of the position buttons
    public void ToPosition2()
    {
        Instantiate(CannonRight, Spawn2, transform.rotation);
        PositionButtons.SetActive(false);
    }
    //Spawns Cannon at Position 3 and then turns of the position buttons
    public void ToPosition3()
    {
        Instantiate(CannonLeft, Spawn3, transform.rotation);
        PositionButtons.SetActive(false);
    }
    //Spawns Cannon at Position 4 and then turns of the position buttons
    public void ToPosition4()
    {
        Instantiate(CannonRight, Spawn4, transform.rotation);
        PositionButtons.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Jacob.Scripts.Controllers;

public class SpawnCannon : MonoBehaviour
{
    //Variables


    //Buutons
    [Header("Buttons")]public Button buyCannon;
    //public Button Buy;

    // Spawn Points
    private Vector2 Spawn1 = new Vector2(-5.7f, 2.5f);
    private Vector2 Spawn2 = new Vector2(5.7f, 2.5f);
    private Vector2 Spawn3 = new Vector2(-5.7f, -3.4f);
    private Vector2 Spawn4 = new Vector2(5.7f, -3.4f);

    //Misc
    [Header("Misc")]public GameObject PositionButtons;//Sorting GameObject to turn on and off Position Buttons all at once
    public GameObject CannonLeft;
    public GameObject CannonRight;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("UniversalGameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    //Turns on spawnpoints
    public void TurnOnPositions()
    {
        if(gameManager.Cash.Money >= 5000)
        {
            PositionButtons.SetActive(true);
        }
        
    }


    //Spawns Cannon at Position 1 and then subtracts money from your total
    public void ToPostion1()
    {
        Instantiate(CannonLeft, Spawn1, transform.rotation);
        gameManager.Cash.SubtractMoney(5000);
        PositionButtons.SetActive(false);
    }
    //Spawns Cannon at Position 2 and then turns of the position buttons
    public void ToPosition2()
    {
        Instantiate(CannonRight, Spawn2, transform.rotation);
        gameManager.Cash.SubtractMoney(5000);
        PositionButtons.SetActive(false);
    }
    //Spawns Cannon at Position 3 and then turns of the position buttons
    public void ToPosition3()
    {
        Instantiate(CannonLeft, Spawn3, transform.rotation);
        gameManager.Cash.SubtractMoney(5000);
        PositionButtons.SetActive(false);
    }
    //Spawns Cannon at Position 4 and then turns of the position buttons
    public void ToPosition4()
    {
        Instantiate(CannonRight, Spawn4, transform.rotation);
        gameManager.Cash.SubtractMoney(5000);
        PositionButtons.SetActive(false);
    }

}

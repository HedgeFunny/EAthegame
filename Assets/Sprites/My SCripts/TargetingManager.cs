using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingManager : MonoBehaviour
{
    //This Script is for the Kerfuffle of Kongregation ad.
    //This script Records how many of each building is on the map.

    //Total Number of Buildings
    [Header("Buildings Left")]
    public int Cannons;
    public int EStorages;
    public int GStorages;
    public int TownHalls;

    //Buildings
    [Header("Bulding Objects")]
    public GameObject TownHall;
    public GameObject GoldStorage;
    public GameObject ElixirStorage;
    public GameObject Cannon;

    //Losing
    public bool YouLose = false;
    

    // Start is called before the first frame update
    void Start()
    {
     
    }

    private void FixedUpdate()
    {
        //Number of Buildings change over time
        Cannons = GameObject.FindGameObjectsWithTag("Cannon").Length;
     GStorages = GameObject.FindGameObjectsWithTag("GoldStorage").Length;
        EStorages = GameObject.FindGameObjectsWithTag("ElixirStorage").Length;
        TownHalls = GameObject.FindGameObjectsWithTag("ElixirStorage").Length;

        //Makes the Ad stop when you lose
        if (TownHalls == 0)
        {
            YouLose = true;
        }
    }
}

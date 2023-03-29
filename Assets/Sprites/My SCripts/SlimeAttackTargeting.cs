using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackTargeting : MonoBehaviour
{
    //Moving
    [Header("Movement Variables")]
    public Vector2 CurrentTarget;
    public float speed;

    //Destroyed Bools
    [Header("No Targets?")]
    public bool AreCannonsDestroyed = false;
    public bool AreGStoragesDestroyed = false;
    public bool AreEStoragesDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
      //Priority Targeting (Cannons)
      


      //Priority Targeting (Gold Storages)
      if(AreCannonsDestroyed == true)
        {

        }


      //Priority Targeting (Elixir Storage)
     // if(Are)

      //Priority Targeting (Town Hall)
    }

    //Targets GoldStorages if All Cannons Are destroyed.
    void DestroyG()
    {
     
    }

    //Targets Elixir Storages if All GoldStorages Are destroyed.

    void DestroyE()
    {

    }

    //Targets The town Hall if All GoldStorages Are Destroyed.

    void DestroyT()
    {

    }

    //Finds Each Building
    void FindTargets()
    {

    }
}

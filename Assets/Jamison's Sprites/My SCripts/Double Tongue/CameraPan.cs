using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    //This is the Camera
    [Header("Camera")]
    public GameObject Camera;

    //These Variables are used to Move the Camera over a certain time
    [Header("Movement Variables")]
    public float BackMovementSpeed;
    public float DownMovementSpeed;
    public float TimeBetweenMovement;

    //These Variables are only visible in unity for debugging reasons
    [Header("Other")]
    public bool CanMove;
    public bool IsCoroutineUsable = true;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Starts Coroutine, only starts once.
        if(IsCoroutineUsable == true)
        {
            StartCoroutine(MovementTime());
        }
    }

    //Thecooldown
    IEnumerator MovementTime()
    {


        yield return new WaitForSeconds(TimeBetweenMovement);
    }
}

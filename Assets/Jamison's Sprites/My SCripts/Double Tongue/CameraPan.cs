using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    //This is the Camera
    [Header("Camera")]
    public Camera Camera;
    public Vector3 CameraPOS;
        

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
        //Updates CameraPOS
        Camera.transform.position = CameraPOS;

        //Starts Coroutine, only starts once.
        if(IsCoroutineUsable == true)
        {
            StartCoroutine(MovementTime());
        }

        //Backwards Movement
        if(CanMove == true)
        {
            Camera.orthographicSize += BackMovementSpeed * Time.deltaTime;
        }

        //Down Movement
        if (CanMove == true)
        {
            transform.Translate(CameraPOS * DownMovementSpeed * Time.deltaTime );
        }
    }

    //Makes Camera unable to move after a certain amount of time
    IEnumerator MovementTime()
    {
        IsCoroutineUsable = false;
        CanMove = true;

        yield return new WaitForSeconds(TimeBetweenMovement);

        CanMove = false;
    }
}

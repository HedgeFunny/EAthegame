using System.Collections;
using System.Collections.Generic;
using Jacob.Scripts.Controllers;
using UnityEngine;

public class StatIncreaser : MonoBehaviour
{
    private GameObject MainPlayer;
    private Player JacobsPlayerScript;
    private float playerBaseSpeed;
    private float playerBaseJump;
    public float playerSpeedIncreaseAmount;
    public float playerJumpIncreaseAmount;
    // Start is called before the first frame update
    void Start()
    {
        MainPlayer = GameObject.Find("Mr. Top Hat");
        JacobsPlayerScript = MainPlayer.GetComponent<Jacob.Scripts.Controllers.Player>();
        playerBaseSpeed = JacobsPlayerScript.moveSpeed;
        playerBaseJump = JacobsPlayerScript.jumpForce;
        Debug.Log("finding crap");
    }
     public IEnumerator waitAsecondManSpeed()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("we finna upgrade moveSpeed");
        IncreaseMoveSpeed();
        
    }

    public IEnumerator waitAsecondManJump()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("we finna upgrade jumpSpeed");
        IncreaseJumpForce();
        
    }
    public void IncreaseMoveSpeed()
    {
        playerBaseSpeed += playerSpeedIncreaseAmount;
        Debug.Log("speed increasing");
    }

    public void IncreaseJumpForce()
    {
        playerBaseJump += playerJumpIncreaseAmount;
        Debug.Log("jump increasing");
    }

}

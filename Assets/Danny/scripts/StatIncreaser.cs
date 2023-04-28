using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatIncreaser : MonoBehaviour
{
    private GameObject mainPlayer;
    private Jacob.Scripts.Controllers.Player JacobsPlayerScript;
    private float playerBaseSpeed;
    private float playerBaseJump;
    public float playerSpeedIncreaseAmount;
    public float playerJumpIncreaseAmount;
    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GameObject.Find("Mr. Top Hat");

        JacobsPlayerScript = mainPlayer.GetComponent<Jacob.Scripts.Controllers.Player>();

        playerBaseSpeed = JacobsPlayerScript.moveSpeed;
        playerBaseJump = JacobsPlayerScript.jumpForce;
    }
    public void IncreaseMoveSpeed()
    {
        playerBaseSpeed += playerSpeedIncreaseAmount;
    }

    public void IncreaseJumpForce()
    {
        playerBaseJump += playerJumpIncreaseAmount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideOnWalls : MonoBehaviour
{
    public Rigidbody2D PlayerObject;
    public float playerXSpeed;
    public float playerYSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerXSpeed = PlayerObject.velocity.x;
        playerYSpeed = PlayerObject.velocity.y;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayerObject.velocity = new Vector2 (0,playerYSpeed);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}

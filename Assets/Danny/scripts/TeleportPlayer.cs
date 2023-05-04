using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    //put this script on anything you want the player to touch to teleport them
    public Vector2 newTeleportSpot;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 newTeleportSpot = new Vector2(25, 2);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = newTeleportSpot;
        }
    }
}

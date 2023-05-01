using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jacob.Scripts.Controllers;

public class SlimeAttack : MonoBehaviour
{
    //Movement Variables
    [Header("Movement")]
    public float speed;
    public bool CanMove;
    public bool IsAbleToMove;

    public GameObject Target;
    public Vector2 TargetX;
    public Vector2 TargetY;

    //Attacking variables
    [Header("Attacking")]
    public float AttackDamage;
    public float TimeBeforeNextAttack;
    public bool IsAbletoAttack;
    public bool CanAttack;

    public GameObject AttackTarget;
    public Jacob.Scripts.Controllers.HealthBar AttackHealth;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Variable declaration
        AttackHealth = AttackTarget.GetComponent<Jacob.Scripts.Controllers.HealthBar>();

        //Movement
        if(CanMove == true && IsAbleToMove == true)
        {
            transform.Translate(transform.position - Target.transform.position * speed * Time.deltaTime);
        }

        //Attacking
        if(CanAttack == true && IsAbletoAttack == true)
        {
            //Lowers health of object
            AttackHealth.healthSystem.SubtractHealth(AttackDamage);
        }
    }

    //Lowers Health of Object It bumped into
    IEnumerator RipandTear()
    {


        yield return new WaitForSeconds(TimeBeforeNextAttack);
    }

    //Finds Objects Of Right Tag
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Slimes can only attack objects tagged with "Cannon", "GoldStorage", "ElixirStorage", or "TownHall"
        if (collision.gameObject.CompareTag("Cannon"))
        {
            CanAttack = true;
            CanMove = false;

            AttackTarget = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("GoldStorage"))
        {
            CanAttack = true;
            CanMove = false;

            AttackTarget = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("ElixirStorage"))
        {
            CanAttack = true;
            CanMove = false;

            AttackTarget = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("TownHall"))
        {
            CanAttack = true;
            CanMove = false;

            AttackTarget = collision.gameObject;
        }
        else
        {
            CanAttack = false;
            CanMove = true;
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChortersSquishtowin : MonoBehaviour
{
    //Variables
    public Animator chortAnim;
    public bool isCrunching = false;
    public float counter = 0;
    public int counterToReach = 3;
    public GameObject otherObject;
    public ScriptToRunAfterCollision scriptToRun;

    // Start is called before the first frame update
    void Start()
    {
        chortAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isCrunching == false)
        {
            CollisionCounter();
            isCrunching = true;
            chortAnim.SetBool("isTouched", isCrunching);
            StartCoroutine(Stall());


        }
    }

    IEnumerator Stall()
    {
        yield return new WaitForSeconds(3);
        isCrunching = false;
        chortAnim.SetBool("isTouched", isCrunching);
    }
    private void CollisionCounter()
    {
        counter++;
    }

    private void Collider()
    {
        float HittoWin = Random.Range(0, 5);
        


    }
}

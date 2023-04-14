using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChortersSquishtowin : MonoBehaviour
{
    //Variables
    public Animator chortAnim;
    public bool isCrunching = false;
    public bool isCrunched = false;

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
        if (collision.gameObject.CompareTag("Player") && isCrunching == false && isCrunched == false)
        {
            isCrunching = true;
            chortAnim.SetBool("isTouched", isCrunching);
            StartCoroutine(Stall());


        }
        if (collision.gameObject.CompareTag("Player")  && isCrunching == false && isCrunched == false)
        {
            isCrunched = true;
            chortAnim.SetBool("isTouched", isCrunched);
            StartCoroutine(Stall2());
        }
    }

    IEnumerator Stall()
    {
        yield return new WaitForSeconds(3);
        isCrunching = false;
        chortAnim.SetBool("isTouched", isCrunching);
    }

    IEnumerator Stall2()
    {
        yield return new WaitForSeconds(3);
        isCrunched = false;
        chortAnim.SetBool("isTouched", isCrunched);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChortersSquishtowin : MonoBehaviour
{
    //Variables
    public Animator chortAnim;
    public bool isCrunching = false;

    // Start is called before the first frame update
    void Start()
    {
        chortAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && isCrunching == false)
        {

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
}

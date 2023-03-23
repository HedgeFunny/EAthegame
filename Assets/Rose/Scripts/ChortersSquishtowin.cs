using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChortersSquishtowin : MonoBehaviour
{
    //Variables
    public Animator chortAnim;
    public bool isCrunching = true;

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
        if(collision.gameObject.CompareTag("Player"))
        {
            chortAnim.SetBool("isTouched", isCrunching);

        }

    }

    IEnumerator Stall()
    {
        yield return new WaitForSeconds(3);
    }
}

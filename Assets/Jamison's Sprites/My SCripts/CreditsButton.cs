using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    //Canvases
    [Header("Canvases")]
    public GameObject Credits;
    public GameObject thankyouscreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Creditsgobrrrrrrr()
    {
        Credits.SetActive(true);
        thankyouscreen.SetActive(false);
    }

    public void Creditsdonotgobrrrr()
    {
        Credits.SetActive(false);
    }
}

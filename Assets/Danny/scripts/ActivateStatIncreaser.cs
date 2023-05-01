using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStatIncreaser : MonoBehaviour
{
    private StatIncreaser TheStatIncreaserScript;
    public bool speedIncreaseAfterAd = false;
    public bool jumpIncreaseAfterAd = false;
    // Start is called before the first frame update
    void Start()
    {
        TheStatIncreaserScript = GameObject.Find("UniversalGameManager").GetComponent<StatIncreaser>();
        increaseGivenStat();
    }
    void increaseGivenStat()
    {
            if (speedIncreaseAfterAd)
            {
            TheStatIncreaserScript.StartCoroutine(TheStatIncreaserScript.waitAsecondManSpeed());
            Debug.Log("time to increase the speed stat");
        }
            if (jumpIncreaseAfterAd)
            {
            TheStatIncreaserScript.StartCoroutine(TheStatIncreaserScript.waitAsecondManJump());
            Debug.Log("time to increase the jump stat");
        }
        Debug.Log("if you only got this message then it didnt work. this came from " + name);
        
    }

}

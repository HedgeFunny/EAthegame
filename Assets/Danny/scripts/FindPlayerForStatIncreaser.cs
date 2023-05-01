using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayerForStatIncreaser : MonoBehaviour
{
    public GameObject mainPlayer;
    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GameObject.Find("Mr. Top Hat").GetComponent<GameObject>();
    }
}

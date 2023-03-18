using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour
{
    //Variables

    //General
    public GameObject titleScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Transfers you to the title scene
    public void Restart()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
